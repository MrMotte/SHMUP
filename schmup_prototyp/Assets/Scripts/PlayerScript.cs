﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //for engine animation
    private Animator animatorEngine;
    private Animator animatorWeapon;

    bool boost = false;

    public float nextDMGPlayer = 0.1f;
    public float dashSpeed = 10f;

    public float dashDuration = 5;


    public float cooldownTime = 5;

    public float dashDamage = 2;

    // Movement
    public Vector2 speed = new Vector2(50, 50);
    public Vector2 oldSpeed = new Vector2(50, 50);


    // Weapons
    [Header("First Half Air, second Water Weapons. Array should always be divisible by 2!")]
    public GameObject[] Weapons;
    public int CurrentWeapon = 1;
    bool WeaponIsChanging = false;
    [Header("Delay for changing weapon")]
    public float WeaponChangingDelay = 0;
    int RequestedWeapon = 1;
    SpriteRenderer WeaponSpriteRenderer;

    // Air - Water Switch
    public float Y_WaterBorder = 0;
    private bool IsPlayerUnderwater = false;

    GameObject shild;
    bool toogleBool = false;
    bool toogleBoolTwo = false;
    bool toogleBoolThree = true;



    float dmgRatePlayer = 0;


    // Update is called once per frame

    private void Start()
    {
        animatorEngine = gameObject.transform.Find("Engine").GetComponent<Animator>();
        animatorWeapon = gameObject.transform.Find("Weapon 1").GetComponentInChildren<Animator>();

        shild = GameObject.Find("Shild");

        oldSpeed.x = speed.x;
        oldSpeed.y = speed.y;

        //GetComponentInChildren<Animator>();

    }

    void Update()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        //checks if moving up, for animation, sets bool
        if (inputY > 0)
        {
            //Debug.Log("über 0")
            animatorEngine.SetBool("IsMovingUp", true);
        }
        else
        {
            animatorEngine.SetBool("IsMovingUp", false);
        }

        // detect if player is currently underwater
        if (this.transform.position.y > Y_WaterBorder)
        {
            if (!IsPlayerUnderwater)
            {
                CurrentWeapon = CurrentWeapon + Weapons.Length / 2;
                // Call VFX, GUI and Sound
            }
            IsPlayerUnderwater = true;  
        }
        else
        {
            if (IsPlayerUnderwater)
            {
                CurrentWeapon = CurrentWeapon - Weapons.Length / 2;
                // Call VFX, GUI and Sound
            }
            IsPlayerUnderwater = false;
        }

        Vector3 movement = new Vector3(inputX * speed.x, inputY * speed.y, 0);

        if(Input.GetButtonDown("EngineDasher") && toogleBoolThree){
            StartCoroutine(boostON(0));
        }
        if(boost == false){
                speed.x = oldSpeed.x;
                speed.y = oldSpeed.y;
            }else if (boost == true && toogleBoolTwo == true){
                speed.x += dashSpeed;
                speed.y += dashSpeed;
                toogleBoolTwo = false;
            }
        

        movement *= Time.deltaTime;

        transform.Translate(movement);

        bool shoot = Input.GetButton("Fire4");

        #region Weapon change
        if (shoot && !WeaponIsChanging)
        {
            if (CurrentWeapon == 1)
            {
                //Debug.Log("Weapon 1 is shooting");
                //Animation Weapon 1 start
                animatorWeapon.SetBool("IsWeaponShooting", true);
            }

            fShoot(CurrentWeapon);
        }
        else
        {
            //Animation Weapon 1 stops
            animatorWeapon.SetBool("IsWeaponShooting", false);
        }

        if (Input.GetButton("Weapon 1"))
        {
            if (IsPlayerUnderwater)
            { RequestedWeapon = 1 + Weapons.Length / 2; }
            else
                RequestedWeapon = 1;
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 2"))
        {
            if (IsPlayerUnderwater)
            { RequestedWeapon = 2 + Weapons.Length / 2; }
            else
                RequestedWeapon = 2;
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 3"))
        {
            if (IsPlayerUnderwater)
            { RequestedWeapon = 3 + Weapons.Length / 2; }
            else
                RequestedWeapon = 3;
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 4"))
        {
            if (IsPlayerUnderwater)
            { RequestedWeapon = 4 + Weapons.Length / 2; }
            else
                RequestedWeapon = 4;
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        #endregion
        // ...

        // 6 - Make sure we are not outside the camera bounds
        #region camera bounds 
        
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );


        if(Input.GetButtonDown("ShildBatteringRam"))
        {
            toogleBool = !toogleBool;
            shild.GetComponent<Image>().enabled = toogleBool;
            shild.GetComponent<EdgeCollider2D>().enabled = toogleBool;
        }
        

        #endregion

        // End of the update method

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy"){

            if(boost == true && Time.time > nextDMGPlayer){
                collider.gameObject.GetComponent<HealthScript>().hp -= dashDamage;
                nextDMGPlayer = Time.time + dmgRatePlayer;
                Debug.Log("AAAAAAAAAAAAAAAAAHHHHHHHHHHHHH");
                }
        }
    }

    void OnDestroy()
    {
        transform.parent.gameObject.AddComponent<GameOverScript>();
    }

    void fShoot(int mCurrentWeapon)
    {
        WeaponScript weapon = Weapons[mCurrentWeapon - 1].GetComponent<WeaponScript>();
        if (weapon != null)
        {
            weapon.Attack(false);
        }
    }

    IEnumerator boostON(float duration){
        duration = dashDuration;
        toogleBoolThree = false;
        boost = true;
        toogleBoolTwo = true;
        yield return new WaitForSeconds(duration);
        boost = false;
        yield return new WaitForSeconds(cooldownTime);
        toogleBoolThree = true;
    }

    IEnumerator fChangeWeapon(float mWeaponChangingDelay)
    {
        if (CurrentWeapon != RequestedWeapon)
        {
            WeaponIsChanging = true;
            yield return new WaitForSeconds(mWeaponChangingDelay);

            WeaponSpriteRenderer = Weapons[CurrentWeapon - 1].GetComponentInChildren<SpriteRenderer>();
            WeaponSpriteRenderer.enabled = false;
            if (RequestedWeapon == 1)
            {
                CurrentWeapon = 1;
                WeaponSpriteRenderer = Weapons[CurrentWeapon - 1].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = true;
            }

            if (RequestedWeapon == 2)
            {
                CurrentWeapon = 2;
                WeaponSpriteRenderer = Weapons[CurrentWeapon - 1].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = true;
            }

            if (RequestedWeapon == 3)
            {
                CurrentWeapon = 3;
                WeaponSpriteRenderer = Weapons[CurrentWeapon - 1].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = true;
            }
            if (RequestedWeapon == 4)
            {
                CurrentWeapon = 4;
                WeaponSpriteRenderer = Weapons[CurrentWeapon - 1].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = true;
            }

            Debug.Log("Changed Weapon to: " + CurrentWeapon);
            WeaponIsChanging = false;
        }

    }
}
