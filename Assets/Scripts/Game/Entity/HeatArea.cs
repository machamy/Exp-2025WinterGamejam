using System;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class HeatArea : BaseArea
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Rocket>(out var rocket))
            {
                rocket.OnHeatAreaEnter(this);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Rocket>(out var rocket))
            {
                rocket.OnHeatAreaExit(this);
            }
        }
    }
}