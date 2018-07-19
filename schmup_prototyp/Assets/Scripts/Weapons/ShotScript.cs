using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{

    public int damage = 1;
    public bool Paralyze = false;
    public float ParalyzeTime;

    [HideInInspector]
    public bool isEnemyShot = false;

    // Use this for initialization
    void Start()
    {

        Destroy(gameObject, 5);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding!");
        if (Paralyze)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                MoveScript moveScript = other.gameObject.GetComponent<MoveScript>();
                moveScript.StartCoroutine("Paralyze", ParalyzeTime);
            }
        }
    }
}
