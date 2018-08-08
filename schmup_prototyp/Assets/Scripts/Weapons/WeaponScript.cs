using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;

	public float bulletXoffset = 0.0f;
	public float bulletYoffset = 0.0f;
	
    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;
    public bool randomRange = false;
    public float rangeMin;
    public float rangeMax;

    public bool shootVolley;
    public int volleyAmount;
    public float volleyTimeDif;
    private int volleyCounter;

    private bool mIsEnemy;

    public AudioSource ShootSound;
    public Animator enemyAnimator;

    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

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

        if (randomRange)
        {

            Quaternion target = Quaternion.Euler(0, 0, Random.Range(rangeMin, rangeMax));
            this.transform.rotation = target;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            //mIsEnemy = isEnemy;
            StartCoroutine("ShootBullet", isEnemy);
            if (gameObject.tag == "Bomber" && enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("BomberAttack");

                Debug.Log("AnimationFired!");
            }
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }



    IEnumerator ShootBullet(bool isEnemy)
    {
        volleyCounter = volleyAmount;
        do
        {

            shootCooldown = shootingRate;
            // Create a new shot

            var shotTransform = Instantiate(shotPrefab) as Transform;

            if (gameObject.CompareTag("Air"))
                SoundList.soundList.Weapon_Lightning_Pistol_Shot_Air.Play();

            if (gameObject.CompareTag("Water") && !SoundList.soundList.Weapon_Lightning_Pistol_Shot_Water.isPlaying)
                SoundList.soundList.Weapon_Lightning_Pistol_Shot_Water.Play();


            // Assign position
            if (isEnemy)
            {
				if(bulletXoffset != 0.0f || bulletYoffset != 0.0f)
				{
					Vector3 bulletOffset = new Vector3((-1 + bulletXoffset), (0 + bulletYoffset), 0);
					shotTransform.position = transform.position + bulletOffset;
				}
				else
				{
					Vector3 bulletOffset = new Vector3(-1, 0, 0);
					shotTransform.position = transform.position + bulletOffset;
				}
            }
            else
            {
                Vector3 bulletOffset = new Vector3(1.5f, 0, 0);
                shotTransform.position = transform.position + bulletOffset;
                if (randomRange)
                    shotTransform.rotation = shotTransform.rotation * Quaternion.Euler(0, 0, Random.Range(-100, 100));
            }


            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }

            volleyCounter--;
            yield return new WaitForSeconds(volleyTimeDif);
        } while (shootVolley && volleyCounter > 0);
    }
}
