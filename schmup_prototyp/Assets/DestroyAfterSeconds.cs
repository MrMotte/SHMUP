using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {


	public float Seconds;

	// Use this for initialization
	void Start () {
		
		if(Seconds == null)
			Seconds = 1.0f;
	
		Destroy(gameObject, Seconds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
