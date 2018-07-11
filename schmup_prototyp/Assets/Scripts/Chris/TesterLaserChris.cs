using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterLaserChris : MonoBehaviour {

    private LineRenderer lineRenderer;
    public Transform LaserHit;
    public Transform WeaponSprite;
    public Transform EndPoint;

    public float LaserDamage;
	public float Speed = 15.0f;
	public float Distance = 15.0f;

	void Start ()
    {
		//Initialising
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
	}
	
	void Update ()
    {
		//Disable Input, if other Weapon is selected
		//	BEGIN
        if (Input.GetButton("Weapon 1")||Input.GetButton("Weapon 2")||Input.GetButton("Weapon 3"))
        {
            lineRenderer.enabled = false;
        }
		//	END
		
		
		//Use Input by button "Fire4"
		//	BEGIN
		if(Input.GetButtonDown("Fire4"))
		{
			StopCoroutine("Beam");
			StartCoroutine("Beam");
		}
		
		if(Input.GetButtonUp ("Fire4"))
		{
			lineRenderer.enabled = false;
		}
		//	END
		
		
		
		
		
		/*
        if (WeaponSprite.GetComponent<SpriteRenderer>().enabled == true)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);

            if(hit.collider.gameObject.tag == "Enemy")
            {
                LaserHit.position = hit.point;
                
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, LaserHit.position);
				
                if (hit)
                {
                    HealthScript health = hit.collider.GetComponent<HealthScript>();

                    if (health != null && GetComponent<LineRenderer>().enabled == true)
                    {
                        health.Damage(LaserDamage);
                    }
                }
            }
            else
            {
                LaserHit.position = new Vector3(EndPoint.position.x, LaserHit.position.y, LaserHit.position.z);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, LaserHit.position);
                if (Input.GetButton("Fire4"))
                {
                    lineRenderer.enabled = true;
                }
                else
                {
                    lineRenderer.enabled = false;
                }
            }
            
        } */
    }
	

	IEnumerator Beam()
	{
		while(Input.GetButton("Fire4"))
		{
			lineRenderer.enabled = true;
			lineRenderer.material.mainTextureOffset = new Vector2 ((-Time.time * Speed), 0);
			Ray ray = new Ray(transform.position, transform.right);
			RaycastHit rayHit;
			
			lineRenderer.SetPosition (0, ray.origin);
			if (Physics.Raycast (ray, out rayHit, Distance))
			{
				lineRenderer.SetPosition(1, rayHit.point);
			}
			else
			{
				lineRenderer.SetPosition (1, ray.GetPoint(Distance));
			}
				
			yield return null;
		}
	}
}
