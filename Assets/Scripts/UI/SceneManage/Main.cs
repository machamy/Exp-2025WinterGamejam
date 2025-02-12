using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Main : MonoBehaviour
{
    

    public void LoadStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public GameObject settingsPopup; // 설정 팝업
    public GameObject StageSelectPopup; // 스테이지 선택 팝업

    // 설정 팝업 열기
    public void OpenSettings()
    {
        settingsPopup.SetActive(true);
    }

    // 설정 팝업 닫기
    public void CloseSettings()
    {
        settingsPopup.SetActive(false);
    }
    public void OpenStageSelectPopup()
    {
        StageSelectPopup.SetActive(true);
    }

    // 설정 팝업 닫기
    public void CloseStageSelectPopup()
    {
        StageSelectPopup.SetActive(false);
    }

}