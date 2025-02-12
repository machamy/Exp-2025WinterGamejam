using System;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class GravityArea : MonoBehaviour
    {
        [SerializeField] private float gravity = 1f;
        public float Gravity => gravity;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Rocket>(out var rocket))
            {
                rocket.AddGravityArea(this);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Rocket>(out var rocket))
            {
                rocket.RemoveGravityArea(this);
            }
        }
        
        public Vector2 GetAcceleration(Vector2 position)
        {
            var direction = (Vector2)transform.position - position;
            var distance = direction.magnitude;
            var force = gravity / (distance * distance);
            return direction.normalized * force;
        }
    }
}