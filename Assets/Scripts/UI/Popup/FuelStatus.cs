using UnityEngine;
using UnityEngine.UI;
using System;

public class FuelStatus : MonoBehaviour
{
    // (������ ���� �ٽ�) ���ᰡ �ٲ� ������ int ���ڷ� ������ �̺�Ʈ
    public event Action<int> OnFuelChanged;

    // ������ ���(FuelStatus)�� Inspector���� �Ҵ�
    [SerializeField] private FuelStatus fuelStatus;

    // Inspector���� �Ҵ��� RectTransform (���� �̹����� ǥ��)
    public RectTransform fuelImage;

    private void Awake()
    {
        // ���� ���۵� ��, FuelStatus�� �̺�Ʈ�� ������ �Ǵ�.
        if (fuelStatus != null)
        {
            fuelStatus.OnFuelChanged += UpdateFuelUI;
        }
    }
    private void OnDestroy()
    {
        // ������Ʈ �ı� ��, ���� ����(�޸� ����/���� ����)
        if (fuelStatus != null)
        {
            fuelStatus.OnFuelChanged -= UpdateFuelUI;
        }
    }

    

    // ���� ���� �� (���۰� 25)
    [SerializeField]
    private int fuel = 25;

    // Fuel�� 25�� �� RectTransform�� ���� X ��ǥ
    private float baseX = -100f;

    // Fuel�� 1 �پ�� ������ �̵��� �⺻ �Ÿ�
    private float baseMove = 20f;

    // (����) 5�� ����� �� �߰��� �̵��� �Ÿ�
    private float bonusMove = 15f;


    /// <summary>
    /// �ܺο��� Fuel ���� ���� �� �ְԲ� ������Ƽ�� ����
    /// (���Ѵٸ� public set���� �ٲ� ���� ����)
    /// </summary>
    public int Fuel
    {
        get => fuel;
        private set
        {
            // ���� ���� �� �ʿ��ϴٸ� ó��
            fuel = Mathf.Max(0, value);

            // (�߿�) ���� ����� ������ �̺�Ʈ�� �뺸
            OnFuelChanged?.Invoke(fuel);
        }
    }

    private void Start()
    {
        // ���� ������ ���� Fuel ���� �� �� �˸�(�ʱ� UI ����ȭ �뵵)
        OnFuelChanged?.Invoke(fuel);
    }

    /// <summary>
    /// �ܺο��� Fuel�� ���̷��� �� �޼��带 ȣ��
    /// </summary>
    public void DecreaseFuel(int amount = 1)
    {
        Fuel -= amount;  // ������Ƽ�� ���� ���� �����ϸ� �ڵ����� �̺�Ʈ �߻�
    }

    /// <summary>
    /// �ܺο��� Fuel�� ���� �����Ϸ��� �� �޼��带 ȣ��
    /// </summary>
    public void SetFuel(int newFuel)
    {
        Fuel = newFuel;  // ���������� �̺�Ʈ �߻�
    }
    /// <summary>
    /// fuel ���� ���� RectTransform�� X���� ���� ����Ͽ� �����Ѵ�.
    /// </summary>
    
    /// <summary>
    /// FuelStatus.OnFuelChanged �̺�Ʈ�� ���� ȣ��Ǵ� �޼���
    /// fuel = ����� ���� ���� ��
    /// </summary>
    private void UpdateFuelUI(int fuel)
    {
        if (fuelImage == null)
            return;

        // �⺻ ���: 25���� ����, 1�� �ٸ� -20�� �̵�
        float usedFuel = 25 - fuel;
        float finalX = baseX - (usedFuel * baseMove);

        // ������ '���ʽ� �̵�' ���
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
        // 0�� ���� -665f�� ����
        else if (fuel == 0)
        {
            finalX = -665f;
        }

        finalX -= bonusOffset;

        // ���� RectTransform ��ǥ�� �ݿ�
        Vector2 pos = fuelImage.anchoredPosition;
        pos.x = finalX;
        fuelImage.anchoredPosition = pos;
    }
    public void OnValidate()
    {
        UpdateFuelUI(Fuel);
    }
}