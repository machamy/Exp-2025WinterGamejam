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

    public GameObject settingsPopup; // ¼³Á¤ ÆË¾÷
    public GameObject StageSelectPopup; // ½ºÅ×ÀÌÁö ¼±ÅÃ ÆË¾÷

    // ¼³Á¤ ÆË¾÷ ¿­±â
    public void OpenSettings()
    {
        settingsPopup.SetActive(true);
    }

    // ¼³Á¤ ÆË¾÷ ´Ý±â
    public void CloseSettings()
    {
        settingsPopup.SetActive(false);
    }
    public void OpenStageSelectPopup()
    {
        StageSelectPopup.SetActive(true);
    }

    // ¼³Á¤ ÆË¾÷ ´Ý±â
    public void CloseStageSelectPopup()
    {
        StageSelectPopup.SetActive(false);
    }

}