using System;
using UnityEngine;

public class FollowingBackground : MonoBehaviour
{
    [SerializeField] private Rocket target;

    [SerializeField] private float smoothness = 0.3f;
    private void Awake()
    {
        if (target == null)
        {
            target = GameObject.FindFirstObjectByType<Rocket>();
        }
    }
    
    
    private void FixedUpdate()
    {
        if (target == null) return;
        transform.position += (Vector3) target.DeltaPosition * (Time.fixedDeltaTime * smoothness);
    }
}
