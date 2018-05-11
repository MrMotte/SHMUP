using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Stage1");

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
                SceneManager.LoadScene("Menu");
            }
    }
}
