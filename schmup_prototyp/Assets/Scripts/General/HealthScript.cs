using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{

    public float dashBackDamage = 2;
    public float hp = 2;
    public float maxHP;

    public bool isEnemy = true;
    public GameObject currencyPickup;

    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] spriteRendererChildren;
    public Image Healthbar;


    public float delay = 3;
    //= 0.32f;

    public AudioSource GotHitted;
    public AudioSource Dead;

    public BoxCollider2D EnemyBox;

    private bool Immunity = false;
    public int BlinkTimes = 10;
    public float BlinkTime;

    public float tickTime = 0.1f;
    float realTickTime;
    public Transform hitPosition;
    public ParticleSystem hitParticle;
    public GameObject destroyParticle;

    public bool healthEnabled = true;

    public float chaserCollisionDmgMul = 1;


    GameObject shild;

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(.1f);
        for (int o = 0; o < spriteRendererChildren.Length; o++)
        {
            spriteRendererChildren[o].enabled = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Chaser"))
            DyingGO();




        if (healthEnabled)
        {
            ShotScript shot = collider.gameObject.GetComponent<ShotScript>();

            if (hp <= 0)
            {
                DyingGO();
            }


            if (!Immunity)
            {
                if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Bomber" || collider.gameObject.tag == "Chaser" && shild.GetComponent<Image>().enabled == false)
                {
                    if (collider.gameObject.tag == "Chaser")
                        this.GetComponentInParent<HealthScript>().hp -= dashBackDamage * chaserCollisionDmgMul;
                    else
                        this.GetComponentInParent<HealthScript>().hp -= dashBackDamage;

                    //Debug.Log("IIIIIIIIIIIIIIIIIIIIIIIII");
                    Immunity = true;
                    StartCoroutine(fSpriteImmunityBlink());
                    if (Healthbar)
                    {
                        Healthbar.fillAmount = (hp / maxHP);
                    }
                }
                if (hp < 1)
                {
                    DyingGO();
                }
                if (shot != null)
                {
                    if (shot.isEnemyShot != isEnemy)
                    {
                        if (GotHitted != null && hp > 0)
                            GotHitted.Play();

                        hp -= shot.damage;
                        if (this.gameObject.CompareTag("Enemy") && this.transform.position.y < GameObject.FindWithTag("Player").GetComponent<PlayerScript>().Y_WaterBorder)
                            SoundList.soundList.Enemy_Damage_Water.Play();

                        if (this.gameObject.CompareTag("Enemy") && this.transform.position.y > GameObject.FindWithTag("Player").GetComponent<PlayerScript>().Y_WaterBorder)
                            SoundList.soundList.Enemy_Damage_Air.Play();

                        if (Healthbar)
                        {
                            Healthbar.fillAmount = (hp / maxHP);
                        }

                        if (hitParticle != null /*|| hitPosition != null*/)
                            //activate first time --> PS play on awake
                            if (!hitParticle.gameObject.activeSelf)
                            {
                                hitParticle.gameObject.SetActive(true);
                            }
                            else
                            {
                                //reset PS
                                hitParticle.Clear();
                                hitParticle.Play();
                            }
                        if (!isEnemy)
                        {
                            //CHRISTIAN:
                            //	PLAY Damage FX
                            //		BEGIN

                            if (shot.gameObject.layer != 8)
                                Destroy(shot.gameObject);


                            //Instantiate(hitParticle, hitPosition);
                            //		END

                            Immunity = true;
                            StartCoroutine(fSpriteImmunityBlink());
                        }
                        if (!shot.gameObject.CompareTag("AOE"))
                        {
                            Destroy(shot.gameObject);
                        }
                        if (hp < 1)
                        {
                            DyingGO();
                        }


                    }
                }
            }
            else
            {
                if (shot != null)
                {
                    if (shot.isEnemyShot != isEnemy)
                    {
                        if (!shot.gameObject.CompareTag("AOE"))
                        {
                            Destroy(shot.gameObject);
                        }
                    }
                }
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            tickTime -= Time.deltaTime;

            if (tickTime <= 0)
            {
                hp -= 1;
                if (Healthbar)
                {
                    Healthbar.fillAmount = (hp / maxHP);
                }
                if (hp < 1)
                {
                    DyingGO();
                }
                tickTime = realTickTime;
            }
        }
    }

    // If we just got hitted enable/disable spriterenderer and prevent to get hitted again
    IEnumerator fSpriteImmunityBlink()
    {
        Debug.Log("Blink startet");
        for (int i = 1; i <= BlinkTimes; i++)
        {
            yield return new WaitForSeconds(BlinkTime);
            for (int c = 0; c < spriteRendererChildren.Length; c++)
            {
                spriteRendererChildren[c].enabled = false;
                Debug.Log("Blink startet");
            }

            //foreach (SpriteRenderer mSpriteRenderer in spriteRendererChildren)
            //  mSpriteRenderer.enabled = false;

            yield return new WaitForSeconds(BlinkTime);
            for (int c = 0; c < spriteRendererChildren.Length; c++)
            {
                spriteRendererChildren[c].enabled = true;
            }


            //foreach (SpriteRenderer mSpriteRenderer in spriteRendererChildren)
            //  mSpriteRenderer.enabled = true;
        }
        Immunity = false;
    }

    // Use this for initialization
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
                                                         //if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
                                                         //  spriteRenderer.sprite = sprite1; // set the sprite to sprite1

        spriteRendererChildren = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        maxHP = hp;

        shild = GameObject.Find("Shild");
        realTickTime = tickTime;
    }

    void ChangeTheDamnSprite()
    {
        if (spriteRenderer.sprite == sprite1) // if the spriteRenderer sprite = sprite1 then change to sprite2
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1; // otherwise change it back to sprite1
        }
    }

    public void Damage(float damageAmount)
    {
        hp -= damageAmount;
        if (hp <= 0)
        {
            DyingGO();
        }
    }

    //CHRISTIAN
    //	Defining the procedure of Death for player and enemies 
    //		BEGIN
    public void DyingGO()
    {
        Debug.Log("Dying!");
        //PlayDeathSound();
        if (this.gameObject.CompareTag("Enemy") && this.transform.position.y < GameObject.FindWithTag("Player").GetComponent<PlayerScript>().Y_WaterBorder)
            SoundList.soundList.Enemy_Death_Water.Play();

        if (this.gameObject.CompareTag("Enemy") && this.transform.position.y > GameObject.FindWithTag("Player").GetComponent<PlayerScript>().Y_WaterBorder)
            SoundList.soundList.Enemy_Death_Air.Play();

        if (destroyParticle != null)
        {
            destroyParticle.SetActive(true);
            //Instantiate(destroyParticle);
        }
        /* 
                if (isEnemy)
                {
                    Instantiate(currencyPickup, this.transform.position, Quaternion.identity);
                    Score.scoreValue += 100;
                }
        */
        if (isEnemy)
        {
            ChangeTheDamnSprite(); // call method to change sprite
            Destroy(gameObject, delay);
        }

        if (!isEnemy)
        {

            StartCoroutine("DestroyPlayerObject");
        }

        Destroy(EnemyBox);
    }
    //		END

    IEnumerator DestroyPlayerObject()
    {

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        GetComponent<PlayerScript>().BeeingDestroyed();

    }
}


