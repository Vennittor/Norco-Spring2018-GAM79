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

    public Transform cameraDock = null;
    public new Camera camera; 


	// Use this for initialization
	void Start ()
    {
        camera = FindObjectOfType<Camera>(); 
		if (target == null) 
		{
			target = GameObject.FindGameObjectWithTag ("Player");
		}

		if (cameraDock == null && target != null) 
		{
			cameraDock = target.transform.Find ("CamDock");
		}

		transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = cameraDock.transform.position;
        transform.LookAt(target.transform.position);
    }

    public void Reposition()
    {
        //TODO change camera positoin to encompass all characters in within frustum
        camera.fieldOfView = 70;
        cameraDock = target.transform.Find("CamDock");
    }

    public void RepositionCameraToOriginalPosition()
    {
        //TODO set to default position
        camera.transform.position = cameraDock.transform.position;
        camera.fieldOfView -= 10;
        cameraDock = target.transform.Find("CamDock"); 
    }

	public void SetTargetPosition(Vector3 newTargetPosition)
	{
		targetPosition = newTargetPosition;
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
