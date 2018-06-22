using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public int Index = 3;
	public GameObject lineRend;
	public GameObject animLineRend;
	public GameObject spriteSheet;
	public GameObject LaserPrototype;

	void Update()
	{
		if(Input.GetKeyDown("up"))
		{
			if(Index == 3)
			{
				Index = 0;
			}
			else
			{
				Index++;
			}
		}
		
		if(Index == 0)
		{
			LaserPrototype.SetActive (false);
			lineRend.SetActive(true);
			animLineRend.SetActive(false);
			spriteSheet.SetActive(false);
		}
		if(Index == 1)
		{
			LaserPrototype.SetActive (false);
			spriteSheet.SetActive(true);
			animLineRend.SetActive(false);
			lineRend.SetActive(false);
		}
		if(Index == 2)
		{
			LaserPrototype.SetActive (false);
			animLineRend.SetActive(true);
			lineRend.SetActive(false);
			spriteSheet.SetActive(false);
		}

		if (Index == 3) {
		
			LaserPrototype.SetActive (true);
			animLineRend.SetActive(false);
			lineRend.SetActive(false);
			spriteSheet.SetActive(false);
		}
	}
}
