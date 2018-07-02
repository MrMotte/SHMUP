using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[System.Serializable]
public struct Enemies
{
	public GameObject Enemy;
	public List<Component> EnemyScriptsList;
}


[InitializeOnLoad]
public class PrefabManager : MonoBehaviour {


	public Enemies[] EnemyArray;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnValidate()
	{
		for(int i = 0; i < EnemyArray.Length; i++)
		{
			EnemyArray[i].Enemy.gameObject.GetComponents(typeof(MonoBehaviour), EnemyArray[i].EnemyScriptsList);
		}
	}
}
