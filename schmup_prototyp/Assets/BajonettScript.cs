using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BajonettScript : MonoBehaviour
{


    //public Transform shotPrefabPoison;
    public float shootingRate = 0.25f;
    public float Damage;
    public float AttackTime;
    public float ParalyzeTime;
    public float timeThreshold;

    public Transform shotPrefabPoison;
    public Transform shotPrefabHarpoon;

    public AudioSource ShootSound;
    public bool waterMode;

    private float oldTime = 0;
    private float newTime = 0;
    private float timeCounter = 0;
    private float timeDifference = 0;

    private bool IsAttacking;
    private float shootCooldown;
    private bool shootPoisonIsRunning;

    void Start()
    {
        shootCooldown = 0f;

        IsAttacking = true;
       // Debug.Log("IasAttacking = true");
       // StartCoroutine("BajonettActiveDelay");
    }

    void Update()
    {
    }

    public void Attack(bool isEnemy)
    {
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsAttacking)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                HealthScript healthScript = other.gameObject.GetComponent<HealthScript>();
                healthScript.Damage(Damage);
                //MoveScript moveScript = other.gameObject.GetComponent<MoveScript>();
                //moveScript.StartCoroutine("Paralyze", ParalyzeTime);
            }
        }
    }

    IEnumerator BajonettActiveDelay()
    {
        yield return new WaitForSeconds(100);
        IsAttacking = false;
        Debug.Log("IsAttacking = false");
    }
}
