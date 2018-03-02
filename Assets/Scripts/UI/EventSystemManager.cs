using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour
{
	#region Variables
	public EventSystem eventSystem;

	public BaseRaycaster graphicRaycaster;

	public PointerEventData data;
	#endregion

	#region Functions
	void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			OnRaycastHit ();
		}
	}

	void OnRaycastHit ()
	{
		data = new PointerEventData(eventSystem);

		data.position = Input.mousePosition;

		List<RaycastResult> results = new List<RaycastResult>();

		graphicRaycaster.Raycast(data, results);

		//display rimlight?

		foreach (RaycastResult result in results)
		{
			print("Hit " + result.gameObject.name);
		}
	}
	#endregion

}
