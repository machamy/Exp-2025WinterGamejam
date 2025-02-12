using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class SplineObject : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    private Spline spline;
    [Header("revolution Settings")]
    [SerializeField] private bool useSplinePosition = true;
    [SerializeField] private bool moveByRatio = false;
    [FormerlySerializedAs("speed")] [SerializeField] private float revolutionSpeed = 1f;
    [FormerlySerializedAs("t")] [SerializeField] private float tRevolution = 0f;
    [SerializeField] private float cooefficient = 1f;
    [SerializeField] private bool isReversed = false;
    [Header("rotation Settings")]
    [SerializeField] private bool rotate = false;
    [SerializeField] private float rotationSpeed = 1f;
    
    private Vector2 lastPosition;
    private Vector2 velocity;
    public Vector2 Velocity => velocity;
    private void Awake()
    {
        spline = splineContainer.Spline;
        lastPosition = transform.position;
    }

    public void FixedUpdate()
    {
        Revolution();
        if(rotate)
        {
            Rotate();
        }
    }
    // 공전
    private void Revolution()
    {
        velocity = (Vector2)transform.position - lastPosition;
        Vector3 targetPosition;
        if (moveByRatio)
        {
            tRevolution += Time.fixedDeltaTime * revolutionSpeed * (isReversed ? -1 : 1);
            targetPosition = spline.EvaluatePosition(tRevolution) * cooefficient;
        }
        else
        {
            float delta = Time.fixedDeltaTime * revolutionSpeed * (isReversed ? -1 : 1);
            targetPosition = spline.GetPointAtLinearDistance(tRevolution, delta, out tRevolution) * cooefficient;
        }
        if(useSplinePosition)
        {
            transform.position = splineContainer.transform.TransformPoint(targetPosition);
        }
        if(!isReversed && tRevolution>=1f)
        {
            if(!spline.Closed)
            {
                isReversed = true;
            }
            else
            {
                tRevolution = 0f;
            }
        }
        else if(isReversed && tRevolution<=0f)
        {
            if(!spline.Closed)
            {
                isReversed = false;
            }
            else
            {
                tRevolution = 1f;
            }
        }
    }
    
    // 회전
    private void Rotate()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime);
    }
}
