using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont_Destroy_On_Load : MonoBehaviour {

	public bool hangar;

	public void Awake()
     {
         DontDestroyOnLoad(this);

	          if (FindObjectsOfType(GetType()).Length > 1)
         {
             DestroyImmediate(gameObject);
         }
     }
}
