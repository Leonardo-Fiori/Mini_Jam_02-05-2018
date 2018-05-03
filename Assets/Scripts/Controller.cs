using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{

    public NavMeshAgent navmeshAgent;
    public float inertia = 50f;
    protected GameObject dummy;

    void Start()
    {
        dummy = new GameObject();
        dummy.transform.position = transform.position;
    }

    void Update()
    {
        MoveDummy();
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 destination = Vector3.Lerp(transform.position, dummy.transform.position, Time.deltaTime * inertia);
        navmeshAgent.SetDestination(destination);
    }

    virtual protected void MoveDummy() { }
}
