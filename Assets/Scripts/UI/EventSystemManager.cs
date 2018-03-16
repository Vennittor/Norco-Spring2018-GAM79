using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	#region Variables
	public EventSystem eventSystem;
    //public CombatManager combatManager;
    //private static EventSystemManager eventInstance;
    public UIManager uIManager;

    //public List<Character> targets; //tie in to combat system

    #endregion

    #region Functions
    //public static EventSystemManager Instance
    //{
    //    get
    //    {
    //        if (eventInstance == null)
    //        {
    //            eventInstance = new EventSystemManager();
    //        }
    //        return eventInstance;
    //    }
    //}

    //void Awake()
    //{
    //    if (eventInstance != null && eventInstance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    eventInstance = this;
    //    DontDestroyOnLoad(gameObject);
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        uIManager.TurnRed();
        print("hit");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uIManager.TurnWhite();
        print("no hit");
    }

    void FixedUpdate()
    {
        if (uIManager.state == UIManager.ActiveState.TARGETING)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
            {
                uIManager.TurnRed();

            }

        }

    }

    //public void TurnRed()
    //{
    //    foreach (Character character in targets)
    //    {
    //        // check if "sometargetvariable" == "one" or "all"
    //        character.GetComponent<Renderer>().material.color = Color.red;
    //    }

    //}

    //public void TurnWhite()
    //{
    //    foreach (Character character in targets)
    //    {
    //        character.GetComponent<Renderer>().material.color = Color.white;
    //    }
    //}

    //public void AcceptTargets(List<Character> targets)
    //{
    //    this.targets = targets;
    //    uIManager.SetMode_Targeting();
    //}
    #endregion

}
