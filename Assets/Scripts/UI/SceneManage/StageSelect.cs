using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TMPro.Examples;
public class StageSelect : MonoBehaviour
{
    public void LoadMainNoChange()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1.0f;
    }
    public void LoadStage1()
    {
        SceneManager.LoadScene("Stage1");
        SoundManager.Instance.StageBgmOn();
        Time.timeScale = 1.0f;
    }

    public void LoadStage2()
    {
        SceneManager.LoadScene("Stage2");
        SoundManager.Instance.StageBgmOn();
        Time.timeScale = 1.0f;
    }

    public void LoadStage3()
    {
        SceneManager.LoadScene("Stage3");
        SoundManager.Instance.StageBgmOn();
        Time.timeScale = 1.0f;
    }

    public GameObject settingsPopup; // ���� �˾�


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

}
