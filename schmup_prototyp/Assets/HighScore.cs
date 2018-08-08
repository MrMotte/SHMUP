using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	public static int scoreValue = 0;
	Text score;

	void Start()
	{
		score = GetComponent<Text>();

		scoreValue = PlayerPrefs.GetInt("HighScore", 0);

		score.text = "Best Score: " + scoreValue;
	}

	void Update()
	{
	}
}
