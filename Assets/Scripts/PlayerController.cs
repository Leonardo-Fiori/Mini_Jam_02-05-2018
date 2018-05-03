using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Controller {

    override protected void MoveDummy()
    {
        Vector3 destination = dummy.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            destination = destination + Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            destination = destination + Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            destination = destination + Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            destination = destination + Vector3.right;
        }
        if(!Input.anyKey)
        {
            destination = transform.position;
        }

        float distance = Vector3.Distance(transform.position, destination);

        if (distance > 2f)
        {
            Vector3 fromOrigin = destination - transform.position;
            fromOrigin *= 2f / distance;
            destination = transform.position + fromOrigin;
        }

        dummy.transform.position = destination;
    }
}
