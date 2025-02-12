
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Channel/AlertEventChannel")]
public class AlertEventChannel : ScriptableObject
{
    
    public event Action<float> OnStartAlert;
    public event Action<float,float> OnUpdateAlert;
    public event Action OnEndAlert;
    
    public void RaiseStartAlert(float maxtime)
    {
        OnStartAlert?.Invoke(maxtime);
    }
    
    public void RaiseUpdateAlert(float maxtime, float curenttime)
    {
        OnUpdateAlert?.Invoke(maxtime,curenttime);
    }
    
    public void RaiseEndAlert(float curenttime)
    {
        OnEndAlert?.Invoke();
    }
    
}
