using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void Quit() // has to be public so the button can access it
    {
        Application.Quit();
    }
}
