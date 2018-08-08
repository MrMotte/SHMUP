using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour {

    public float BlinkTime;

	public Image Anzeige;

	bool jaNein;

	public GameObject player;

	// Use this for initialization
	void Start () {
		jaNein = false;
		 BlinkTime = 0.5f;
		 Anzeige = GetComponent<Image>();
		 player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (( player.GetComponent<HealthScript>().hp <= 4) && (jaNein == false)){
			jaNein = true;
			StartCoroutine(fSpriteImmunityBlink());
		}

	}

	IEnumerator fSpriteImmunityBlink()
    {
		while(jaNein == true){
			yield return new WaitForSeconds(BlinkTime);
            Anzeige.enabled = true;

            yield return new WaitForSeconds(BlinkTime);
            Anzeige.enabled = false;
		}
    }
}
