using System;
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

    public static bool DoTutorial =  true;
    
    public static SoundManager Sound => SoundManager.Instance;

    public GameState State = GameState.None;
    [SerializeField]private int _stage = 0;
    public int Stage => _stage;

    private int maxStage = 1;
    public int MaxStage => maxStage;

    private void OnEnable()
    {
        maxStage = PlayerPrefs.GetInt("MaxStage", 1);
    }
    
    private void OnDisable()
    {
        PlayerPrefs.SetInt("MaxStage", maxStage);
    }

    public void GoToMain()
    {
        State = GameState.Main;
        SceneManager.LoadScene("Main");
        PlayerPrefs.SetInt("SkipIntro", 1);
    }
    
    public void GoToStage(int stage)
    {
        State = GameState.Running;
        _stage = stage;
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
    
    public void StageClear() => StageClear(_stage);
    public void StageClear(int stage)
    {
        if (stage == _stage)
        {
            if (stage == maxStage)
            {
                maxStage++;
            }
        }
    }
}
