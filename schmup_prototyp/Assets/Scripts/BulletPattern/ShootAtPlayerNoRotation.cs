using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayerNoRotation : MonoBehaviour {

     private Transform target;//set target from inspector instead of looking in Update
     public float speed = 3f;
	private Vector3 NewPos;
	public float OffsetX;
 
     void Start () {
         target = GameObject.Find("Player").transform;
		NewPos= new Vector3 (target.position.x + OffsetX, target.position.y, target.position.z);

		          //rotate to look at the player
         transform.LookAt(NewPos);
         transform.Rotate(new Vector3(0,90,0),Space.Self);//correcting the original rotation
     }
 
     void Update(){
         

         
         
         //move towards the player
         if (Vector3.Distance(transform.position,target.position)>1f){//move if distance from target is greater than 1
             transform.Translate(new Vector3(-speed* Time.deltaTime,0,0) );
         }
 
     }
}
