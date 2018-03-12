using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	#region Variables
	public EventSystem eventSystem;
    public CombatManager combatManager;
    public UIManager uIManager;

    public GameObject target; //tie in to combat system

    #endregion

    #region Functions
    public void OnPointerEnter(PointerEventData eventData)
    {
        TurnRed();
        print("hit");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TurnWhite();
        print("no hit");
    }

    public void TurnRed()
    {
        target.GetComponent<Renderer>().material.color = Color.red;
    }

    public void TurnWhite()
    {
        target.GetComponent<Renderer>().material.color = Color.white;
    }
    #endregion

}
