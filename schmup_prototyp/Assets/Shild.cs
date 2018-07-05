using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shild : MonoBehaviour {

	public float shildHP = 10;
	public float maxShildHP;
	public float dmgRate = 0.1f;
	public float nextDMG = 0f;

	public Image Shildbar;

	void Start () 
	{
		maxShildHP = shildHP;
	}
	
	void Update () 
	{
		if (shildHP <= 0)
		{
			Destroy(this.gameObject);
		}
	}


	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Enemy" && Time.time > nextDMG)
		{
			shildHP -= 1;
			Debug.Log("?????????????????");
			Shildbar.fillAmount = (shildHP / maxShildHP);
			nextDMG = Time.time + dmgRate;
			if(collider.gameObject.GetComponent<HealthScript>().isEnemy == true);
			collider.gameObject.GetComponent<HealthScript>().hp -= 10;
		}
	}
	
}
