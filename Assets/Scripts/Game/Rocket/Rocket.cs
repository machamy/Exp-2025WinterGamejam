using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{

    private Rigidbody2D rbody;

    [Header("Rocket Settings")]
    [SerializeField] private bool updateSpeedOnTick = true;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float boostSpeed = 10f;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float boostCost =  1f;
    [Header("Rocket State")]
    [SerializeField] private float fuel = 100f;
    [SerializeField] private RocketState state = RocketState.None;
    
    
    public float Fuel => fuel;
    public RocketState State
    {
        get => state;
        set
        {
            state = value;
            UpdateSpeed();
        }
    }
    public enum RocketState
    {
        None,
        Normal,
        Boosting,
        Attached,
        Dead
    }
    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        fuel = maxFuel;
    }
   

    // public Vector2 NormalVelocity => transform.up * speed;
    // public Vector2 BoostVelocity => transform.up * boostSpeed;
    public Vector2 up
    {
        get
        {
            float rot = rbody.rotation;
            // print(rot);
            return new Vector2(-Mathf.Sin(rot * Mathf.Deg2Rad), Mathf.Cos(rot * Mathf.Deg2Rad));
        }
    }
    public Vector2 NormalVelocity => up * speed;
    public Vector2 BoostVelocity => up * boostSpeed;
    private void Start()
    {
        Launch();
    }

    public void Launch()
    {
        State = RocketState.Normal;
    }

    public void StartBoost()
    {
        if(fuel <= 0) return;
        State = RocketState.Boosting;
    }
    
    public void EndBoost()
    {
        State = RocketState.Normal;
    }

    public void UpdateSpeed()
    {
        switch (state)
        {
            case RocketState.Normal:
                rbody.linearVelocity = NormalVelocity;
                break;
            case RocketState.Boosting:
                rbody.linearVelocity = BoostVelocity;
                break;
            case RocketState.Attached:
                break;
            case RocketState.Dead:
                rbody.linearVelocity = Vector2.zero;
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if (updateSpeedOnTick)
            UpdateSpeed();
        if (State == RocketState.Boosting)
        {
            fuel -= boostCost * Time.fixedDeltaTime;
            if (fuel <= 0)
            {
                fuel = 0;
                EndBoost();
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        
    }

    private void Move()
    {
        
        
    }

    public void PointTo(Vector2 target)
    {
        PointDirection(target - (Vector2)transform.position);
    }
    
    public void PointDirection(Vector2 dir)
    {
        // transform.up = dir.normalized;
        var rot = Quaternion.LookRotation(Vector3.forward, dir).eulerAngles.z;
         // print(rot);
        rbody.SetRotation(rot);
        // print(rot);
        UpdateSpeed();
    }

    public void Die()
    {
        
    }
}
