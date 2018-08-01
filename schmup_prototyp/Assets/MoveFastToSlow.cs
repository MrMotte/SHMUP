using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFastToSlow : MonoBehaviour {

	//public Vector2 speed = new Vector2(10, 10);

	public Vector2 direction = new Vector2(-1, 0);

	public float currentSpeed;

	public float initialSpeed = 10;

	public float finalSpeed = 0;

	private float t;

	void Start () 
	{
		currentSpeed = initialSpeed;
		t = 0;
	}
	
	void Update () 
	{

		t += 0.5f * Time.deltaTime;

		currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, t);

		Vector3 movement = new Vector3(currentSpeed * direction.x, currentSpeed * direction.y, 0);

		movement *= Time.deltaTime;

		transform.Translate(movement);

		
	}
}
