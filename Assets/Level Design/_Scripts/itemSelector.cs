using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemSelector : MonoBehaviour
{
    public Image imageSelect;
    public List<Sprite> list = new List<Sprite>();
    private int itemLoc = 0;

    public void RightArrow()
    {
        if(itemLoc < list.Count -1)
        {
            itemLoc++;
            imageSelect.sprite = list[itemLoc];
        }
    }

    public void LeftArrow()
    {
        if (itemLoc > 0)
        {
            itemLoc--;
            imageSelect.sprite = list[itemLoc];
        }
    }

    public void NavigationInventory()
    {

    }
}