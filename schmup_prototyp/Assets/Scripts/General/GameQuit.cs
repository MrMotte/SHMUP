using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        if (Input.GetKey("escape"))
        {
            Debug.Log("Game quit.");
			
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
        }
    }
}
