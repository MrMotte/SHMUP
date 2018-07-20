using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMoney : MonoBehaviour {

	Currency script;

	public int addAmount;

	void Start()
	{
				script = GameObject.FindWithTag("GameController").GetComponent<Currency>();
	}

	private void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject.tag == "Player")
		{
			script.gold += addAmount;
			Destroy(gameObject);
		}
	}
}
