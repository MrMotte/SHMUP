using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour {

	public float moveSpeed = 7f;

	Rigidbody2D rb;

	GameObject target;

	Vector2 moveDirection;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		target = GameObject.Find("Player");
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
		Destroy(gameObject,3f);
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col) {
		
		if (col.gameObject.name.Equals ("Player")){
			Destroy (gameObject);
		}
		
	}

	void Update(){

		if (transform.position.x <= target.transform.position.x){

            Quaternion rot = target.transform.rotation;
            
            transform.rotation = new Quaternion (0,200,0,0);

            moveDirection.x = 1;

        }else{

            Quaternion rot2 = target.transform.rotation;

            transform.rotation = new Quaternion (0,0,0,0);

            moveDirection.x = -1;
        }
	}
	
}
