
using System;
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

    private bool isVisible = false;
    public bool IsVisible => isVisible;
    public event Action<ClearArea> OnBecameVisibleEvent;
    public event Action<ClearArea> OnBecameInvisibleEvent; 

    private void OnBecameVisible()
    {
        isVisible = true;
        OnBecameVisibleEvent?.Invoke(this);
    }
    
    private void OnBecameInvisible()
    {
        isVisible = false;
        OnBecameInvisibleEvent?.Invoke(this);
    }
}
