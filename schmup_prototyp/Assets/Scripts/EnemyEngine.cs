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

    // Enemy Class that shoul spawn inside Formation
    public GameObject EnemyClass;
    
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

    // GemeObjects to store new Instantiated objects
    private GameObject newFormation;
    private GameObject newEnemy;

    // store Script from Instantiated Formation to Get Sort Array
    private EnemyFormation FormationScript;


    // Use this for initialization
    void Start () {

        // Start Routine that run timer for proper delay between waves
        StartCoroutine(fSpawnTimer(InitialDelay));
    }



    IEnumerator fSpawnTimer(float mInitialDelay) {
        
        // first delay to prevent instant spawn of first wave
        yield return new WaitForSeconds(mInitialDelay);


        // for each wave, spawn Formation
        for (int i = 0; i <= FormationData.Length; i++)
        {
            // iFormationCounter is used to use "i" in other functions
            iFormationCounter = i;

            // resets Spawn Transform to position of Enemy Engine
            SpawnPosition = this.transform.position;
            SpawnRotation = this.transform.rotation;

            // if Offset is used start function to adjust
            if(FormationData[i].UsePositionOffset)
                fSpawnOffset();
            
            // spawn Formation
            fSPawnFormation();
                
            // MIrror formation if required
            if(FormationData[i].MirrorFormation)
                 fMirrorFormation();

            // finaly spawn Enemys    
            fSpawnEnemy();  

            // wait for next Wave
            yield return new WaitForSeconds(FormationData[i].DelayToNextSpawn);

            // reset spawn position
            SpawnPosition = this.transform.position;
            SpawnRotation = this.transform.rotation;
        }
    }


    // simple function to Mirror Formation if required
    // executed after spawn, cause Instatiate does not have an scale option
    void fMirrorFormation() {
        SpawnRotation.x = SpawnRotation.x * -1;
        newFormation.transform.localScale = new Vector3(-1, 1, 1);
    }

    // function to spawn formation
    // executes fSpawnEnemy afterwards
    void fSPawnFormation() {
        newFormation = Instantiate(FormationData[iFormationCounter].FormationPattern, SpawnPosition, SpawnRotation) as GameObject; 
    }

    // function to edit spawn position with an offset
    // will be reseted after spawn
    void fSpawnOffset() {
        SpawnPosition = SpawnPosition + FormationData[iFormationCounter].SpawnOffset;
    }

    // spawn enemys at array locations stored inside Formation SCript
    // Formation  Script ist Stored in every Formation
    void fSpawnEnemy() {
        FormationScript = newFormation.GetComponent<EnemyFormation>();
        
        foreach(GameObject SpawnOrder in FormationScript.SpawnOrder)
        {
            Debug.Log("Enemy Spawned");
            newEnemy = Instantiate(FormationData[iFormationCounter].EnemyClass, SpawnOrder.transform.position, SpawnOrder.transform.rotation)  as GameObject;
        }

        Destroy(newFormation);
    }
}
