using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Stage1 : MonoBehaviour
{
    [SerializeField] private FuelStatus fuelStatus;
    public void LoadStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public GameObject settingsPopup; // ���� �˾�
    public GameObject StageSelectPopup; // �������� ���� �˾�

    // ���� �˾� ����
    public void OpenSettings()
    {
        settingsPopup.SetActive(true);
    }

    // ���� �˾� �ݱ�
    public void CloseSettings()
    {
        settingsPopup.SetActive(false);
    }
    public void FuelDecrease()
    {
        fuelStatus.DecreaseFuel(1);
    }
    public void FuelSet()
    {
        fuelStatus.SetFuel(25);
    }



}