using UnityEngine;
using UnityEngine.UI;

public class FuelStatus : MonoBehaviour
{
    public static FuelStatus Instance { get; private set; }
    // Inspector���� �Ҵ��� RectTransform (���� �̹����� ǥ��)
    public RectTransform fuelImage;

    // ���� ���� �� (���۰� 25)
    [SerializeField]
    private int fuel = 25;

    // Fuel�� 25�� �� RectTransform�� ���� X ��ǥ
    private float baseX = -100f;

    // Fuel�� 1 �پ�� ������ �̵��� �⺻ �Ÿ�
    private float baseMove = 20f;

    // (����) 5�� ����� �� �߰��� �̵��� �Ÿ�
    private float bonusMove = 15f;


    void Start()
    {
        // ���� ������ Fuel UI ����
        UpdateFuelUI();
    }
    private void Awake()
    {
        // �̹� Instance�� �����ϸ� �ߺ� ����(�̱��� ���� �뵵)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �̱��� �Ҵ�
        Instance = this;

        // ���� �� ��ȯ �ÿ��� �ı����� �ʰ� �ϰ� �ʹٸ�:
        // DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// ���� ���Ҹ� Ʈ�����ϴ� �Լ�.
    /// (��: �ٸ� ��ũ��Ʈ���� canDecreaseFuel = true �� ����� �� �Լ��� ȣ��)
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
    /// fuel ���� ���� RectTransform�� X���� ���� ����Ͽ� �����Ѵ�.
    /// </summary>
    private void UpdateFuelUI()
    {
        if (fuelImage == null) return;

        // 0 ���Ϸ� �������� �ʵ��� ó��(���û���)
        if (fuel < 0)
        {
            fuel = 0;
        }

        // 1) �⺻ X���: 
        //    "���� 25"�� �� X=-100, 
        //    ���ᰡ 1�� �� ������ -20�� ����
        float usedFuel = 25 - fuel;                // ���� ���ᷮ
        float finalX = baseX - (usedFuel * baseMove);

        // 2) ������ '���ʽ� �̵�' ���
        float bonusOffset = 0f;

        // 20 ~ 16 �� -15
        if (fuel <= 20 && fuel >= 16)
        {
            bonusOffset = 15f;
        }
        // 15 ~ 11 �� -30
        else if (fuel <= 15 && fuel >= 11)
        {
            bonusOffset = 30f;
        }
        // 10 ~ 6 �� -45
        else if (fuel <= 10 && fuel >= 6)
        {
            bonusOffset = 45f;
        }
        // 5 ~ 1 �� -60
        else if (fuel <= 5 && fuel >= 1)
        {
            bonusOffset = 60f;
        }

        else if(fuel == 0)
        {
            finalX = -665f;
        }
        // ������ �°� �߰� �̵�
        finalX -= bonusOffset;

        // 3) RectTransform�� ���� X��ǥ ����
        Vector2 pos = fuelImage.anchoredPosition;
        pos.x = finalX;
        fuelImage.anchoredPosition = pos;
    }
    public void OnValidate()
    {
        UpdateFuelUI();
    }
}