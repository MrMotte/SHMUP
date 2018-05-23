using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    private GUISkin skin;

    void Start()
    {
        skin = Resources.Load("GUISkin") as GUISkin;
    }

    public void OnGUI()
    {
        const int buttonWidth = 84;
        const int buttonHeight = 60;

        GUI.skin = skin;

        if (
            GUI.Button(
                
                new Rect(
                Screen.width / 2 - (buttonWidth / 2 ),
                (1 * Screen.height / 2) - (buttonHeight / 2 ),
                buttonWidth,
                buttonHeight
                ),
                "Start!"
                )
            )
        {
            SceneManager.LoadScene("1_Level");
        }

    }

}