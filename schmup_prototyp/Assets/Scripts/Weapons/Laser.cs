using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private LineRenderer lineRenderer;
    public Transform LaserHit;
    public Transform WeaponSprite;
    public Transform EndPoint;

    public float LaserDamage;

	// Use this for initialization
	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Weapon 1"))
        {
            lineRenderer.enabled = false;
        }
        if (Input.GetButton("Weapon 2"))
        {
            lineRenderer.enabled = false;
        }
        if (Input.GetButton("Weapon 3"))
        {
            lineRenderer.enabled = false;
        }

        if (WeaponSprite.GetComponent<SpriteRenderer>().enabled == true)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);

            if(hit.collider.gameObject.tag == "Enemy")
            {
                LaserHit.position = hit.point;
                Debug.DrawLine(transform.position, hit.point);
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
            
        }
    }
}
