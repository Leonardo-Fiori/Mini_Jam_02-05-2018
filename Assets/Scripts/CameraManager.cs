using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public GameObject subject1;
    public GameObject subject2;
    public float inertia = 50f;
    public float zoomMultiplier = 5f;
    private Camera cam;
    public float zoomInertia = 10f;
    public float minCameraZoom = 10f;
    public float maxCameraZoom = 15f;
    private GameObject dummy;

    private void Start()
    {
        dummy = new GameObject();
        cam = GetComponentInChildren<Camera>();
    }

    void Update () {
        Vector3 destination = subject1.transform.position + ((subject2.transform.position - subject1.transform.position) / 2);

        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * inertia);

        cam.orthographicSize = Mathf.Clamp(Mathf.Abs(Vector3.Distance(subject1.transform.position, subject2.transform.position)),minCameraZoom,maxCameraZoom);
    }
}
