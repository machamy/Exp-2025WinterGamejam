using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    [SerializeField] private Button[] stageButtons; // 스테이지 버튼들

    public GameObject settingsPopup; // 설정 팝업
    public GameObject StageSelectPopup; // 스테이지 선택 팝업


    private void OnEnable()
    {
        int maxStage = PlayerPrefs.GetInt("MaxStage", 1); // 최대 스테이지 불러오기
        for (int i = 0; i < stageButtons.Length; i++)
        {
            bool isInteractable = i < maxStage; // 클리어한 스테이지까지만 버튼 활성화
            stageButtons[i].interactable = isInteractable;
            ColorBlock cb = stageButtons[i].colors;
            Color color = isInteractable ? cb.normalColor : cb.disabledColor;
            stageButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = color;
        }
    }

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