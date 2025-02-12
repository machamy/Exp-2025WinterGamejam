
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class AlertUI : MonoBehaviour
{
    [SerializeField] private string alertText = "{0}";
    [SerializeField] private TextMeshProUGUI alertTextUI = null;
    [SerializeField] private AlertEventChannel alertEventChannel = null;

    private void Awake()
    {
        alertEventChannel.OnStartAlert += OnStartAlert;
        alertEventChannel.OnUpdateAlert += OnUpdateAlert;
        alertEventChannel.OnEndAlert += OnEndAlert;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        alertEventChannel.OnStartAlert -= OnStartAlert;
        alertEventChannel.OnUpdateAlert -= OnUpdateAlert;
        alertEventChannel.OnEndAlert -= OnEndAlert;
    }
    
    
    private void OnStartAlert(float maxTime)
    {
        gameObject.SetActive(true);
        alertTextUI.text = string.Format(alertText, GetTime(maxTime));
    }
    
    private void OnUpdateAlert(float maxTime, float currentTime)
    {
        
        alertTextUI.text = string.Format(alertText, GetTime(maxTime-currentTime));
    }
    
    private void OnEndAlert()
    {
        alertTextUI.gameObject.SetActive(false);
    }

    private string GetTime(float time)
    {
        return $"{(int)time/60}:{(int)time%60:00}";
    }
}
