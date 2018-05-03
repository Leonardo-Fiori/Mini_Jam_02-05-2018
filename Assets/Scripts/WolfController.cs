using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : Controller {

    private static bool mangiato = false;
    public float tempoAttesaMangiata = 3f;

    public static bool HaAppenaMangiato()
    {
        return mangiato;
    }

    public void PecoraMangiata()
    {
        mangiato = true;
        Invoke("ResetMangiato", tempoAttesaMangiata);
    }

    private void ResetMangiato()
    {
        mangiato = false;
    }

    override protected void MoveDummy()
    {
        if (mangiato) return;

        Vector3 destination = dummy.transform.position;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            destination = destination + Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            destination = destination + Vector3.back;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            destination = destination + Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            destination = destination + Vector3.right;
        }
        if (!Input.anyKey)
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
