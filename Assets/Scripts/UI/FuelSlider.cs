using System;
using DefaultNamespace;
using UnityEngine;

public class FuelSlider : MonoBehaviour
{
    [SerializeField] private FloatVariableSO maxFuelVariableSo;
    [SerializeField] private FloatVariableSO fuelVariableSo;
    
    [SerializeField] private FuelSliderNode[] fuelSliderNodes;
    private void OnEnable()
    {
        fuelVariableSo.OnValueChanged += UpdateFuelUI;
    }
    
    private void OnDisable()
    {
        fuelVariableSo.OnValueChanged -= UpdateFuelUI;
    }
    
    private void UpdateFuelUI(float fuel)
    {
        // print(fuel);
        if (fuelVariableSo == null)
            return;
        int n = (int) 25 / fuelSliderNodes.Length;
        int idx = (int)fuel / (int)n;
        for(int i = 0; i < idx; i++)
        {
            fuelSliderNodes[i].Value = 1;
        }
        if(fuelSliderNodes.Length > idx)
        {
            fuelSliderNodes[idx].Value = fuel % fuelSliderNodes.Length / fuelSliderNodes.Length;
        }
        for(int i = idx + 1; i < fuelSliderNodes.Length; i++)
        {
            fuelSliderNodes[i].Value = 0;
        }
    }
}
