using UnityEngine;
using UnityEngine.UI;
using System;
using DefaultNamespace;

public class FuelStatus : MonoBehaviour
{
    // (������ ���� �ٽ�) ���ᰡ �ٲ� ������ int ���ڷ� ������ �̺�Ʈ
    [SerializeField] private FloatVariableSO FuelVariableSo;

    // ������ ���(FuelStatus)�� Inspector���� �Ҵ�
    [SerializeField] private FuelStatus fuelStatus;

    // Inspector���� �Ҵ��� RectTransform (���� �̹����� ǥ��)
    public RectTransform fuelImage;

    private void Awake()
    {
        // ���� ���۵� ��, FuelStatus�� �̺�Ʈ�� ������ �Ǵ�.
        if (fuelStatus != null)
        {
            FuelVariableSo.OnValueChanged += UpdateFuelUI;
        }
    }
    private void OnDestroy()
    {
        // ������Ʈ �ı� ��, ���� ����(�޸� ����/���� ����)
        if (fuelStatus != null)
        {
            FuelVariableSo.OnValueChanged -= UpdateFuelUI;
        }
    }
    

    // Fuel�� 25�� �� RectTransform�� ���� X ��ǥ
    private float baseX = -100f;

    // Fuel�� 1 �پ�� ������ �̵��� �⺻ �Ÿ�
    private float baseMove = 20f;

    // (����) 5�� ����� �� �߰��� �̵��� �Ÿ�
    private float bonusMove = 15f;

    /// <summary>
    /// FuelStatus.OnFuelChanged �̺�Ʈ�� ���� ȣ��Ǵ� �޼���
    /// fuel = ����� ���� ���� ��
    /// </summary>
    private void UpdateFuelUI(float fuel)
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
}