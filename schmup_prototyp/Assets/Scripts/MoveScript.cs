using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public Vector2 speed = new Vector2(10, 10);

    public Vector2 direction = new Vector2(-1, 0);

    GameObject player;

    GameObject startTrans;

    //public GameObject shotTyp;

    void Start (){
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update () {

        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        if (transform.position.x <= player.transform.position.x && transform.name == "EnemyShootAtPlayer"){

            Quaternion rot = player.transform.rotation;
            
            transform.rotation = new Quaternion (0,200,0,0);

            direction.x = 1;

        }else if (transform.name == "Shot(Clone)"){
            
            Quaternion rot = player.transform.rotation;
            
            transform.rotation = new Quaternion (0,0,0,0);

            direction.x = 1;
            
        }else{

            Quaternion rot2 = player.transform.rotation;

            transform.rotation = new Quaternion (0,0,0,0);

            direction.x = -1;
        }
            
    }
}
