using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimation : MonoBehaviour {

	//maximumLength on xAxis
	public float maxLength = 20f;
	public float drawSpeed = 5f;
	public Transform source, destination;


	Vector2 startPoint, endPoint;

	private LineRenderer rend;
	private float pointCounter;
	private float pointDistance;


	void Start(){

		startPoint = new Vector2 (source.position.x, source.position.y);
		endPoint = new Vector2 (maxLength, source.position.y);
	

		rend = GetComponent<LineRenderer>();
		rend.SetPosition (0, startPoint);
		rend.SetPosition (1, endPoint);

		//Calculat Distance between StartPoint and EndPoint
		pointDistance = Vector2.Distance(startPoint, endPoint);
	}

	void Update(){

		// -- TESTING
		//is it maxLength?
		Debug.Log (pointDistance + " ___ " + startPoint + " ___ " + endPoint);
		//-- TESTING
		if(maxLength != 0){
			if (pointCounter < pointDistance) {
			pointCounter += .1f / drawSpeed;

			float xAxis = Mathf.Lerp (0, pointDistance, pointCounter);
		
			Vector2 pointOne = startPoint;
			Vector2 pointTwo = endPoint;

			Vector2 pointDif = pointTwo - pointOne;
			pointDif.Normalize ();

			Vector2 pointInLineDirection = xAxis * pointDif + pointOne;

			rend.SetPosition (1, pointInLineDirection);
			}
		}
		
	}
}
