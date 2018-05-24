using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour {
	
		private Vector3 startPosition;
		public float xSpeed;
		public float height = 2.5f;


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
