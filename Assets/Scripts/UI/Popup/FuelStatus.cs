using UnityEngine;
using UnityEngine.UI;

public class FuelStatus : MonoBehaviour
{
    public static FuelStatus Instance { get; private set; }
    // Inspector에서 할당할 RectTransform (연료 이미지를 표현)
    public RectTransform fuelImage;

    // 현재 연료 값 (시작값 25)
    [SerializeField]
    private int fuel = 25;

    // Fuel이 25일 때 RectTransform의 시작 X 좌표
    private float baseX = -100f;

    // Fuel이 1 줄어들 때마다 이동할 기본 거리
    private float baseMove = 20f;

    // (예시) 5의 배수일 때 추가로 이동할 거리
    private float bonusMove = 15f;


    void Start()
    {
        // 시작 시점에 Fuel UI 갱신
        UpdateFuelUI();
    }
    private void Awake()
    {
        // 이미 Instance가 존재하면 중복 제거(싱글톤 패턴 용도)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 싱글톤 할당
        Instance = this;

        // 만약 씬 전환 시에도 파괴되지 않게 하고 싶다면:
        // DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 연료 감소를 트리거하는 함수.
    /// (예: 다른 스크립트에서 canDecreaseFuel = true 로 만들고 이 함수를 호출)
    /// </summary>
    public void TriggerFuelDecrease()
    {
        fuel--;
        UpdateFuelUI();
    }
    public void FuelSet(int getfuel)
    {
        fuel = getfuel;
        UpdateFuelUI();
    }
    /// <summary>
    /// fuel 값에 따라 RectTransform의 X값을 직접 계산하여 갱신한다.
    /// </summary>
    private void UpdateFuelUI()
    {
        if (fuelImage == null) return;

        // 0 이하로 내려가지 않도록 처리(선택사항)
        if (fuel < 0)
        {
            fuel = 0;
        }

        // 1) 기본 X계산: 
        //    "연료 25"일 때 X=-100, 
        //    연료가 1씩 줄 때마다 -20씩 누적
        float usedFuel = 25 - fuel;                // 사용된 연료량
        float finalX = baseX - (usedFuel * baseMove);

        // 2) 구간별 '보너스 이동' 계산
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

        else if(fuel == 0)
        {
            finalX = -665f;
        }
        // 구간에 맞게 추가 이동
        finalX -= bonusOffset;

        // 3) RectTransform에 최종 X좌표 적용
        Vector2 pos = fuelImage.anchoredPosition;
        pos.x = finalX;
        fuelImage.anchoredPosition = pos;
    }
    public void OnValidate()
    {
        UpdateFuelUI();
    }
}