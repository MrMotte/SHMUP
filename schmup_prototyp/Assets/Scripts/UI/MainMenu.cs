﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject hangerMenu;

	public GameObject gameManager;

	public void PlayGame ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Time.timeScale = 1f;
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}


	public void Update(){
		
	}
	public void Start()
	{
		gameManager = GameObject.Find("DontKillMe");

		if (gameManager.GetComponent<Dont_Destroy_On_Load>().hangar == true)
		{
			mainMenu.SetActive(false);
			hangerMenu.SetActive(true);
		}
	}

	public void clickedHanger(){
		gameManager.GetComponent<Dont_Destroy_On_Load>().hangar = true;
	}
}
