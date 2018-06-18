﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //for engine animation
    private Animator animatorEngine;
    private Animator animatorWeapon;

    public Vector2 speed = new Vector2(50, 50);

    [Header("Max 3 Elements!")]
    public GameObject[] Weapons;

    int CurrentWeapon = 1;
    bool WeaponIsChanging = false;

    [Header("Delay for changing weapon")]
    public float WeaponChangingDelay = 0;

    int RequestedWeapon = 1;
    SpriteRenderer WeaponSpriteRenderer;

    // Update is called once per frame

    private void Start()
    {
        animatorEngine = gameObject.transform.Find("Engine").GetComponent<Animator>();
        animatorWeapon = gameObject.transform.Find("Weapon 1").GetComponentInChildren<Animator>();

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

        Vector3 movement = new Vector3(inputX * speed.x, inputY * speed.y, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        bool shoot = Input.GetButton("Fire4");

        #region Weapon change
        if (shoot && !WeaponIsChanging)
        {
            if (CurrentWeapon == 1)
            {
                Debug.Log("Weapon 1 is shooting");
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

        if (Input.GetButtonDown("Weapon 1"))
        {
            RequestedWeapon = 1;
            //StopCoroutine(fChangeWeapon(WeaponChangingDelay));
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 2"))
        {
            RequestedWeapon = 2;
            //StopCoroutine(fChangeWeapon(WeaponChangingDelay));
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 3"))
        {
            RequestedWeapon = 3;
            //StopCoroutine(fChangeWeapon(WeaponChangingDelay));
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 4"))
        {
            RequestedWeapon = 4;
            //StopCoroutine(fChangeWeapon(WeaponChangingDelay));
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

        #endregion

        // End of the update method

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
