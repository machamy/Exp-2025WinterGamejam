using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // public void LoadMain()
    // {
    //     SceneManager.LoadScene("Main");
    //     SoundManager.Instance.MainBgmOn();
    //     Time.timeScale = 1.0f;
    // }
    //
    // public void LoadMainNoChange()
    // {
    //     SceneManager.LoadScene("Main");
    //     Time.timeScale = 1.0f;
    // }
    //
    // public void LoadStageSelectfromMain()
    // {
    //     SceneManager.LoadScene("StageSelect");
    //     Time.timeScale = 1.0f;
    // }
    //
    // public void LoadStageSelect()
    // {
    //     SceneManager.LoadScene("StageSelect");
    //     SoundManager.Instance.MainBgmOn();
    //     Time.timeScale = 1.0f;
    // }

    public void LoadStage1()
    {
        GameManager.Instance.GoToStage(1);
    }

    public void LoadStage2()
    {
        if (GameManager.Instance.MaxStage < 2)
        {
            return;
        }
        GameManager.Instance.GoToStage(2);
    }

    public void LoadStage3()
    {
        if (GameManager.Instance.MaxStage < 3)
        {
            return;
        }
        GameManager.Instance.GoToStage(3);
    }
}
