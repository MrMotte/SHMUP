using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSaver : MonoBehaviour {

    private static bool created = false;
	//private static 

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }
}
