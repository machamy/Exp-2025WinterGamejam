
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothness = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
    }
}
