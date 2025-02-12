using UnityEngine;
using UnityEngine.UI;
using System;

public class FuelStatus : MonoBehaviour
{
    // (옵저버 패턴 핵심) 연료가 바뀔 때마다 int 인자로 던져줄 이벤트
    public event Action<int> OnFuelChanged;

    // 관찰할 대상(FuelStatus)을 Inspector에서 할당
    [SerializeField] private FuelStatus fuelStatus;

    // Inspector에서 할당할 RectTransform (연료 이미지를 표현)
    public RectTransform fuelImage;

    private void Awake()
    {
        // 씬이 시작될 때, FuelStatus의 이벤트에 구독을 건다.
        if (fuelStatus != null)
        {
            fuelStatus.OnFuelChanged += UpdateFuelUI;
        }
    }
    private void OnDestroy()
    {
        // 오브젝트 파괴 시, 구독 해제(메모리 누수/에러 방지)
        if (fuelStatus != null)
        {
            fuelStatus.OnFuelChanged -= UpdateFuelUI;
        }
    }

    

    // 현재 연료 값 (시작값 25)
    [SerializeField]
    private int fuel = 25;

    // Fuel이 25일 때 RectTransform의 시작 X 좌표
    private float baseX = -100f;

    // Fuel이 1 줄어들 때마다 이동할 기본 거리
    private float baseMove = 20f;

    // (예시) 5의 배수일 때 추가로 이동할 거리
    private float bonusMove = 15f;


    /// <summary>
    /// 외부에서 Fuel 값을 읽을 수 있게끔 프로퍼티를 제공
    /// (원한다면 public set으로 바꿀 수도 있음)
    /// </summary>
    public int Fuel
    {
        get => fuel;
        private set
        {
            // 음수 방지 등 필요하다면 처리
            fuel = Mathf.Max(0, value);

            // (중요) 값이 변경될 때마다 이벤트로 통보
            OnFuelChanged?.Invoke(fuel);
        }
    }

    private void Start()
    {
        // 시작 시점에 현재 Fuel 값을 한 번 알림(초기 UI 동기화 용도)
        OnFuelChanged?.Invoke(fuel);
    }

    /// <summary>
    /// 외부에서 Fuel을 줄이려면 이 메서드를 호출
    /// </summary>
    public void DecreaseFuel(int amount = 1)
    {
        Fuel -= amount;  // 프로퍼티를 통해 값을 변경하면 자동으로 이벤트 발생
    }

    /// <summary>
    /// 외부에서 Fuel을 직접 세팅하려면 이 메서드를 호출
    /// </summary>
    public void SetFuel(int newFuel)
    {
        Fuel = newFuel;  // 마찬가지로 이벤트 발생
    }
    /// <summary>
    /// fuel 값에 따라 RectTransform의 X값을 직접 계산하여 갱신한다.
    /// </summary>
    
    /// <summary>
    /// FuelStatus.OnFuelChanged 이벤트에 의해 호출되는 메서드
    /// fuel = 변경된 뒤의 연료 값
    /// </summary>
    private void UpdateFuelUI(int fuel)
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
    public void OnValidate()
    {
        UpdateFuelUI(Fuel);
    }
}