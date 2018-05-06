using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public LevelManager levelMan;
    private TransitionManager transitionMan; 

    public HeatZone heatState;
    public List<Character> partyMembers;
    public PartyType type = PartyType.ENEMY;
    
    float heatInterval = 2;
    float NextTickAt = 0;
    public uint myCurrentHeatIntensity;
    public uint lastHeat;

    public enum PartyType
    {
        PLAYER,
        ENEMY
    }
    public enum HeatZone
    {
        OutofHeat,
        InHeat
    }

    void Start()
    {
        transitionMan = FindObjectOfType<TransitionManager>(); 
        levelMan = LevelManager.Instance;

		if (levelMan == null)
		{
			Debug.LogError (this.gameObject.name + " could not find reference to LevelManager");
		}

        DontDestroyOnLoad(levelMan);

		for (int i = partyMembers.Count - 1; i >= 0; i--) 
		{
			if (partyMembers [i] == null)
			{
				partyMembers.RemoveAt(i);
			}
		}

		if (partyMembers.Count == 0)
		{
			Debug.LogError ("The " + this.gameObject.name + " has no party Memebers!");
		}
		else 
		{
			if (type == PartyType.PLAYER)
			{
				for (int i = partyMembers.Count-1; i > 0; i--)
				{
					PlayerCharacter charType = partyMembers[i].GetComponent<PlayerCharacter>();
					if (charType == null)
					{
						partyMembers.RemoveAt(i);
					}
				}

			}
			else
			{
				for (int i = partyMembers.Count - 1; i > 0; i--)
				{
					EnemyCharacter charType = partyMembers[i].GetComponent<EnemyCharacter>();
					if (charType == null)
					{
						partyMembers.RemoveAt(i);
					}
				}
			}
			foreach (Character character in partyMembers)
			{
				if (character.GetComponent<SpriteRenderer> () != null) 
				{
					character.GetComponent<SpriteRenderer>().enabled = false;
				}
				if (character.GetComponent<MeshRenderer> () != null) 
				{
					character.GetComponent<MeshRenderer>().enabled = false;
				}

				character.GetComponent<Collider>().enabled = false;
			}
		}

    }


    public void Update()
    {
		if (levelMan != null)
		{
			if (lastHeat != myCurrentHeatIntensity)
			{
				levelMan.GetHeat(myCurrentHeatIntensity);
				lastHeat = myCurrentHeatIntensity;
			}
				
			if (levelMan.combatManager.inCombat == false)
			{
				if (NextTickAt <= Time.time)
				{
					if (heatState == HeatZone.InHeat)
					{
						LevelHeatApplication();
						NextTickAt = Time.time + heatInterval;
					}
				}
			}
		}
    }

    public void IncreaseHeatRate(uint heatZoneIntensity)
    {
        myCurrentHeatIntensity += heatZoneIntensity;
        if (myCurrentHeatIntensity > 0)
        {
            heatState = HeatZone.InHeat;
        }
    }

    public void DecreaseHeatRate(uint heatZoneIntensity)
    {
        myCurrentHeatIntensity -= heatZoneIntensity;
		myCurrentHeatIntensity = (uint)Mathf.Clamp (myCurrentHeatIntensity, 0, myCurrentHeatIntensity);
        if (myCurrentHeatIntensity <= 0)
        {
            heatState = HeatZone.OutofHeat;
        }
    }

    public void LevelHeatApplication()
    {
        if (heatState == HeatZone.InHeat)
        {
            foreach (Character _char in partyMembers)
            {
                _char.ApplyDamage(myCurrentHeatIntensity, ElementType.HEAT);
            }
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (this.type == PartyType.PLAYER)
		{
			if (collision.gameObject.tag != "Enemy") //TODO change the way we check for this
			{
				return;
			}
			else if (collision.gameObject.GetComponent<Party>().type == PartyType.ENEMY)
			{
				//List<Character> enemies = new List<Character>();
				Party enemyParty = collision.gameObject.GetComponent<Party>();
				collision.gameObject.GetComponent<Collider>().enabled = false;

				StartCoroutine( levelMan.InitiateCombat(this, enemyParty) );
			}
		}

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Entrance")
        {
            transitionMan.StartCoroutine(transitionMan.TransitionIn());
            transitionMan.StartIAnim();
            levelMan.LoadScene(0);  
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Entrance" || col.gameObject.name == "Exit")
        {
            transitionMan.transitionImage.enabled = true;

            transitionMan.StartCoroutine(transitionMan.TransitionIn());
            transitionMan.StartIAnim();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Entrance" || col.gameObject.name == "Exit")
        {
            transitionMan.StartCoroutine(transitionMan.TransitionOut());
            levelMan.LoadScene(1);

            while (true)
            {
                transitionMan.StopIAnim();
                col.gameObject.SetActive(false);
                break;
            }

            transitionMan.StopCoroutine(transitionMan.TransitionOut());
        }
    }
}