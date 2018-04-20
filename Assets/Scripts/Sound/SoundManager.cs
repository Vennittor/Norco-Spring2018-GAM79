﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public GameObject audioItemSFX;
    public GameObject audioItemVFX;
    public GameObject audioItemMX;

    private GameObject prefabBus;

    // Use this for initialization
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

        DontDestroyOnLoad(gameObject);
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
        else if (bus == "mx")
        {
            prefabBus = audioItemMX;
        }

        GameObject go = (GameObject)Instantiate(prefabBus);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = clip;
        src.Play();
        Destroy(go, clip.length);
    }
}