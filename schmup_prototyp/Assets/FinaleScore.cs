using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinaleScore : MonoBehaviour {

	Text score;


	int finaleScore;

	void Start () 
	{
		score = GetComponent<Text>();

		finaleScore = Score.scoreValue;

		score.text = "Best Score: " + finaleScore;
	}
	
	void Update () 
	{
		
	}
}
