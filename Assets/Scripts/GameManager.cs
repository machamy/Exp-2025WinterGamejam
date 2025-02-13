using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{
    
    
    public enum GameState
    {
         None,
         Main,
         Dialog,
         Running,
         Dead
    }

    
    public static SoundManager Sound => SoundManager.Instance;

    public GameState State = GameState.None;
 
    public void GoToMain()
    {
        State = GameState.Main;
        SceneManager.LoadScene("Main");
    }
    
    public void GoToStage(int stage)
    {
        State = GameState.Running;
        switch (stage)
        {
            case 1:
                Instance.State = GameState.Dialog;
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                Instance.State = GameState.Running;
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                Instance.State = GameState.Running;
                SceneManager.LoadScene("Stage3");
                break;
        }
        SoundManager.Instance.PlayBGM(SoundData.Sound.StageBgm);
        Time.timeScale = 1.0f;
    }
}
