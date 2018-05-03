using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour {
    public UnityEvent whatToDo;
    public SEvent whatToListen;

    private void OnEnable()
    {
        whatToListen.Register(this);
    }

    private void OnDisable()
    {
        whatToListen.Unregister(this);
    }

    public void OnEventRaised()
    {
        whatToDo.Invoke();
    }
}
