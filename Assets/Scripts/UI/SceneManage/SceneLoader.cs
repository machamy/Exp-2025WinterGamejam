using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
        SoundManager.Instance.MainBgmOn();
        Time.timeScale = 1.0f;
    }

    public void LoadMainNoChange()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1.0f;
    }

    public void LoadStageSelectfromMain()
    {
        SceneManager.LoadScene("StageSelect");
        Time.timeScale = 1.0f;
    }

    public void LoadStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
        SoundManager.Instance.MainBgmOn();
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
}
