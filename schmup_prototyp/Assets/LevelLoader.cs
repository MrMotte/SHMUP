using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;
	public Text progressText;

	public Text m_Text;

	public void LoadLevel(int sceneIndex)
	{
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}

	IEnumerator LoadAsynchronously (int sceneIndex)
	{
		yield return null;

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		//SceneManager.LoadScene("Level_1",LoadSceneMode.Additive);

		loadingScreen.SetActive(true);

		while (!operation.isDone)
		{
			progressText.text = "Loading progress: " + (operation.progress * 100) + "%";
			slider.value = operation.progress;


			if (operation.progress >= 90f)
            {
                //Change the Text to show the Scene is ready
                m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    operation.allowSceneActivation = true;
            }

			yield return null;
		}
	}
}
