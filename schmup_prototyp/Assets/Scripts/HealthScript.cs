using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    public int hp = 2;

    public bool isEnemy = true;


    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here
    private SpriteRenderer spriteRenderer;

    float delay = 0.32f;


    public BoxCollider2D EnemyBox;

    private bool Immunity = false;
    public int BlinkTimes = 10;
    public float BlinkTime;


    private void OnTriggerEnter2D(Collider2D collider)
    {

        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();


        if (!Immunity)
        {
            if (shot != null)
            {
                if (shot.isEnemyShot != isEnemy)
                {
                    Immunity = true;
                    hp -= shot.damage;

                    if (!isEnemy)
                    StartCoroutine(fSpriteImmunityBlink());

                    Destroy(shot.gameObject);

                    if (hp <= 0)
                    {
                        Destroy(EnemyBox);
                        ChangeTheDamnSprite(); // call method to change sprite
                        Destroy(gameObject, delay);
                    }
                }
            }
        }

    }

    // If we just got hitted enable/disable spriterenderer and prevent to get hitted again
    IEnumerator fSpriteImmunityBlink()
    {
        for (int i = 1; i <= BlinkTimes; i++)
        {
            yield return new WaitForSeconds(BlinkTime);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(BlinkTime);
            spriteRenderer.enabled = true;
        }
        Immunity = false;
    }

    // Use this for initialization
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1; // set the sprite to sprite1


    }

    // Update is called once per frame
    void Update()
    {

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
}
