using UnityEngine;
using UnityEngine.UI;
using System;
using DefaultNamespace;

public class FuelStatus : MonoBehaviour
{
    // (옵저버 패턴 핵심) 연료가 바뀔 때마다 int 인자로 던져줄 이벤트
    [SerializeField] private FloatVariableSO FuelVariableSo;

    // 관찰할 대상(FuelStatus)을 Inspector에서 할당
    [SerializeField] private FuelStatus fuelStatus;

    // Inspector에서 할당할 RectTransform (연료 이미지를 표현)
    public RectTransform fuelImage;

    private void Awake()
    {
        // 씬이 시작될 때, FuelStatus의 이벤트에 구독을 건다.
        if (fuelStatus != null)
        {
            FuelVariableSo.OnValueChanged += UpdateFuelUI;
        }
    }
    private void OnDestroy()
    {
        // 오브젝트 파괴 시, 구독 해제(메모리 누수/에러 방지)
        if (fuelStatus != null)
        {
            FuelVariableSo.OnValueChanged -= UpdateFuelUI;
        }
    }
    

    // Fuel이 25일 때 RectTransform의 시작 X 좌표
    private float baseX = -100f;

    // Fuel이 1 줄어들 때마다 이동할 기본 거리
    private float baseMove = 20f;

    // (예시) 5의 배수일 때 추가로 이동할 거리
    private float bonusMove = 15f;

    /// <summary>
    /// FuelStatus.OnFuelChanged 이벤트에 의해 호출되는 메서드
    /// fuel = 변경된 뒤의 연료 값
    /// </summary>
    private void UpdateFuelUI(float fuel)
    {
        if (fuelImage == null)
            return;

        // 기본 계산: 25에서 시작, 1씩 줄면 -20씩 이동
        float usedFuel = 25 - fuel;
        float finalX = baseX - (usedFuel * baseMove);

        // 구간별 '보너스 이동' 계산
        float bonusOffset = 0f;

        // 20 ~ 16 → -15
        if (fuel <= 20 && fuel >= 16)
        {
            bonusOffset = 15f;
        }
        // 15 ~ 11 → -30
        else if (fuel <= 15 && fuel >= 11)
        {
            bonusOffset = 30f;
        }
        // 10 ~ 6 → -45
        else if (fuel <= 10 && fuel >= 6)
        {
            bonusOffset = 45f;
        }
        // 5 ~ 1 → -60
        else if (fuel <= 5 && fuel >= 1)
        {
            bonusOffset = 60f;
        }
        // 0일 때는 -665f로 고정
        else if (fuel == 0)
        {
            finalX = -665f;
        }

        finalX -= bonusOffset;

        // 최종 RectTransform 좌표에 반영
        Vector2 pos = fuelImage.anchoredPosition;
        pos.x = finalX;
        fuelImage.anchoredPosition = pos;
    }
}