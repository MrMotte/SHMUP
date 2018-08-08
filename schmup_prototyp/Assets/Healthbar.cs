using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

	public Sprite Healthbar_100;
	public Sprite Healthbar_80;
	public Sprite Healthbar_60;
	public Sprite Healthbar_40;
	public Sprite Healthbar_20;

	public Image Anzeige;

	public GameObject player;


	// Use this for initialization
	void Start () {

		player = GameObject.Find("Player");
		Anzeige.sprite = Healthbar_100;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (( player.GetComponent<HealthScript>().hp >= 12) && (player.GetComponent<HealthScript>().hp <= 17)){
			Anzeige.sprite = Healthbar_80;
		}else
		if (( player.GetComponent<HealthScript>().hp >= 8) && (player.GetComponent<HealthScript>().hp <= 13)){
			Anzeige.sprite = Healthbar_60;
		}else
		if (( player.GetComponent<HealthScript>().hp >= 4) && (player.GetComponent<HealthScript>().hp <= 9)){
			Anzeige.sprite = Healthbar_40;
		}else
		if (( player.GetComponent<HealthScript>().hp >= 0) && (player.GetComponent<HealthScript>().hp <= 5)){
			Anzeige.sprite = Healthbar_20;
		}
	}
}
