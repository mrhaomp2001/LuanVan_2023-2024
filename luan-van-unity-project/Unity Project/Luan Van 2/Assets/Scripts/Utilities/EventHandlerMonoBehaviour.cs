using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandlerMonoBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent unityEvent;

    public void FireEvents()
    {
        unityEvent.Invoke();
    }
}
