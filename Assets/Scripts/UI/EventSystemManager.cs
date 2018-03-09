using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	#region Variables
	public EventSystem eventSystem;

    public GameObject target; //tie in to combat system

    #endregion

    #region Functions
    public void OnPointerEnter(PointerEventData eventData)
    {
        target.GetComponent<Renderer>().material.color = Color.red;
        print("hit");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        target.GetComponent<Renderer>().material.color = Color.white;
        print("no hit");
    }

    #endregion

}
