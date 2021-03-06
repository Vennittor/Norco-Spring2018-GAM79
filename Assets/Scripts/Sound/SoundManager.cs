﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public GameObject audioItemSFX;
    public GameObject audioItemVFX;
    public GameObject audioItemMXlevel;
    public GameObject audioItemMXcombat;

    //snapshots
    public AudioMixerSnapshot noLevel;
    public AudioMixerSnapshot noCombat;

    private GameObject prefabBus;
    private GameObject go;

    void Awake()
    {
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}

		this.gameObject.transform.SetParent(null);

        //DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip, string bus)
    {
        if (bus == "sfx")
        {
            prefabBus = audioItemSFX;
        }
        else if (bus == "vfx")
        {
            prefabBus = audioItemVFX;
        }
        else if (bus == "mxL")
        {
            prefabBus = audioItemMXlevel;
        }
        else if (bus == "mxC")
        {
            prefabBus = audioItemMXcombat;
        }

        go = (GameObject)Instantiate(prefabBus);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = clip;
        src.Play();

        //DontDestroyOnLoad(go); 

        if (go.gameObject.GetComponent<AudioSource>().loop == false)
        {
            Destroy(go, clip.length);
        }
    }

    public void LevelToCombat()
    {
        noLevel.TransitionTo(1.0f);
    }

    public void CombatToLevel()
    {
        GameObject combatMX;
        noCombat.TransitionTo(2.0f);
        combatMX = GameObject.FindGameObjectWithTag("CombatMX");
        Destroy(combatMX, 2.1f);
    }

    public void StopAClip()
    {
        Destroy(go);
    }
}