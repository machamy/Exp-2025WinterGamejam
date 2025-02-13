using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(FixedJoint2D))]
public class Rocket : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rbody;
    private FixedJoint2D joint;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [Header("Event Channel")]
    [SerializeField] private AlertEventChannel alertEventChannel;
    [SerializeField] private UnityEvent onClearEvent;
    [SerializeField] private UnityEvent onFailEvent;
    // [SerializeField] private FloatVariableSO fuelVariableChannel;

    [Header("Rocket Settings(Movement)")]
    [SerializeField] private bool useAngularVelocity = false;
    [SerializeField] private bool updateSpeedOnTick = true;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float boostSpeed = 10f;
    [SerializeField] private FloatVariableSO maxFuel;
    [SerializeField] private float boostCost =  1f;
    [SerializeField] private float rotateCost = 1f;
    
    public float MaxFuel
    {
        get => maxFuel.Value;
        private set => maxFuel.Value = value;
    }
    [Header("Rocket Settings(Interraction)")]
    [SerializeField] private bool applyGravityArea = true;
    [SerializeField] private float gravityRotationSpeed = 1f;
    [SerializeField] private bool applyHeatArea = true;
    [SerializeField] private Color originalColor = Color.white;
    [SerializeField] private Color heatColor = Color.red;

    [Header("Rocket State")] [SerializeField]
    private FloatVariableSO fuelVariable;
    [SerializeField] private float currentHeat = 0f;
    [SerializeField] private float heatTime = 2f;
    [SerializeField] private RocketState state = RocketState.None;
    [SerializeField] private bool isBoosting = false;
    
    private HashSet<GravityArea> gravityAreas = new HashSet<GravityArea>();
    private HashSet<HeatArea> heatAreas = new HashSet<HeatArea>();

    public float Fuel
    {
        get => fuelVariable.Value;
        private set => fuelVariable.Value = value;
    }
    public bool IsOnHeat => heatAreas.Count > 0;
    public float CurrentHeat => currentHeat;
    public float RemainingHeatTime => heatTime - currentHeat;
    public bool IsAbsAttached => state == RocketState.Attached || state == RocketState.BreakingAttached;
    [Header("Passenger")]
    [SerializeField] private IntListVariableSO passengers;
    // [SerializeField] private FloatListVariableSO passengerListVariable;
    [SerializeField] private float[] sizeArr = new []{1f,1.3f,1.69f,2.2f,2.86f};
    [SerializeField] private float[] fuelArr = new []{25f,20f,15f,10f,5f};
    public int PassengerCount => passengers.Count;
    
    private Vector2 previousPosition;
    private Vector2 deltaPosition;
    public Vector2 DeltaPosition => deltaPosition;
   
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
        Dead,
        Clear
    }
    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();
        if(!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();
        rbody.gravityScale = 0;
        Fuel = MaxFuel;
        previousPosition = transform.position;
        deltaPosition = Vector2.zero;
        rbody.centerOfMass = Vector2.up;
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
        originalColor = spriteRenderer.color;
        UpdatePassengerState();
        Launch();
    }

    public void Launch()
    {
        State = RocketState.Normal;
    }

    public void StartBoost()
    {
        if(Fuel <= 0) return;
        if (!IsBoosting)
        {
            SoundManager.Instance.PlaySFX(SoundData.Sound.RocketBoost, 1f, true);
        }
        IsBoosting = true;
    }
    
    public void EndBoost()
    {
        SoundManager.Instance.StopSFX(SoundData.Sound.RocketBoost);
        IsBoosting = false;
    }

    public void UpdateSpeed()
    {
        Vector2 vel;
        switch (state)
        {   
            case RocketState.None:
                return;
            case RocketState.Normal:
                if (isBoosting)
                    vel = BoostVelocity;
                else
                    vel = NormalVelocity;
                break;
            case RocketState.Attached:
            case RocketState.BreakingAttached:
                return;
            case RocketState.Dead:
                vel = Vector2.zero;
                break;
            default:
                return;
                break;
        }

        if (applyGravityArea)
        {
            Vector2 gravity = Vector2.zero;
            foreach (var area in gravityAreas)
            {
                gravity += area.GetAcceleration(rbody.position) * Time.fixedDeltaTime;
            }
            vel += gravity;

            if (gravity != Vector2.zero)
            {
                PointDirectionSmooth(vel, gravityRotationSpeed, false,false);
            }
        }
        rbody.linearVelocity = vel;
    }

    #if UNITY_EDITOR
    
    [ContextMenu("Add Test Passenger")]
    public void AddTestPassenger()
    {
        AddPassenger(0);
    }
    [ContextMenu("Remove Test Passenger")]
    public void RemoveTestPassenger()
    {
        passengers.RemoveAt(passengers.Count-1);
        UpdatePassengerState();
    }
    #endif
    public void AddPassenger(int id)
    {
        passengers.AddValue(id);
        UpdatePassengerState();
    }
    
    public void PopPassenger()
    {
        passengers.RemoveAt(passengers.Count-1);
        UpdatePassengerState();
    }
    
    public List<int> GetPassenger()
    {
        return new List<int>(passengers.Value);
    }
    
    public void ClearPassenger()
    {
        passengers.Clear();
        UpdatePassengerState();
    }
    
    [ContextMenu("Update Passenger State")]
    public void UpdatePassengerState()
    {
        print("Update Passenger State");
        MaxFuel = fuelArr[passengers.Count];
        Fuel = MaxFuel;
        transform.localScale = Vector3.one * sizeArr[passengers.Count];
    }

    private void FixedUpdate()
    {
        var position = rbody.position;
        deltaPosition = (Vector2)position - previousPosition;
        previousPosition = position;
        if(!useAngularVelocity)
            rbody.angularVelocity = 0;
        if (updateSpeedOnTick)
            UpdateSpeed();
        if (State == RocketState.BreakingAttached || IsBoosting && State!=RocketState.Dead)
        {
            Fuel -= boostCost * Time.fixedDeltaTime;
            if (Fuel <= 0)
            {
                Fuel = 0;
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     
    // }
    
    public void PointTo(Vector2 target,bool updateSpeed = true)
    {
        PointDirection(target - (Vector2)transform.position,updateSpeed);
    }
    
    public void PointDirection(Vector2 dir,bool useFuel = true, bool updateSpeed = true)
    {
        if(useFuel && Fuel <= 0)
            return;
        // transform.up = dir.normalized;
        var rot = Quaternion.LookRotation(Vector3.forward, dir).eulerAngles.z;
         // print(rot);
        rbody.SetRotation(rot);
        // print(rot);
        Fuel -= rotateCost;
        if(updateSpeed)
            UpdateSpeed();
    }

    public void PointDirectionSmooth(Vector2 dir, float smoothness,bool useFuel = true,bool updateSpeed = true)
    {
        if(useFuel && Fuel <= 0)
            return;
        var targetRot = Quaternion.LookRotation(Vector3.forward, dir).eulerAngles.z;
        var currentRot = rbody.rotation;
        var newRot = Mathf.LerpAngle(currentRot, targetRot, smoothness * Time.fixedDeltaTime);
        rbody.SetRotation(newRot);
        if(useFuel)
            Fuel -= rotateCost;
        // fuel -= rotateCost;
        if(updateSpeed)
         UpdateSpeed();
    }
    
    public void OnHeatAreaEnter(HeatArea area)
    {
        if (!applyHeatArea)
            return;
        if (heatAreas.Contains(area))
        {
            return;
        }
        if(!IsOnHeat)
        {
            heatAreas.Add(area);
            alertEventChannel.RaiseStartAlert(heatTime);
            StartCoroutine(HeatRoutine());
        }
        else
        {
            heatAreas.Add(area);
        }
    }
    
    private IEnumerator HeatRoutine()
    {
        if(!IsOnHeat) 
            yield break;
        while (currentHeat < heatTime)
        {
            yield return new WaitForFixedUpdate();
            currentHeat += Time.fixedDeltaTime;
            float t = currentHeat / heatTime;
            spriteRenderer.color = Color.Lerp(originalColor, heatColor, t);
            alertEventChannel.RaiseUpdateAlert(heatTime, currentHeat);
            if(!IsOnHeat)
                yield break;
        }
        if(!IsOnHeat)
            yield break;
        alertEventChannel.RaiseEndAlert(currentHeat);
        Die();
    }
    
    public void OnHeatAreaExit(HeatArea area)
    {
        alertEventChannel.RaiseEndAlert(currentHeat);
        spriteRenderer.color = originalColor;
        if(!heatAreas.Contains(area))
            return;
        heatAreas.Remove(area);
        if(!IsOnHeat)
            currentHeat = 0;
    }


    
    public void AddGravityArea(GravityArea area)
    {
        if(gravityAreas.Contains(area))
            return;
        gravityAreas.Add(area);
    }
    
    public void RemoveGravityArea(GravityArea area)
    {
        if(!gravityAreas.Contains(area))
            return;
        gravityAreas.Remove(area);
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
    
    [ContextMenu("Clear")]
    public void OnClearAreaEnter()
    {
        // TODO : CLEAR
        Debug.Log("Clear!!!");
        GameManager.Instance.StageClear();
        State = RocketState.Clear;
        onClearEvent.Invoke();
    }
    public void Die()
    {
        State = RocketState.Dead;
        Debug.Log("Rocket Died");
        SoundManager.Instance.PlayBGM(SoundData.Sound.GameOver);
        SoundManager.Instance.StopSFX();
        SoundManager.Instance.PlaySFX(SoundData.Sound.RocketExplosion, 1f);
        onFailEvent.Invoke();
        gameObject.SetActive(false);
    }
}
