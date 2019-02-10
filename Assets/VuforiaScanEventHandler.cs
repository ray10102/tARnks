using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VuforiaScanEventHandler : DefaultTrackableEventHandler
{
    [SerializeField] private UnityEvent onFound;

    [SerializeField] private UnityEvent onLost;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        onFound.Invoke();
    }
    
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        onLost.Invoke();
    }
}
