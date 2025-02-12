using System.Collections;
using UnityEngine;

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
    [SerializeField] private ObstacleType obstacleType = ObstacleType.None;
    [SerializeField] private float breakTime = 1f;
    [SerializeField] private float attachCooldown = 1f;
    private float remainingAttachCooldown = 0f;
    public float BreakTime => breakTime;
    public ObstacleType Type => obstacleType;

    
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
        Destroy(gameObject);
    }


}
