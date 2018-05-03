using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "SEvent")]
public class SEvent : ScriptableObject {

    List<EventListener> listeners;

    public void Register(EventListener listener)
    {
        listeners.Add(listener);
    }

    public void Unregister(EventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

    public void Raise()
    {
        foreach(EventListener listener in listeners)
        {
            listener.OnEventRaised();
        }
    }
}
