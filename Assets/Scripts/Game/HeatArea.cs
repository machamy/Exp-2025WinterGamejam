using System;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class HeatArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Rocket>(out var rocket))
            {
                rocket.HeatAreaEnter();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Rocket>(out var rocket))
            {
                rocket.HeatAreaExit();
            }
        }
    }
}