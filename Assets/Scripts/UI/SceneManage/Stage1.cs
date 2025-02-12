using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Stage1 : MonoBehaviour
{
    
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
        FuelStatus.Instance.TriggerFuelDecrease();
    }
    public void FuelSet()
    {
        FuelStatus.Instance.FuelSet(25);
    }



}