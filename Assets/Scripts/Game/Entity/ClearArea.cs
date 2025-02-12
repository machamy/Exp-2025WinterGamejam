
using UnityEngine;

public class ClearArea : BaseArea
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Rocket>(out var rocket))
        {
            rocket.OnClearAreaEnter();
        }
    }
}
