using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UltEvents;
using UnityEngine;

[System.Serializable]
public class TagEvent
{
    public string tagName;
    public UltEvent Event;
}
[RequireComponent(typeof(Collider))]
public class CollissionEvent : MonoBehaviour
{
    public List<TagEvent> TriggerEnterEvents, TriggerExitEvents, ColEnterEvents, ColExitEvents;
    void OnCollisionEnter(Collision other) => FindAndExecuteEvent(other.transform.tag, ColEnterEvents);
    void OnCollisionExit(Collision other) => FindAndExecuteEvent(other.transform.tag, ColExitEvents);
    void OnTriggerEnter(Collider other) => FindAndExecuteEvent(other.transform.tag, TriggerEnterEvents);
    void OnTriggerExit(Collider other) => FindAndExecuteEvent(other.transform.tag, TriggerEnterEvents);
    void OnCollisionEnter2D(Collision2D other) => FindAndExecuteEvent(other.transform.tag, ColEnterEvents);
    void OnCollisionExit2D(Collision2D other) => FindAndExecuteEvent(other.transform.tag, ColExitEvents);
    void OnTriggerEnter2D(Collider2D other) => FindAndExecuteEvent(other.transform.tag, TriggerEnterEvents);
    void OnTriggerExit2D(Collider2D other) => FindAndExecuteEvent(other.transform.tag, TriggerEnterEvents);
    void FindAndExecuteEvent(string tag, List<TagEvent> events) => events.Find(e => e.tagName == tag).Event.Invoke();
}
