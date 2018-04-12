using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFinishLine : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject hitObj = collider.gameObject;

        if ( hitObj.tag == "Player")
        {
            transform.parent.gameObject.AddComponent<GameOverScript>();
        }
    }
}
