using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;

	public GameObject pauseMenuUI;

	public GameObject player;

	public GameObject air_GUI;
	public GameObject water_GUI;

	public Sprite spaceON;
	public Sprite spaceOFF;
	public Image spaceAir;
	public Image spaceWater;

	void Start()
	{
		player = GameObject.Find("Player");
	}

		void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}

		if (player.transform.position.y >= player.GetComponent<PlayerScript>().Y_WaterBorder)
		{
			air_GUI.SetActive(true);
			water_GUI.SetActive(false);
		}else if (player.transform.position.y <= player.GetComponent<PlayerScript>().Y_WaterBorder)
		{
			air_GUI.SetActive(false);
			water_GUI.SetActive(true);
		}

		if(player.GetComponent<PlayerScript>().toogleBoolThree == true){
			spaceAir.sprite = spaceON;
			spaceWater.sprite = spaceON;
		}else if(player.GetComponent<PlayerScript>().toogleBoolThree == false){
			spaceAir.sprite = spaceOFF;
			spaceWater.sprite = spaceOFF;
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}
}
