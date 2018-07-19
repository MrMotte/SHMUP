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
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if (Input.GetButton("Fire4"))
        {
            timeCounter += Time.deltaTime;
            if(Time.frameCount % 40 == 0)
            Debug.Log(timeCounter);
        }
        //Debug.Log(timeCounter);
    }

    public void Attack(bool isEnemy)
    {
        // Play Sound
        // Play Anim
        if (CanAttack)
        {
            IsAttacking = true;
            Debug.Log("IasAttacking = true");
            StartCoroutine("BajonettActiveDelay");


            if (waterMode)
            {
                StartCoroutine("DelayCheckInput");
            }
        }

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
        Debug.Log("Colliding!");
        if (IsAttacking)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                HealthScript healthScript = other.gameObject.GetComponent<HealthScript>();
                healthScript.Damage(Damage);
                MoveScript moveScript = other.gameObject.GetComponent<MoveScript>();
                moveScript.StartCoroutine("Paralyze", ParalyzeTime);
            }
        }
    }

    IEnumerator BajonettActiveDelay()
    {
        yield return new WaitForSeconds(AttackTime);
        IsAttacking = false;
        Debug.Log("IsAttacking = false");
    }

    IEnumerator DelayCheckInput()
    {
        if (!shootPoisonIsRunning)
        {
            shootPoisonIsRunning = true;
            yield return new WaitForSeconds(0.2f);
            if (!Input.GetButton("Fire4"))
            {
                if (timeCounter > timeThreshold)
                {
                    ShootHarpoon();
                }
                else
                {
                    ShootPoison();
                }

                

            }
            shootPoisonIsRunning = false;
        }
    }

    void ShootPoison()
    {
        var shotTransformPoison = Instantiate(shotPrefabPoison) as Transform;
        if (ShootSound != null)
            ShootSound.Play();

        Vector3 bulletOffset = new Vector3(1.5f, 0, 0);
        shotTransformPoison.position = transform.position + bulletOffset;

        ShotScript shot = shotTransformPoison.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.isEnemyShot = false;
        }

        MoveScript move = shotTransformPoison.gameObject.GetComponent<MoveScript>();
        if (move != null)
        {
            move.direction = this.transform.right; // towards in 2D space is the right of the sprite
        }
        timeCounter = 0;
    }

    void ShootHarpoon()
    {
        var shotTransformHarpoon = Instantiate(shotPrefabHarpoon) as Transform;
        if (ShootSound != null)
            ShootSound.Play();

        Vector3 bulletOffset = new Vector3(1.5f, 0, 0);
        shotTransformHarpoon.position = transform.position + bulletOffset;

        ShotScript shot = shotTransformHarpoon.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.isEnemyShot = false;
        }

        MoveScript move = shotTransformHarpoon.gameObject.GetComponent<MoveScript>();
        if (move != null)
        {
            move.direction = this.transform.right; // towards in 2D space is the right of the sprite
        }
        timeCounter = 0;
    }



}
