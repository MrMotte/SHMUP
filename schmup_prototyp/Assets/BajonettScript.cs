using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajonettScript : MonoBehaviour
{


    public Transform shotPrefab;
    public float shootingRate = 0.25f;
    public float Damage;
    public float AttackTime;

    public AudioSource ShootSound;


    private bool IsAttacking;
    private float shootCooldown;

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
    }

    public void Attack(bool isEnemy)
    {

        // Play Sound
        // Play Anim
        IsAttacking = true;

    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (IsAttacking)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                HealthScript healthScript = other.gameObject.GetComponent<HealthScript>();
                healthScript.Damage(Damage);
            }
        }
    }

    IEnumerator BajonettActiveDelay()
    {
        yield return new WaitForSeconds(AttackTime);
        IsAttacking = false;
    }


}
