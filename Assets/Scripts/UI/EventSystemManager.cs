using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
	#region Variables
	public EventSystem eventSystem;
    private static EventSystemManager eventInstance;
    public UIManager uIManager;
    public CombatManager combatManager;

    public Camera mainCamera;

    private Character previousHitCharacter;
    #endregion

    public static EventSystemManager Instance
    {
        get
        {
            if (eventInstance == null)
            {
                eventInstance = new EventSystemManager();
            }
            return eventInstance;
        }
    }

    void Awake()
    {
        if (eventInstance != null && eventInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        eventInstance = this;

		this.gameObject.transform.SetParent (this.gameObject.transform);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        uIManager = UIManager.Instance;
        combatManager = CombatManager.Instance;

		if (mainCamera == null) 
		{
			mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		}
    }

}
