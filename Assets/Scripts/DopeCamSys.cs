using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DopeCamSys : MonoBehaviour
{

    public GameObject target = null;

    public Vector3 offset = Vector3.zero;
    public float zoomSpeed = 1.0f;

    public Vector3 targetPosition = Vector3.zero;
    public float cameraSnapSpeed = 1.0f;

    public GameObject cameraDock = null;

	// Use this for initialization
	void Start ()
    {
        transform.position = cameraDock.transform.position;
        transform.LookAt(target.transform.position);

    }
	
	// Update is called once per frame
	void Update ()
    {
        //transform.LookAt(target.transform);
        transform.position = Vector3.Lerp(transform.position, cameraDock.transform.position, Time.deltaTime * cameraSnapSpeed);

        cameraDock.transform.LookAt(target.transform);
        //transform.position = Vector3.Lerp(transform.position, target.transform.position - target.transform.forward - offset, Time.deltaTime * cameraSnapSpeed);

        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            cameraDock.transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            cameraDock.transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
        }
    }
}
