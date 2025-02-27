
using System;
using UnityEngine;

public class DestinationIndicator : MonoBehaviour
{
    [SerializeField] private ClearArea clearArea = null;
    [SerializeField] private Camera mainCamera = null;

    private void Awake()
    {
        mainCamera = Camera.main;
        if(clearArea == null)
        {
            clearArea = FindFirstObjectByType<ClearArea>();
        }
        if(clearArea == null)
        {
            Debug.LogError("ClearArea is not found");
        }
        else
        {
            clearArea.OnBecameVisibleEvent += OnClearAreaVisible;
            clearArea.OnBecameInvisibleEvent += OnClearAreaInvisible;
        }
    }

    private void Start()
    {
        if(!clearArea.IsVisible)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private float width = 1080;
    private float height = 1920;
    [SerializeField] private float padding = 75;
    private void OnEnable()
    {
        if (!clearArea)
        {
            clearArea = FindFirstObjectByType<ClearArea>();
        }
        mainCamera = Camera.main;
        width = Screen.width;
        height = Screen.height;
    }

    private void OnDestroy()
    {
        clearArea.OnBecameVisibleEvent -= OnClearAreaVisible;
        clearArea.OnBecameInvisibleEvent -= OnClearAreaInvisible;
    }

    private void OnClearAreaVisible(ClearArea clearArea)
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(!clearArea.IsVisible)
        {
            var targetPosition = clearArea.transform.position;
            var screenPos = mainCamera.WorldToScreenPoint(targetPosition);
            var x = Mathf.Clamp(screenPos.x, padding, width - padding);
            var y = Mathf.Clamp(screenPos.y, padding, height - padding);
            float rotation = Quaternion.LookRotation(Vector3.forward, targetPosition - mainCamera.transform.position).eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            transform.position = new Vector3(x, y, 0);
        }
    }

    private void OnClearAreaInvisible(ClearArea clearArea)
    {
        gameObject.SetActive(true);
    }
}
