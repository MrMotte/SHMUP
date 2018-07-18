using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajonettScript : MonoBehaviour
{


    //public Transform shotPrefab;
    public float shootingRate = 0.25f;
    public float Damage;
    public float AttackTime;
    public float ParalyzeTime;

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
        Debug.Log("IasAttacking = true");
        StartCoroutine("BajonettActiveDelay");

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
                Debug.Log("Hitted!");
            }
        }
    }

    IEnumerator BajonettActiveDelay()
    {
        yield return new WaitForSeconds(AttackTime);
        IsAttacking = false;
        Debug.Log("IsAttacking = false");
    }


}
