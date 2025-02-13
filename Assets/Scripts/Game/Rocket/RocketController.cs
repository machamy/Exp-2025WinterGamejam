using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(Rocket))]
public class RocketController : MonoBehaviour
{
    private TouchInputManager touchInputManager;
    private Rocket rocket;
   
    private void Awake()
    {
        touchInputManager = TouchInputManager.Instance;
        rocket = GetComponent<Rocket>();
    }

    private void OnEnable()
    {
        if(touchInputManager == null)
            touchInputManager = TouchInputManager.Instance;
        // TouchInputManager.Instance.OnTouchDown += OnTouchDown;
        touchInputManager.OnTouching += OnTouching;
        touchInputManager.OnTouchUp += OnTouchUp;
        touchInputManager.OnDragging += OnDragging;
        touchInputManager.OnDragEnd += OnDragEnd;  
    }
    
    private void OnDisable()
    {
        // TouchInputManager.Instance.OnTouchDown -= OnTouchDown;
        touchInputManager.OnTouching -= OnTouching;
        touchInputManager.OnTouchUp -= OnTouchUp;
        touchInputManager.OnDragging -= OnDragging;
        touchInputManager.OnDragEnd -= OnDragEnd;  
    }

    #region 인풋 이벤트 핸들러
    // private void OnTouchDown(int id, Vector2 pos)
    // {
    //     Debug.Log($"OnTouchDown {id} : {pos}");
    // }
    
    
    // 드래그 중에 부스트 X
    private int lastBoostId = -1;
    private void OnTouching(int id, Vector2 pos)
    {
        if(GameManager.Instance.State != GameManager.GameState.Running)
            return;
        if(rocket.State == Rocket.RocketState.Attached)
        {
            rocket.Detach();
        }
        if (rocket.State == Rocket.RocketState.Normal)
        {
            lastBoostId = id;
            rocket.StartBoost();
        }
    }
    
    private void OnTouchUp(int id, Vector2 pos)
    {
        if(GameManager.Instance.State != GameManager.GameState.Running)
            return;
        if (rocket.IsBoosting)
        {
            // if (lastBoostId == id)
            rocket.EndBoost();
        }
    }
    
    private void OnDragging(int id, Vector2 pos, Vector2 endPos)
    {
        if(GameManager.Instance.State != GameManager.GameState.Running)
            return;
    }
    

    private void OnDragEnd(int id, Vector2 pos, Vector2 endPos)
    {
        
        if(GameManager.Instance.State != GameManager.GameState.Running)
            return;
        if(rocket.State == Rocket.RocketState.Attached)
        {
            rocket.Detach();
        }
        Vector2 direction = endPos - pos;
        rocket.PointDirection(direction);
    }
    
    #endregion

    // public Vector2 NormalVelocity => transform.up * speed;
    // public Vector2 BoostVelocity => transform.up * boostSpeed;
    private void Start()
    {
        rocket.Launch();
    }
    
}
