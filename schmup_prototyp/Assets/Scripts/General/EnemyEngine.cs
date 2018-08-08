using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


// Holds data for every enemy wave
// this struct is later used as an array
[System.Serializable]
public struct FormationData
{

    [Header("Single-Pattern")]
    // slot to place formation prefab
    [SerializeField]
    public GameObject FormationPattern;

    // Enemy Class that shoul spawn inside Formation
    public GameObject EnemyClass;

    // Delay after wave till next one is spawned
    [SerializeField]
    public float XToNextSpawn;



    [Space(5)]
    [Header("Multi-Pattern")]

    public bool AdvancedFormation;

    public GameObject[] EnemyClassAdvanced;


    [Space(5)]
    [Header("Optional")]

    // bool that offer the Mirror function
    public bool MirrorFormation;

    // bool that offer the spawn position offset function
    public bool UsePositionOffset;

    // adds an vec3 to our normal spawn position
    public Vector3 SpawnOffset;


}



public class EnemyEngine : MonoBehaviour
{
    [Header("Enemy Spawn")]
    // init struct as array so we can set new variables for every wave
    // this should work like an wave manager, where franzi can simply adjust every part of spawning an enemy
    [SerializeField]
    public FormationData[] FormationData;

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

    [Header("Finishline")]
    public float FinishLineDelayAfterLastEnemy = 0;

    private GameObject[] ExistingEnemys;

    public GameObject Finishline;

    private bool test;

    private Vector3 EnemyEnginePosition;
    private Vector3 NextWavePosition;

    // Use this for initialization
    void Start()
    {
        EnemyEnginePosition = this.gameObject.transform.position;


        // Start Routine that run timer for proper delay between waves
        StartCoroutine(fSpawnTimer());
    }

    void Update()
    {
        if (Time.frameCount % 5 == 0)
        {
            EnemyEnginePosition = this.gameObject.transform.position;
        }
    }


    void OnValidate()
    {
        for (int i = 0; i < FormationData.Length; i++)
        {
            try{
            if (FormationData[i].AdvancedFormation == true && FormationData[i].EnemyClassAdvanced.Length != FormationData[i].FormationPattern.GetComponent<EnemyFormation>().SpawnOrder.Length)
            {
                FormationData[i].EnemyClassAdvanced = new GameObject[FormationData[i].FormationPattern.GetComponent<EnemyFormation>().SpawnOrder.Length];  //FormationData[i].FormationPattern.GetComponent<EnemyFormation>().SpawnOrder.Length;
            }
            else
            {

            }

            if (FormationData[i].AdvancedFormation == false)
            {
                FormationData[i].EnemyClassAdvanced = null;
            }
            }
            catch(NullReferenceException e){}
        }
    }



    IEnumerator fSpawnTimer()
    {
        NextWavePosition.x = FormationData[0].XToNextSpawn;

        // for each wave, spawn Formation
        for (int i = 0; i < FormationData.Length; i++)
        {
            if (NextWavePosition.x <= EnemyEnginePosition.x)
            {
                // iFormationCounter is used to use "i" in other functions
                iFormationCounter = i;

                // resets Spawn Transform to position of Enemy Engine
                SpawnPosition = this.transform.position;
                SpawnRotation = this.transform.rotation;

                SpawnPosition = FormationData[iFormationCounter].FormationPattern.transform.position;

                // if Offset is used start function to adjust
                if (FormationData[i].UsePositionOffset)
                {
                    fSpawnOffset();
                }
                // spawn Formation
                fSPawnFormation();

                // MIrror formation if required
                if (FormationData[i].MirrorFormation)
                    fMirrorFormation();

                // finaly spawn Enemys    
                fSpawnEnemy();

                // wait for next Wave
                NextWavePosition.x = FormationData[iFormationCounter + 1].XToNextSpawn;

                // reset spawn position
                SpawnPosition = this.transform.position;
                SpawnRotation = this.transform.rotation;
            }
            else
            {
                yield return new WaitForSeconds(.5f);
                i--;
            }
        }
        Debug.Log("Check Enemys");
        StartCoroutine(SearchForRemainingEnemys());
    }


    // simple function to Mirror Formation if required
    // executed after spawn, cause Instatiate does not have an scale option
    void fMirrorFormation()
    {
        SpawnRotation.x = SpawnRotation.x * -1;
        newFormation.transform.localScale = new Vector3(-1, 1, 1);
    }

    // function to spawn formation
    // executes fSpawnEnemy afterwards
    void fSPawnFormation()
    {
        newFormation = Instantiate(FormationData[iFormationCounter].FormationPattern, SpawnPosition, SpawnRotation) as GameObject;
        SoundList.soundList.Enemy_Formation_Appears.Play();
    }

    // function to edit spawn position with an offset
    // will be reseted after spawn
    void fSpawnOffset()
    {
        SpawnPosition = SpawnPosition + FormationData[iFormationCounter].SpawnOffset;
    }

    // spawn enemys at array locations stored inside Formation SCript
    // Formation  Script ist Stored in every Formation
    void fSpawnEnemy()
    {
        FormationScript = newFormation.GetComponent<EnemyFormation>();

        for (int i = 0; i < FormationScript.SpawnOrder.Length;)
        {
            //            Debug.Log("Enemy Spawned");
            if (FormationData[iFormationCounter].AdvancedFormation == true)
            {
                newEnemy = Instantiate(FormationData[iFormationCounter].EnemyClassAdvanced[i], FormationScript.SpawnOrder[i].transform.position, FormationScript.SpawnOrder[i].transform.rotation) as GameObject;
            }
            else
            {
                newEnemy = Instantiate(FormationData[iFormationCounter].EnemyClass, FormationScript.SpawnOrder[i].transform.position, FormationScript.SpawnOrder[i].transform.rotation) as GameObject;
            }


            newEnemy.gameObject.tag = "Enemy";
            i++;

        }

        Destroy(newFormation);
    }

    IEnumerator SearchForRemainingEnemys()
    {
        ExistingEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Search Enemy");
        while (1 > ExistingEnemys.Length)
        {
            Debug.Log("EnemyFound");
            yield return new WaitForSeconds(1);
            ExistingEnemys = null;
            ExistingEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        Debug.Log("No More Enemys!");
        yield return new WaitForSeconds(FinishLineDelayAfterLastEnemy);
        Instantiate(Finishline, SpawnPosition, SpawnRotation);

    }


}
