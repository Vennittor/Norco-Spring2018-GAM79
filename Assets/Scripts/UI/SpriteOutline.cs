using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOutline : MonoBehaviour
{
    #region Variables
    public Material material;
    public RaycastHit2D rayHit;

    public bool clicked;
    #endregion

    #region Functions
    void Start ()
    {
        material = GetComponent<Renderer>().material;
        //rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    void OnMouseOver()
    {
        material.color = Color.red;

        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
        }
             
    }

    void OnMouseExit()
    {
        if (clicked)
        {
            material.color = Color.red;
        }
 
        else
        {
            material.color = Color.white;
        }

    }
    #endregion
}
