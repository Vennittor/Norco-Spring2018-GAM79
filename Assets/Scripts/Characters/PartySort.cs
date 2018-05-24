using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySort : MonoBehaviour
{
    [SerializeField] private Transform slot1;
    [SerializeField] private Transform slot2;
    [SerializeField] private Transform slot3;
    [SerializeField] private PlayerCharacter crusader;
    [SerializeField] private PlayerCharacter alchemist;
    [SerializeField] private PlayerCharacter hunter;

    public void SortParty()
    {
        if (crusader.isLeader && crusader.transform.position != slot1.position)
        {
            if (crusader.transform.position == slot2.position)
            {
                if (alchemist.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(crusader, alchemist, slot1, slot2));
                }
                else if (hunter.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(crusader, hunter, slot1, slot2));
                }
            }
            else if (crusader.transform.position == slot3.position)
            {
                if (alchemist.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(crusader, alchemist, slot1, slot3));
                }
                else if (hunter.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(crusader, hunter, slot1, slot3));
                }
            }
        }
        else if (alchemist.isLeader && alchemist.transform.position != slot1.position)
        {
            if (alchemist.transform.position == slot2.position)
            {
                if (crusader.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(alchemist, crusader, slot1, slot2));
                }
                else if (hunter.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(alchemist, hunter, slot1, slot2));
                }
            }
            else if (alchemist.transform.position == slot3.position)
            {
                if (crusader.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(alchemist, crusader, slot1, slot3));
                }
                else if (hunter.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(alchemist, hunter, slot1, slot3));
                }
            }
        }
        else if (hunter.isLeader && hunter.transform.position != slot1.position)
        {
            if (hunter.transform.position == slot2.position)
            {
                if (crusader.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(hunter, crusader, slot1, slot2));
                }
                else if (alchemist.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(hunter, alchemist, slot1, slot2));
                }
            }
            else if (hunter.transform.position == slot3.position)
            {
                if (crusader.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(hunter, crusader, slot1, slot3));
                }
                else if (alchemist.transform.position == slot1.position)
                {
                    StartCoroutine(SwapChars(hunter, alchemist, slot1, slot3));
                }
            }
        }
    }

    private IEnumerator SwapChars(PlayerCharacter char1, PlayerCharacter char2, Transform slot1, Transform slot2)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / 5;
            char1.transform.position = Vector3.Lerp(char1.transform.position, slot1.position, time);
            char2.transform.position = Vector3.Lerp(char2.transform.position, slot2.position, time);
            yield return null;
        }
    }
}
