using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{

    public float hp = 2;
    public float maxHP;

    public bool isEnemy = true;

    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] spriteRendererChildren;
    public Image Healthbar;

    float delay = 1.0f;
	//= 0.32f;


    public BoxCollider2D EnemyBox;

    private bool Immunity = false;
    public int BlinkTimes = 10;
    public float BlinkTime;
	
	public Transform hitPosition;
	public GameObject hitParticle;
	public GameObject destroyParticle;


    private void OnTriggerEnter2D(Collider2D collider)
    {

        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();


        if (!Immunity)
        {
            if (shot != null)
            {
                if (shot.isEnemyShot != isEnemy)
                {
                    hp -= shot.damage;
                    if(Healthbar)
                    {
                        Healthbar.fillAmount = (hp / maxHP);
                    }


                    if (!isEnemy)
                    {
						//CHRISTIAN:
						//	PLAY Damage FX
						//		BEGIN
						if(hitParticle != null || hitPosition != null)
							Instantiate(hitParticle, hitPosition);
                        //		END
						Immunity = true;
                        StartCoroutine(fSpriteImmunityBlink());
                    }

                    Destroy(shot.gameObject);

                    if (hp <= 0)
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
                    Destroy(shot.gameObject);
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

            //foreach (SpriteRenderer mSpriteRenderer in spriteRendererChildren)
              //  mSpriteRenderer.enabled = false;

            yield return new WaitForSeconds(BlinkTime);
            spriteRenderer.enabled = true;

            //foreach (SpriteRenderer mSpriteRenderer in spriteRendererChildren)
              //  mSpriteRenderer.enabled = true;
        }
        Immunity = false;
    }

    // Use this for initialization
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1; // set the sprite to sprite1

        spriteRendererChildren = GetComponentsInChildren<SpriteRenderer>();
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
	
	public void DyingGO()
	{
		Debug.Log("Dead");
		if(destroyParticle != null)
		{
			if(hitPosition != null){
				Instantiate(destroyParticle, hitPosition, true);
			}
			Instantiate(destroyParticle, this.transform, true);
		}
		
		Destroy(EnemyBox);
        ChangeTheDamnSprite(); // call method to change sprite
        Destroy(gameObject, delay);
	}
	
	void OnDestroy(){
	
	}
}
