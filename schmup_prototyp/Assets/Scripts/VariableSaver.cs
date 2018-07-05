using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableSaver : MonoBehaviour {

    private static bool created = false;
	public static int[] WeaponIDs = new int[] {0, 2, 4};
    public static int WeaponIDTemp;
    public static Button StoredButton;
    public static Text StoredButtonText;

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
