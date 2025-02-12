using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseObstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        None,
        Breakable,
        BreakableByBoost,
        Attachable,
        Unbreakable
    }
    [SerializeField] private GameObject breakEffect = null;
    [SerializeField] private ObstacleType obstacleType = ObstacleType.None;
    [SerializeField] private float breakTime = 1f;
    [SerializeField] private float attachCooldown = 1f;
    private float remainingAttachCooldown = 0f;
    public float BreakTime => breakTime;
    public ObstacleType Type => obstacleType;

    private void Reset()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void OnRocketCollision(Rocket rocket,bool isHead)
    {
        switch (obstacleType)
        {
            case ObstacleType.Breakable:
                Break();
                break;
            case ObstacleType.BreakableByBoost:
                if(rocket.IsAbsAttached || !isHead)
                {
                    return;
                }
                if(rocket.IsBoosting)
                {
                    rocket.AttachForBreak(this);
                }
                else
                {
                    rocket.Die();
                }
                break;
            case ObstacleType.Unbreakable:
                rocket.Die();
                break;
            case ObstacleType.Attachable:
                if(rocket.IsAbsAttached || remainingAttachCooldown > 0)
                {
                    return;
                }
                rocket.Attach(this);
                remainingAttachCooldown = attachCooldown;
                break;
        }
    }
    
    private void Update()
    {
        if(remainingAttachCooldown > 0)
        {
            remainingAttachCooldown -= Time.deltaTime;
        }
    }
    public void Break()
    {
        if(breakEffect != null)
        {
            var effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3f);
        }
        Destroy(gameObject);
    }


}
