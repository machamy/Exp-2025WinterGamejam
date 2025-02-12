using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Serialization;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchInputManager : SingletonBehaviour<TouchInputManager>
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private bool DebugPrint = false;
    [SerializeField] private bool ClearOnDisable = true;
    [SerializeField] private TouchSettingSO TouchSetting;
    private float DragThresholdDistance => TouchSetting.dragThreshold;
    private void Awake()
    {
        EnhancedTouchSupport.Enable();  
        MainCamera = Camera.main;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(TouchSetting == null)
            TouchSetting = ScriptableObject.FindFirstObjectByType<TouchSettingSO>();
        if(TouchSetting == null)
            ScriptableObject.CreateInstance<TouchSettingSO>();
    }

    public event Action<int,Vector2> OnTap;
    public event Action<int,Vector2> OnTouchDown;
    public event Action<int, Vector2> OnTouching; 
    public event Action<int,Vector2> OnTouchUp;
    public event Action<int, Vector2, Vector2> OnDragging;
    public event Action<int, Vector2, Vector2> OnDragEnd;

    private void OnValidate()
    {
        if (Application.IsPlaying(this))
        {
            OnTap -= DebugPrintOnTap;
            OnTouchDown -= DebugPrintOnTouchDown;
            OnTouching -= DebugPrintOnTouching;
            OnTouchUp -= DebugPrintOnTouchUp;
            OnDragging -= DebugPrintOnDragging;
            OnDragEnd -= DebugPrintOnDragEnd;
            if (DebugPrint)
            {
                OnTap += DebugPrintOnTap;
                OnTouchDown += DebugPrintOnTouchDown;
                OnTouching += DebugPrintOnTouching;
                OnTouchUp += DebugPrintOnTouchUp;
                OnDragging += DebugPrintOnDragging;
                OnDragEnd += DebugPrintOnDragEnd;
            }
        }
    }
    
    private void DebugPrintOnTap(int id, Vector2 pos)
    {
        Debug.Log($"OnTap {id} : {pos}");
    }
    
    private void DebugPrintOnTouchDown(int id, Vector2 pos)
    {
        Debug.Log($"OnTouchDown {id} : {pos}");
    }
    
    private void DebugPrintOnTouching(int id, Vector2 pos)
    {
        Debug.Log($"OnTouching {id} : {pos}");
    }
    
    private void DebugPrintOnTouchUp(int id, Vector2 pos)
    {
        Debug.Log($"OnTouchUp {id} : {pos}");
    }
    
    private void DebugPrintOnDragging(int id, Vector2 start, Vector2 end)
    {
        Debug.Log($"OnDragging {id} : {start} -> {end}");
    }
    
    private void DebugPrintOnDragEnd(int id, Vector2 start, Vector2 end)
    {
        Debug.Log($"OnDragEnd {id} : {start} -> {end}");
    }
    
    
    private void OnEnable()
    {
        #region 디버깅 메시지
        if (DebugPrint)
        {
            OnTap += DebugPrintOnTap;
            OnTouchDown += DebugPrintOnTouchDown;
            OnTouching += DebugPrintOnTouching;
            OnTouchUp += DebugPrintOnTouchUp;
            OnDragging += DebugPrintOnDragging;
            OnDragEnd += DebugPrintOnDragEnd;
        }
        #endregion
    }
    

    private void OnDisable()
    {
        if (ClearOnDisable)
        {
            OnTap = null;
            OnTouchDown = null;
            OnTouching = null;
            OnTouchUp = null;
            OnDragging = null;
            OnDragEnd = null;
        }
    }
    
    public Vector2 ScreenToWorldPoint(Vector2 screenPos)
    {
        return MainCamera.ScreenToWorldPoint(screenPos);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            int id = touch.touchId;
            Vector2 pos = touch.screenPosition;
            if (touch.isTap)
            {
                OnTap?.Invoke(id, pos);
                OnTouchUp?.Invoke(id, pos);
            }
            else
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchDown?.Invoke(id, pos);
                        break;
                    case TouchPhase.Moved:
                        OnDragging?.Invoke(id,touch.startScreenPosition,pos);
                        break;
                    case TouchPhase.Stationary:
                        if(Vector2.Distance(touch.startScreenPosition, pos) <= DragThresholdDistance)
                            OnTouching?.Invoke(id, pos);
                        else
                            OnDragging?.Invoke(id, touch.startScreenPosition,pos);
                        break;
                    case TouchPhase.Ended:
                        OnTouchUp?.Invoke(id, pos);
                        if(Vector2.Distance(touch.startScreenPosition, pos) >= DragThresholdDistance)
                            OnDragEnd?.Invoke(id,touch.startScreenPosition,pos);
                        break;
                    case TouchPhase.Canceled:
                        break;
                }
            }
            
        }
    }
}
