﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkParticle : MonoBehaviour
{
    public ParticleSystem walkParticle;
    public float duration;

    public void StartParticle()
    {
        ParticleSystem thing = Instantiate(walkParticle, transform.position+(Vector3.down*3.2f)+(Vector3.right*4), transform.rotation);
        thing.Play();
        StartCoroutine(KillMe(thing, duration));
    }

    public IEnumerator KillMe(ParticleSystem toDie, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(toDie.gameObject);
    }
}
