using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //for engine animation
    private Animator playerAnimator;

    public GameObject winScreen;
    public GameObject loseScreen;

    private float chargeTimer = 0;
    public float maxChargeTime = 1;

    public bool boost = false;

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
    public int CurrentWeapon = 0;
    bool WeaponIsChanging = false;
    [Header("Delay for changing weapon")]
    public float WeaponChangingDelay = 0;
    int RequestedWeapon = 0;
    SpriteRenderer WeaponSpriteRenderer;

    // Air - Water Switch
    public float Y_WaterBorder = 0;
    private bool IsPlayerUnderwater = false;

    GameObject shild;
    bool toogleBool = false;
    bool toogleBoolTwo = false;
    public bool toogleBoolThree = true;

    public AudioSource WeaponSwitch;
    public AudioSource EngineDash;

    float dmgRatePlayer = 0;

    //Splash, Drain, Bubbles, Thruster - Prefabs
    [Header("Movement FX  -  Air/Water Switch FX")]
    public GameObject splashGO;
    public GameObject drainGO;
    public GameObject bubbleSplashGO;

    public GameObject airThrusterGO;
    public GameObject waterThrusterGO;

    public GameObject airBoostThrusterGO;
    public GameObject waterBoostThrusterGO;
    public GameObject boostImpactGO;
    public GameObject boostWeaponGlowGO;
    // Update is called once per frame

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        shild = GameObject.Find("Shild");

        oldSpeed.x = speed.x;
        oldSpeed.y = speed.y;
        SoundList.soundList.Ingame_Music.Play();
    }

    void Update()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("EngineDasher") && toogleBoolThree)
        {
            StartCoroutine(boostON(0));
            if (EngineDash != null)
                EngineDash.Play();
        }

        if (boost == false)
        {
            speed.x = oldSpeed.x;
            speed.y = oldSpeed.y;
        }
        //Hier wird der Boost / Canonball aktiviert
        else if (boost == true && toogleBoolTwo == true)
        {
            speed.x += dashSpeed;
            speed.y += dashSpeed;
            toogleBoolTwo = false;
        }

        // detect if player is currently underwater
        if (this.transform.position.y < Y_WaterBorder)
        {
            if (!IsPlayerUnderwater && boost == false)
            {
                Weapons[CurrentWeapon].SetActive(false);
                CurrentWeapon++;
                Weapons[CurrentWeapon].SetActive(true);
            }
            StopCoroutine("Splash");
            StartCoroutine("Splash");

            if (!boost)
            {
                airThrusterGO.SetActive(false);
                waterThrusterGO.SetActive(true);
                airBoostThrusterGO.SetActive(false);
                waterBoostThrusterGO.SetActive(false);
            }
            else
            {
                airBoostThrusterGO.SetActive(false);
                waterBoostThrusterGO.SetActive(true);
                airThrusterGO.SetActive(false);
                waterThrusterGO.SetActive(false);
            }
            IsPlayerUnderwater = true;

        }
        else
        {
            if (IsPlayerUnderwater && boost == false)
            {
                Weapons[CurrentWeapon].SetActive(false);
                CurrentWeapon--;
                Weapons[CurrentWeapon].SetActive(true);
            }
            StopCoroutine("Drain");
            StartCoroutine("Drain");

            if (!boost)
            {
                airThrusterGO.SetActive(true);
                waterThrusterGO.SetActive(false);
                airBoostThrusterGO.SetActive(false);
                waterBoostThrusterGO.SetActive(false);
            }
            else
            {
                airBoostThrusterGO.SetActive(true);
                waterBoostThrusterGO.SetActive(false);
                airThrusterGO.SetActive(false);
                waterThrusterGO.SetActive(false);
            }
            IsPlayerUnderwater = false;
        }
        Vector3 movement = new Vector3(inputX * speed.x, inputY * speed.y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        if (CurrentWeapon == 0 || CurrentWeapon == 1)
        {
            bool shoot = Input.GetButton("Fire4");
            if (shoot)
            {
                if (CurrentWeapon == 0 || CurrentWeapon == 1)
                {
                    playerAnimator.SetBool("CanonBallActive", false);
                    playerAnimator.SetBool("PistolAttack", true);
                }

                fShoot(CurrentWeapon);
            }
            else
            {
                playerAnimator.SetBool("PistolAttack", false);
            }

        }

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
        // End of the update method

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {

            if (boost == true && Time.time > nextDMGPlayer)
            {
                collider.gameObject.GetComponent<HealthScript>().hp -= dashDamage;
                nextDMGPlayer = Time.time + dmgRatePlayer;

            }
        }
        if (collider.gameObject.tag == "Endgate")
        {
            //Debug.Log("U WON!");
            Time.timeScale = 0f;
            winScreen.SetActive(true);
        }
    }

    public void BeeingDestroyed()
    {
        Time.timeScale = 0f;
        loseScreen.SetActive(true);
    }

    void fShoot(int mCurrentWeapon)
    {
        WeaponScript weapon = Weapons[mCurrentWeapon].GetComponentInChildren<WeaponScript>();
        if (weapon != null)
        {
            weapon.Attack(false);
        }

        BajonettScript bajonett = Weapons[mCurrentWeapon].GetComponentInChildren<BajonettScript>();
        if (bajonett != null)
        {
            bajonett.Attack(false);
        }
    }

    //Canonball FX
    IEnumerator boostON(float duration)
    {
        duration = dashDuration;
        toogleBoolThree = false;
        boost = true;
        toogleBoolTwo = true;
        this.gameObject.GetComponent<HealthScript>().healthEnabled = false;
        shild.GetComponent<Image>().enabled = true;
        Weapons[CurrentWeapon].SetActive(false);
        Weapons[2].SetActive(true);
        boostWeaponGlowGO.SetActive(true);
        SoundList.soundList.Activation.Play();
        SoundList.soundList.DurationFeedback.Play();



        yield return new WaitForSeconds(duration);

        SoundList.soundList.DurationFeedback.Stop();
        SoundList.soundList.Deactivation.Play();
        boost = false;
        shild.GetComponent<Image>().enabled = false;
        this.gameObject.GetComponent<HealthScript>().healthEnabled = true;
        if (IsPlayerUnderwater)
            CurrentWeapon = 1;
        else
            CurrentWeapon = 0;
        Weapons[2].SetActive(false);
        Weapons[CurrentWeapon].SetActive(true);
        boostWeaponGlowGO.SetActive(false);


        yield return new WaitForSeconds(cooldownTime);
        SoundList.soundList.CooldownOver.Play();
        toogleBoolThree = true;
    }

    IEnumerator fChangeWeapon(float mWeaponChangingDelay)
    {
        if (CurrentWeapon != RequestedWeapon)
        {
            yield return new WaitForSeconds(0);
            Weapons[CurrentWeapon].SetActive(false);
            CurrentWeapon = RequestedWeapon;
            Weapons[CurrentWeapon].SetActive(true);
            WeaponIsChanging = false;
        }
    }

    //Plays FX if player moves below Y_WaterBorder
    IEnumerator Splash()
    {   //is started once in Update before bool is set to true
        // ---> only played once after change to water
        if (!IsPlayerUnderwater)
        {
            if (splashGO != null)
            {
                Instantiate(splashGO, new Vector2(transform.position.x, Y_WaterBorder - 0.2f), transform.rotation);

                SoundList.soundList.Air_Water_Switch_Eintauchen.Play();
                SoundList.soundList.Weapon_Switch.Play();
                //Play BubbleSplashFX
                if (bubbleSplashGO != null)
                {
                    if (bubbleSplashGO.activeSelf == false)
                    {
                        bubbleSplashGO.SetActive(true);
                    }
                    bubbleSplashGO.GetComponent<ParticleSystem>().Clear();
                    bubbleSplashGO.GetComponent<ParticleSystem>().Play();
                }
            }
        }
        yield return null;
    }

    //Plays FX if player moves above Y_WaterBorder
    IEnumerator Drain()
    {

        if (IsPlayerUnderwater)
        {
            if (drainGO != null)
            {
                //FX is looped, that's why we activate it just for 1 second
                drainGO.SetActive(true);
                SoundList.soundList.Air_Water_Switch_Auftauchen.Play();
                SoundList.soundList.Weapon_Switch.Play();
                yield return new WaitForSeconds(1);
                drainGO.SetActive(false);
            }
        }
        yield return null;
    }
}
