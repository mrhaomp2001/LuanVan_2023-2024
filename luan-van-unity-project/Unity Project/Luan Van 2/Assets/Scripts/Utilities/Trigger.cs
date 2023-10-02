using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [System.Serializable]
    public class TriggerContent
    {
        public string tag;
        public UnityEvent unityEventEnter;
        public UnityEvent unityEventStay;
        public UnityEvent unityEventExit;
    }

    [SerializeField] private List<TriggerContent> triggerContents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var triggerContent in triggerContents)
        {
            if (collision.CompareTag(triggerContent.tag))
            {
                triggerContent.unityEventEnter.Invoke();
                return;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var triggerContent in triggerContents)
        {
            if (collision.CompareTag(triggerContent.tag))
            {
                triggerContent.unityEventStay.Invoke();
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var triggerContent in triggerContents)
        {
            if (collision.CompareTag(triggerContent.tag))
            {
                triggerContent.unityEventExit.Invoke();
                return;
            }
        }
    }
}
