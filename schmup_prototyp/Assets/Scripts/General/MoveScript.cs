using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public Vector2 speed = new Vector2(10, 10);

    public Vector2 direction = new Vector2(-1, 0);

    GameObject player;

    GameObject startTrans;

    private bool Paralyzed;

    //public GameObject shotTyp;

    void Start (){
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update () {

        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);


                    
        if (transform.position.x <= player.transform.position.x && transform.name == "Enemy2Gay" || transform.position.x <= player.transform.position.x && transform.name == "EnemyShotTargetPlayerPosition")
        { //Zwischen dem " " muss der name des Enemys drinne stehen, der die Gezielten schüsse auf den player macht.

            Quaternion rot = player.transform.rotation;
            
            transform.rotation = new Quaternion (0,200,0,0);

            direction.x = 1;


        //Die namen der verschiedenen Player schüsse müssen hier richtig geschrieben drinne stehen und dahinter ein"(clone)" sein wie unten gezeigt.
        }else if (transform.name == "PS_ElectricBall_4x4(Clone)" || transform.name == "PS_ElectricBall_4x4_otherColor(Clone)" || transform.name == "LightningPistol(Clone)"){
            
            Quaternion rot = player.transform.rotation;
            
            transform.rotation = new Quaternion (0,0,0,0);

            direction.x = 1;
            
        }else if (transform.name == "EnemyShotBomb(Clone)"){

            direction.x = -1;
            direction.y = 0;
        }else{

            Quaternion rot2 = player.transform.rotation;

            transform.rotation = new Quaternion (0,0,0,0);

            //direction.x = -1;
        }
            
    }


    public IEnumerator Paralyze(float time)
    {
        Paralyzed = true;
        Vector2 StoredSpeed = speed;
        speed = new Vector2 (0,0);
        yield return new WaitForSeconds(time);
        speed = StoredSpeed;
    }
}
