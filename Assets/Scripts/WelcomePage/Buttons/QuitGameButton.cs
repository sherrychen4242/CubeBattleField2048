using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public bool onMouseHover;

    public void MouseHoverTrue()
    {
        onMouseHover = true;
    }

    public void MouseHoverFalse()
    {
        onMouseHover = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
