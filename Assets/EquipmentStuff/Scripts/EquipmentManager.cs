using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {
    [Header("Prefabs")]
    public Button shopRingButton;

    //[HideInInspector]
    public List<CharClass> characters;

    [Header("Dungeon Settings")]
    public int currentGold;

    public IntRange goldDropValues;
    public int ringDropChance;
    public List<Ring> ringDropList;

    [Header("Equipment Stuff")]

    Ring currentRing;

    public List<Ring> shopRingList;

    public Text dropText;
    public GameObject equipmentMenu;

    public Text currentGoldText;

    Button currentButton;

    public GameObject shopMenu;
    public GameObject shopMenuContent;

    public void Awake()
    {
        characters = new List<CharClass>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject character in players)
        {
            characters.Add(character.GetComponent<CharClass>());
        }
    }

    public void CalcDrops()
    {
        int goldDropped = goldDropValues.Random;

        currentGold += goldDropped;
        currentGoldText.text = "" + currentGold;

        string message = goldDropped + " gold dropped";

        if (Random.value < ringDropChance / 100f && ringDropList.Count > 0)
        {
            int rand = Random.Range(0, ringDropList.Count);
            message += " and " + ringDropList[rand].name;
            RingDropped(ringDropList[rand]);
        }

        message += ".";

        dropText.text = message;
    }

    void RingDropped(Ring ring)
    {
        currentRing = ring;
        equipmentMenu.SetActive(true);
    }

    public void RingSlotChosen()
    {
        if (currentButton != null)
        {
            currentGold -= currentRing.costAtShop;
            currentGoldText.text = "" + currentGold;
            GameObject b = currentButton.gameObject;
            currentButton = null;
            Destroy(b);
        }

        if (ringDropList.Contains(currentRing)) ringDropList.Remove(currentRing);
        currentRing = null;

        equipmentMenu.SetActive(false);
    }

    public void ShopPickUpRing(Ring _ringDropped)
    {
        shopRingList.Add(_ringDropped);

        //Here is how the buttons are spawned
        Button b = Instantiate(shopRingButton, shopMenuContent.transform);

        b.GetComponentInChildren<Text>().text = _ringDropped.name + " " + _ringDropped.costAtShop;

        b.onClick.AddListener(delegate { ShopSelectRing(_ringDropped); });
        b.onClick.AddListener(delegate { RemoveShopButton(b); });
    }

    public void ShopSelectRing(Ring _triedRing)
    {
        if (currentGold >= _triedRing.costAtShop)
        {
            RingDropped(_triedRing);
        }
    }

    public void RemoveShopButton(Button button)
    {
        currentButton = button;
    }

    public void LeftRingClicked(int characterNumber)
    {
        if (characters[characterNumber].ringSlots[0] != null)
        {
            ShopPickUpRing(characters[characterNumber].ringSlots[0]);
        }
        characters[characterNumber].ringSlots[0] = currentRing;
        characters[characterNumber].RefreshStats();
        RingSlotChosen();
    }

    public void RightRingClicked(int characterNumber)
    {
        if (characters[characterNumber].ringSlots[1] != null)
        {
            ShopPickUpRing(characters[characterNumber].ringSlots[1]);
        }
        characters[characterNumber].ringSlots[1] = currentRing;
        characters[characterNumber].RefreshStats();
        RingSlotChosen();
    }

    public void CloseEquipScreen()
    {
        if (currentButton != null)
        {
            currentButton = null;
        }

        if (currentRing != null)
        {
            if (ringDropList.Contains(currentRing))
            {
                ringDropList.Remove(currentRing);
            }

            if (!shopRingList.Contains(currentRing))
            {
                ShopPickUpRing(currentRing);
            }
        }

        equipmentMenu.SetActive(false);
    }
}
