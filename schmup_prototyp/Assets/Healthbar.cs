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

		// FRANZI HIER DIE HP WERTE VERÄNDERN. Das unten ist mit 20 HP. Jetzt musst du einfach nur 4/5 3/5 2/5 1/5 nehmen. ( 4/5 = 16 ) ( 3/5 = 12) etc.
		// FRANZI HIER DIE HP WERTE VERÄNDERN. Das unten ist mit 20 HP. Jetzt musst du einfach nur 4/5 3/5 2/5 1/5 nehmen. ( 4/5 = 16 ) ( 3/5 = 12) etc.
		// FRANZI HIER DIE HP WERTE VERÄNDERN. Das unten ist mit 20 HP. Jetzt musst du einfach nur 4/5 3/5 2/5 1/5 nehmen. ( 4/5 = 16 ) ( 3/5 = 12) etc.

		if (( player.GetComponent<HealthScript>().hp >= 13) && (player.GetComponent<HealthScript>().hp <= 16)){
			Anzeige.sprite = Healthbar_80;
		}
		if (( player.GetComponent<HealthScript>().hp >= 9) && (player.GetComponent<HealthScript>().hp <= 12)){
			Anzeige.sprite = Healthbar_60;
		}
		if (( player.GetComponent<HealthScript>().hp >= 4) && (player.GetComponent<HealthScript>().hp <= 8)){
			Anzeige.sprite = Healthbar_40;
		}
		if (player.GetComponent<HealthScript>().hp <= 3){
			Anzeige.sprite = Healthbar_20;
		}
	}
}