using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {


	public int Seconds;

	// Use this for initialization
	void Start () {
		
		if(Seconds == null)
			Seconds = 1;
	
		Destroy(gameObject, Seconds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
