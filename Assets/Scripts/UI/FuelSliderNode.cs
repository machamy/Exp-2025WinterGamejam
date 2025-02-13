using System;
using UnityEngine;
using UnityEngine.UI;

public class FuelSliderNode : MonoBehaviour
{

    [SerializeField] private GameObject fillBar;
    [SerializeField] private float startX = -100f;
    [SerializeField] private float finalX = 0;

    [SerializeField] private float value = 1f;

    private void Awake()
    {
        startX = -fillBar.GetComponent<Image>().rectTransform.localScale.x;
    }

    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            UpdateFuelUI();
        }
    }
    
    public void UpdateFuelUI()
    {
        if (fillBar == null)
            return;
        float x = Mathf.Lerp(startX, finalX, value);
        fillBar.transform.localPosition = new Vector3(x, 0, 0);
    }

    private void OnValidate()
    {
        UpdateFuelUI();
    }
}
