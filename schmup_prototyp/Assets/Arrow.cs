using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public GameObject player;
	public float waterBorder;

	void Start () 
	{
		waterBorder = player.GetComponent<PlayerScript>().Y_WaterBorder;
	}
	
	void Update () 
	{
		
	}
}
