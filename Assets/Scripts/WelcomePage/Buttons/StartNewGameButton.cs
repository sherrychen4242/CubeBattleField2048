using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewGameButton : MonoBehaviour
{
    public bool onMouseHover;

    private void Start()
    {
        onMouseHover = false;
    }

    /*private void OnMouseEnter()
    {
        onMouseHover = true;
    }

    private void OnMouseOver()
    {
        onMouseHover = true;
    }

    private void OnMouseExit()
    {
        onMouseHover = false;
    }*/

    public void MouseHoverTrue()
    {
        onMouseHover = true;
    }

    public void MouseHoverFalse()
    {
        onMouseHover = false;
    }
}
