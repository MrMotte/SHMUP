using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{

    private GUISkin skin;

    void Start()
    {
        skin = Resources.Load("GUISkin") as GUISkin;
    }

    public void OnGUI()
    {
        const int buttonWidth = 120;
        const int buttonHeight = 60;

        GUI.skin = skin;

        if (
            GUI.Button(

                new Rect(
                Screen.width / 2 - (buttonWidth / 2),
                (1 * Screen.height / 3) - (buttonHeight / 2),
                buttonWidth,
                buttonHeight
                ),
                "Retry!"
                )
            )
        {
            Application.LoadLevel("Stage1");

        }


        if (
            GUI.Button(

                new Rect(
                Screen.width / 2 - (buttonWidth / 2),
                (2 * Screen.height / 3) - (buttonHeight / 2),
                buttonWidth,
                buttonHeight
                ),
                "Menu"
                )
            )
            {
                Application.LoadLevel("Menu");
            }
    }
}
