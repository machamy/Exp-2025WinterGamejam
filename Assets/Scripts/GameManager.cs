using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
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
 
    private void Awake()
    {
        
    }


    private void Update()
    {

    }
}
