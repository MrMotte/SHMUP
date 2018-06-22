using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BeamManager : MonoBehaviour {




	public LineRenderer line1;
	public LineRenderer line2;
	public LineRenderer line3;
	public LineRenderer line4;
	public LineRenderer line5;

	float timer = 0f;


	public int Iteration = 1;
	public int maxIteration = 5;
	public int Speed = 15;
	public int Distance = 15;

	void Start () 
	{

		line1.enabled = false;
		line2.enabled = false;
		line3.enabled = false;
		line4.enabled = false;
		line5.enabled = false;
	}

	void Update ()
	{
		

		//WENN LINKE MAUSTASTE GEDRÜCKT GEHALTEN WIRD
		if (Input.GetButtonDown ("Fire1")) 
		{
			


			//STOP ALL COROUTINES -->  STARTING BY 1
			for (int j = 0; j < maxIteration; j++) 
			{
				StopCoroutine ("Beam" + j);			
			}

			StartCoroutine ("Beam" + Iteration);	
		}
		//WENN LINKE MAUSTASTE GELÖST WIRD
		if (Input.GetButtonUp ("Fire1"))
		{
			timer = 0f;

			line1.enabled = false;
			line2.enabled = false;
			line3.enabled = false;
			line4.enabled = false;
			line5.enabled = false;
		}
		//WENN RECHTE MAUSTASTE GEKLICKT WIRD
		if (Input.GetMouseButtonDown (1))
		{
			if (Iteration == maxIteration)
			{
				Iteration = 1;
			} 
			else 
			{
				Iteration += 1;
			}
		}		
	}
	
	IEnumerator Beam1 ()
	{
		while (Input.GetButton("Fire1"))
		{
			//RAYCAST == ZEIT * SPEED
			line1.enabled = true;
			line1.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 ((-Time.time * Speed), 0);
			Ray ray = new Ray (transform.position, transform.right);
			RaycastHit rayHit;

			//22:05:2018 <>
			// --
			// --
			//22:05:2018><

			line1.SetPosition (0, ray.origin);
			if (Physics.Raycast (ray, out rayHit, Distance))
			{
				line1.SetPosition(1, rayHit.point);
			}
			else
			{
				line1.SetPosition (1, ray.GetPoint(Distance));
			}
				
			yield return null;
		}
	}
	
	IEnumerator Beam2 ()
	{
		while (Input.GetButton("Fire1"))
		{
			//RAYCAST == ZEIT * SPEED
			line2.enabled = true;
			line2.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 ((-Time.time * Speed), 0);
			Ray ray = new Ray (transform.position, transform.right);
			RaycastHit rayHit;

			line2.SetPosition (0, ray.origin);
			if (Physics.Raycast (ray, out rayHit, Distance))
			{
				line2.SetPosition(1, rayHit.point);
			}
			else
			{
				line2.SetPosition (1, ray.GetPoint(Distance));
			}
			
			yield return null;
		}
	}

	IEnumerator Beam3 ()
	{
		while (Input.GetButton("Fire1"))
		{
			//RAYCAST == ZEIT * SPEED
			line3.enabled = true;
			line3.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 ((-Time.time * Speed), 0);
			Ray ray = new Ray (transform.position, transform.right);
			RaycastHit rayHit;

			line3.SetPosition (0, ray.origin);
			if (Physics.Raycast (ray, out rayHit, Distance))
			{
				line3.SetPosition(1, rayHit.point);
			}
			else
			{
				line3.SetPosition (1, ray.GetPoint(Distance));
			}
			
			yield return null;
		}
	}

	IEnumerator Beam4 ()
	{
		while (Input.GetButton("Fire1"))
		{
			//RAYCAST == ZEIT * SPEED
			line4.enabled = true;
			line4.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 ((-Time.time * Speed), 0);
			Ray ray = new Ray (transform.position, transform.right);
			RaycastHit rayHit;

			line4.SetPosition (0, ray.origin);
			if (Physics.Raycast (ray, out rayHit, Distance))
			{
				line4.SetPosition(1, rayHit.point);
			}
			else
			{
				line4.SetPosition (1, ray.GetPoint(Distance));
			}
			
			yield return null;
		}
	}	

	IEnumerator Beam5 ()
	{
		while (Input.GetButton("Fire1"))
		{
			//RAYCAST == ZEIT * SPEED
			line5.enabled = true;
			line5.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 ((-Time.time * Speed), 0);
			Ray ray = new Ray (transform.position, transform.right);
			RaycastHit rayHit;

			line5.SetPosition (0, ray.origin);
			if (Physics.Raycast (ray, out rayHit, Distance))
			{
				line5.SetPosition(1, rayHit.point);
			}
			else
			{
				line5.SetPosition (1, ray.GetPoint(Distance));
			}
			
			yield return null;
		}
	}	
}

