﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

    public int damage = 1;

    [HideInInspector]
    public bool isEnemyShot = false;

	// Use this for initialization
	void Start () {

       Destroy(gameObject, 5);
		
	}
}
