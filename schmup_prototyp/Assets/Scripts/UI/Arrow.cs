using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	private GameObject player;

	void Start () 
	{

		player = GameObject.Find("Player");

	}
	
	void Update () 
	{
		if (player.transform.position.y >= player.GetComponent<PlayerScript>().Y_WaterBorder)
		{
			if (player.GetComponent<PlayerScript>().CurrentWeapon == 1)
			{
				transform.localPosition = new Vector3(-370, -165, 0);
				transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
			else if (player.GetComponent<PlayerScript>().CurrentWeapon == 2)
			{
				transform.localPosition = new Vector3(-342, -167, 0);
				transform.localRotation = Quaternion.Euler(0, 0, 315);
			}
			else if (player.GetComponent<PlayerScript>().CurrentWeapon == 3)
			{
				transform.localPosition = new Vector3(-340, -195, 0);
				transform.localRotation = Quaternion.Euler(0, 0, 270);
			}
		}
		else if (player.transform.position.y <= player.GetComponent<PlayerScript>().Y_WaterBorder)
		{
			if (player.GetComponent<PlayerScript>().CurrentWeapon == 1)
			{
				transform.localPosition = new Vector3(-340, 195, 0);
				transform.localRotation = Quaternion.Euler(0, 0, 270);
			}
			else if (player.GetComponent<PlayerScript>().CurrentWeapon == 2)
			{
				transform.localPosition = new Vector3(-342, 167, 0);
				transform.localRotation = Quaternion.Euler(0, 0, 225);
			}
			else if (player.GetComponent<PlayerScript>().CurrentWeapon == 3)
			{
				transform.localPosition = new Vector3(-370, 165, 0);
				transform.localRotation = Quaternion.Euler(0, 0, 180);
			}
		}


	}
}
