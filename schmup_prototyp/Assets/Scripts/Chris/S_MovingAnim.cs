using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingAnim : MonoBehaviour {


	public float playDuration = 1f;
	public float degreePerSecond = 90f;
	public Quaternion localRot;


	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		
		rb2d = this.GetComponent<Rigidbody2D> ();	
		localRot = this.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetMouseButtonDown (0)) {
			//RotateSprite ();
			Debug.Log("MouseButton");
			localRot.z += 1f;
			transform.localRotation = localRot;
		}
		if (Input.GetButtonUp ("Fire1")) {
			localRot = transform.localRotation;
		}

	}
}
