using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Holds data for every enemy wave
// this struct is later used as an array
[System.Serializable]
public struct FormationData 
{
    // slot to place formation prefab
    [SerializeField]
    public GameObject FormationPattern;
    
    // Delay after wave till next one is spawned
    [SerializeField]
    public float DelayToNextSpawn;

    // bool that offer the Mirror function
    public bool MirrorFormation;

    // bool that offer the spawn position offset function
    public bool UsePositionOffset;

    // adds an vec3 to our normal spawn position
    public Vector3 SpawnOffset;
 } 

public class EnemyEngine : MonoBehaviour {

    // init struct as array so we can set new variables for every wave
    // this should work like an wave manager, where franzi can simply adjust every part of spawning an enemy
    [SerializeField]
    public FormationData[] FormationData;

    // Delay that starts first to offer some time for intruduction before game (and enemy waves) begins
    public float InitialDelay = 0;

    // store "i" from 
    private int iFormationCounter = 0;

    // Rotation and Location of that point where the formation will be spawned
    private Quaternion SpawnRotation;
    private Vector3 SpawnPosition;

    private GameObject newObject;

    private EnemyFormation FormationScript;


    // Use this for initialization
    void Start () {

        // Start Routine that run timer for proper delay between waves
        StartCoroutine(fSpawnTimer(InitialDelay));
    }


    IEnumerator fSpawnTimer(float mInitialDelay) {
        for (int i = 0; i <= FormationData.Length; i++)
        {
            //decides which spaw method is used
            if(FormationData[i].UsePositionOffset)
                fSpawnOffset();
            
                fSPawnFormation();
                if(FormationData[i].MirrorFormation)
                    fMirrorFormation();
            
            iFormationCounter = i;
            yield return new WaitForSeconds(FormationData[i].DelayToNextSpawn);

            // reset spawn position
            SpawnPosition = this.transform.position;
            SpawnRotation = this.transform.rotation;
        }
    }


    // simple function to Mirror Formation if required
    void fMirrorFormation() {
        SpawnRotation.x = SpawnRotation.x * -1;
        newObject.transform.localScale = new Vector3(-1, 1, 1);
    }

    void fSPawnFormation() {
        newObject = Instantiate(FormationData[iFormationCounter].FormationPattern, SpawnPosition, SpawnRotation) as GameObject; 
        fSpawnEnemy();  
    }

    void fSpawnOffset() {
        SpawnPosition = SpawnPosition + FormationData[iFormationCounter].SpawnOffset;
    }

    void fSpawnEnemy() {
        FormationScript = newObject.GetComponent("EnemyFormation") as EnemyFormation;
    }
}
