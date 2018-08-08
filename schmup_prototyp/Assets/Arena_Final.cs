using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arena_Final : MonoBehaviour
{

    private bool FadeOn = false;
	public Image FadeImg;
	public float FadeDelay;
	public float Fadespeed;
	Vector4 tempColor;
	public GameObject winScreen;

    // Use this for initialization
    void Start()
    {
	tempColor = FadeImg.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeOn)
        {
			tempColor.w += Fadespeed * Time.deltaTime;
			FadeImg.color = tempColor;
			if(tempColor.w >= 1)
				{
					FadeOn = false;
					winScreen.SetActive(true);
				}
        }
    }
    public Animator Mouth;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Mouth.SetBool("GateOpens", true);
			StartCoroutine("Fade");
			
			SoundList.soundList.Ingame_Music.Stop();
			SoundList.soundList.End_Gate_opens.Play();

        }
    }

    IEnumerator Fade()
    {

        yield return new WaitForSeconds(1);
        FadeOn = true;
		
			SoundList.soundList.Win_Music.Play();
    }
}
