using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealthScript : MonoBehaviour
{


    public float hp = 2;
    public float maxHP;

    public bool isEnemy = true;

    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here
    private SpriteRenderer spriteRenderer;

    public CircleCollider2D EnemyBox;

    public Transform hitPosition;
    public GameObject hitParticle;
    public GameObject destroyParticle;

    public GameObject Shield;


    private void OnTriggerEnter2D(Collider2D collider)
    {

        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();



        if (shot != null)
        {
            if (shot.isEnemyShot != isEnemy)
            {
                hp -= shot.damage;



                if (!isEnemy)
                {
                    //CHRISTIAN:
                    //	PLAY Damage FX
                    //		BEGIN

                    if (hitParticle != null || hitPosition != null)
                        Instantiate(hitParticle, hitPosition);
                    //		END
                }

                Destroy(shot.gameObject);

                if (hp < 1)
                {
                    DyingGO();
                }
            }
        }

        else
        {
            if (shot != null)
            {
                if (shot.isEnemyShot != isEnemy)
                {
                    Destroy(shot.gameObject);
                }
            }
        }

    }


    // Use this for initialization
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1; // set the sprite to sprite1

        maxHP = hp;

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

        if (destroyParticle != null)
        {
            Instantiate(destroyParticle, transform.position, transform.rotation);
        }

        if (isEnemy)
        {
            ChangeTheDamnSprite(); // call method to change sprite
        }

        Destroy(EnemyBox);
		Shield.gameObject.SetActive(false);
        
    }
    //		END

    void OnDestroy()
    {

    }
}
