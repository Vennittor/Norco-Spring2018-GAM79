using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour {
    public void Attack(Character Attacker, Character Defender) {
        Debug.Log(Attacker.name + " attacks " + Defender.name + "!");
    }
}
