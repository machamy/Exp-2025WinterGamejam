using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(FixedJoint2D))]
public class Rocket : MonoBehaviour
{

    private Rigidbody2D rbody;
    private FixedJoint2D joint;

    [Header("Rocket Settings")]
    [SerializeField] private bool useAngularVelocity = false;
    [SerializeField] private bool updateSpeedOnTick = true;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float boostSpeed = 10f;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float boostCost =  1f;
    [SerializeField] private float rotateCost = 1f;
    [Header("Rocket State")]
    [SerializeField] private float fuel = 100f;
    [SerializeField] private RocketState state = RocketState.None;
    [SerializeField] private bool isBoosting = false;
    [Header("Passenger")]
    [SerializeField] private bool hasPassenger = false;
    [SerializeField] private List<int> passengers = new List<int>();
    [SerializeField] private float[] sizeArr = new []{1f,1.3f,1.69f,2.2f,2.86f};
    [SerializeField] private float[] fuelArr = new []{25f,20f,15f,10f,5f};
    public int PassengerCount => passengers.Count;
    public bool IsAbsAttached => state == RocketState.Attached || state == RocketState.BreakingAttached;
    
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
    
    public bool IsBoosting
    {
        get => isBoosting;
        set
        {
            isBoosting = value;
            UpdateSpeed();
        }
    }
    public enum RocketState
    {
        None,
        Normal,
        Attached,
        BreakingAttached,
        Dead
    }
    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();
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
        UpdatePassengerState();
        Launch();
    }

    public void Launch()
    {
        State = RocketState.Normal;
    }

    public void StartBoost()
    {
        if(fuel <= 0) return;
        IsBoosting = true;
    }
    
    public void EndBoost()
    {
        IsBoosting = false;
    }

    public void UpdateSpeed()
    {
        switch (state)
        {
            case RocketState.Normal:
                if (isBoosting)
                    rbody.linearVelocity = BoostVelocity;
                else
                    rbody.linearVelocity = NormalVelocity;
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

    public void AddPassenger(int id)
    {
        passengers.Add(id);
        UpdatePassengerState();
    }
    public void UpdatePassengerState()
    {
        maxFuel = fuelArr[passengers.Count];
        transform.localScale = Vector3.one * sizeArr[passengers.Count];
    }

    private void FixedUpdate()
    {
        if(!useAngularVelocity)
            rbody.angularVelocity = 0;
        if (updateSpeedOnTick)
            UpdateSpeed();
        if (State == RocketState.BreakingAttached || IsBoosting && State!=RocketState.Dead)
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
        if(other.gameObject.TryGetComponent<BaseObstacle>(out var obstacle))
        {
            obstacle.OnRocketCollision(this,other.otherCollider.CompareTag("PlayerHead"));
        }
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
        fuel -= rotateCost;
        UpdateSpeed();
    }

    
    public void Attach(BaseObstacle parent)
    {
        joint.enabled = true;
        joint.connectedBody = parent.GetComponent<Rigidbody2D>();
        State = RocketState.Attached;
    }
    
    public void AttachForBreak(BaseObstacle parent)
    {
        joint.enabled = true;
        joint.connectedBody = parent.GetComponent<Rigidbody2D>();
        State = RocketState.BreakingAttached;
        StartCoroutine(BreakRoutine(parent));
    }
    private IEnumerator BreakRoutine(BaseObstacle obstacle)
    {
        float remainTime = obstacle.BreakTime;
        while(remainTime>0)
        {
            yield return new WaitForFixedUpdate();
            remainTime -= Time.fixedDeltaTime;
        }
        obstacle.Break();
        Detach();
    }
    public void Detach(RocketState state = RocketState.Normal)
    {
        joint.connectedBody = null;
        joint.enabled = false;
        State = state;
    }
    public void Die()
    {
        State = RocketState.Dead;
        Debug.Log("Rocket Died");
        gameObject.SetActive(false);
    }
}
