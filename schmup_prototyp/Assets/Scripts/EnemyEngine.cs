using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FormationData
 {
    [SerializeField]
    public GameObject FormationPattern;
    [SerializeField]
    public float DelayToNextSpawn;
 } 

public class EnemyEngine : MonoBehaviour {

    [SerializeField]
    public FormationData[] FormationData;
    public float InitialDelay = 0;

    int FormationPatternLength = 0;


    // Use this for initialization
    void Start () {


        FormationPatternLength = FormationData.Length;
        StartCoroutine(Delay(InitialDelay));
    }

    IEnumerator Delay(float mInitialDelay) {
        yield return new WaitForSeconds(mInitialDelay);

        for (int i = 0; i <= FormationData.Length; i++)
        {
            Instantiate(FormationData[i].FormationPattern, this.transform.position, this.transform.rotation);
            Debug.Log("Spawned Object");
            yield return new WaitForSeconds(FormationData[i].DelayToNextSpawn);
            Debug.Log("Delay over");
            
        }

    }

    // Update is called once per frame
    void Update () {
		
	}


}
