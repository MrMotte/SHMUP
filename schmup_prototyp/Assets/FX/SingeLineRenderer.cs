using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingeLineRenderer : MonoBehaviour {

	public LineRenderer line;
	
	public int Speed = 15;
	public int Distance = 15;
	
	void Start()
	{
		line = this.gameObject.GetComponent<LineRenderer>();
		line.enabled = false;
	}
	
	void Update()
	{
		if(Input.GetKeyDown("space"))
		{
			StopCoroutine("Beam");
			StartCoroutine("Beam");
		}

		if (Input.GetKeyUp ("space")) 	
		{
			line.enabled = false;
		}
	}
	
	IEnumerator Beam ()
	{
		while (Input.GetKey("space"))
		{
			//RAYCAST == ZEIT * SPEED
			line.enabled = true;
			line.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 ((Time.time * Speed), 0);
			Ray ray = new Ray (transform.position, transform.right);
			RaycastHit rayHit;

			line.SetPosition (0, ray.origin);
			if (Physics.Raycast (ray, out rayHit, Distance))
			{
				line.SetPosition(1, rayHit.point);
			}
			else
			{
				line.SetPosition (1, ray.GetPoint(Distance));
			}
			
			yield return null;
		}
	}
}
