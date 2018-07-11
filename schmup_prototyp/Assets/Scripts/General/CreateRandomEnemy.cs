using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CreateRandomEnemy : MonoBehaviour {

    public GameObject enemyPrefab;
    public float numEnemies;
    public float xMin = 20;
    public float xMax = 85;
    public float yMin = 3.5f;
    public float yMax = -4.5f;

    // Use this for initialization
    void Start () {

        GameObject newParent = GameObject.Find("3 - Middleground - Land");

        for(int i = 0; i< numEnemies; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
            GameObject ene = Instantiate(enemyPrefab, newPos, Quaternion.identity);
            ene.transform.parent = newParent.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
