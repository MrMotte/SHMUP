using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour {
	
		private Vector3 startPosition;

		[Header("Wie hoch.(kleine zahl bei vielen schiffen")]
		public float height = 2.5f;

		[Header("Speed(werte zwischen -0.1 und -0.01")]
		public float xSpeed;


	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update(){
		float x = startPosition.x += xSpeed;
		float y = height*Mathf.Sin (Time.timeSinceLevelLoad)+startPosition.y;
		float z = startPosition.z;
		transform.position = new Vector3 (x,y,z);
	}
}
