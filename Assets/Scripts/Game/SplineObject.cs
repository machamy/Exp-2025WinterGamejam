using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody2D))]
public class SplineObject : MonoBehaviour
{
    private Rigidbody2D rbody;
    
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
        rbody = GetComponent<Rigidbody2D>();
        lastPosition = rbody.position;
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
        velocity = rbody.position - lastPosition;
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
            rbody.MovePosition(splineContainer.transform.TransformPoint(targetPosition));
            // print($"spline : {splineContainer.transform.position}");
            // print($"target : {targetPosition}");
            // print($"transform : {splineContainer.transform.TransformPoint(targetPosition)}");
            // print($"other : {splineContainer.transform.position + targetPosition}");
        }
        else
        {
            rbody.MovePosition(targetPosition);
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
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime);
        rbody.MoveRotation(rbody.rotation + rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnValidate()
    {
        if (useSplinePosition)
        {
            if(splineContainer != null)
                if(splineContainer.transform != null)
                    transform.position = splineContainer.transform.position;
        }
    }
}
