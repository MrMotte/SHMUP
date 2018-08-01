using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //for engine animation
    private Animator animatorEngine;
    private Animator animatorWeapon;

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
	[Header ("Movement FX  -  Air/Water Switch FX")]
    public GameObject splashGO;
	public GameObject drainGO;
	public GameObject bubbleSplashGO;
	
	public GameObject airThrusterGO;
	public GameObject waterThrusterGO;


    // Update is called once per frame

    private void Start()
    {
        animatorEngine = gameObject.transform.Find("Engine").GetComponent<Animator>();
        //animatorWeapon = gameObject.transform.Find("LightningPistolAir").GetComponentInChildren<Animator>();

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
            //animatorEngine.SetBool("IsMovingUp", true);
        }
        else
        {
            //animatorEngine.SetBool("IsMovingUp", false);
        }

        // detect if player is currently underwater
        if (this.transform.position.y < Y_WaterBorder)
        {
            if (!IsPlayerUnderwater)
            {
                WeaponSpriteRenderer = Weapons[CurrentWeapon].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = false;
                CurrentWeapon++;
                WeaponSpriteRenderer = Weapons[CurrentWeapon].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = true;
                // Call VFX, GUI and Sound
            }




            //CHRISTIAN
            //Instantiate Splash FX on WaterSurface
            //	BEGIN
            StopCoroutine("Splash");
            StartCoroutine("Splash");
			
			airThrusterGO.SetActive(false);
			waterThrusterGO.SetActive(true);
			
            //	END	
            IsPlayerUnderwater = true;
        }
        else
        {
            if (IsPlayerUnderwater)
            {
                WeaponSpriteRenderer = Weapons[CurrentWeapon].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = false;
                CurrentWeapon--;
                WeaponSpriteRenderer = Weapons[CurrentWeapon].GetComponentInChildren<SpriteRenderer>();
                WeaponSpriteRenderer.enabled = true;
                // Call VFX, GUI and Sound
            }
            

            //CHRISTIAN
            //Activate Drain FX
            //	BEGIN
			
			StopCoroutine("Drain");
			StartCoroutine("Drain");
			
			airThrusterGO.SetActive(true);
			waterThrusterGO.SetActive(false);
			
            //	END	
			IsPlayerUnderwater = false;
		}

        Vector3 movement = new Vector3(inputX * speed.x, inputY * speed.y, 0);

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
        else if (boost == true && toogleBoolTwo == true)
        {
            speed.x += dashSpeed;
            speed.y += dashSpeed;
            toogleBoolTwo = false;
        }


        movement *= Time.deltaTime;

        transform.Translate(movement);

		if(CurrentWeapon != 1)
		{
			bool shoot = Input.GetButton("Fire4");
			#region Weapon change
			if (shoot && !WeaponIsChanging)
			{
				if (CurrentWeapon == 0)
				{
					//Debug.Log("Weapon 1 is shooting");
					//Animation Weapon 1 start
					//animatorWeapon.SetBool("IsWeaponShooting", true);
				}

				fShoot(CurrentWeapon);
			}

			//else
			//{
			//Animation Weapon 1 stops
			//animatorWeapon.SetBool("IsWeaponShooting", false);
			//}
		}

		if (Input.GetKey(KeyCode.W) && CurrentWeapon == 1 && !WeaponIsChanging)
		{
			chargeTimer += Time.deltaTime;
		}
		if (Input.GetKeyUp(KeyCode.W) && (chargeTimer > maxChargeTime) && CurrentWeapon == 1 && !WeaponIsChanging)
		{
			fShoot(CurrentWeapon);
			chargeTimer = 0;
		}
		if (Input.GetKeyUp(KeyCode.W) && (chargeTimer < maxChargeTime) && CurrentWeapon == 1 && !WeaponIsChanging)
		{
			chargeTimer = 0;
		}


		if (Input.GetButton("Weapon 1"))
        {
            if (IsPlayerUnderwater) { RequestedWeapon = 1; }
            else
                RequestedWeapon = 0;
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 2"))
        {
            if (IsPlayerUnderwater) { RequestedWeapon = 3; }
            else
                RequestedWeapon = 2;
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 3"))
        {
            if (IsPlayerUnderwater) { RequestedWeapon = 5; }
            else
                RequestedWeapon = 4;
            StartCoroutine(fChangeWeapon(WeaponChangingDelay));
        }
        if (Input.GetButton("Weapon 4"))
        {
            if (IsPlayerUnderwater) { RequestedWeapon = 6; }
            else
                RequestedWeapon = 5;
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


        if (Input.GetButtonDown("ShildBatteringRam"))
        {
            toogleBool = !toogleBool;
            shild.GetComponent<Image>().enabled = toogleBool;
            shild.GetComponent<CapsuleCollider2D>().enabled = toogleBool;
            

        }
        if(Time.frameCount % 20 == 0)
        {
        if(toogleBool && shild.GetComponent<Shild>().shildHP > 0)
        {
            this.gameObject.GetComponent<HealthScript>().healthEnabled = false;
        }
        else
           this.gameObject.GetComponent<HealthScript>().healthEnabled = true;
        }


        #endregion

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
                Debug.Log("AAAAAAAAAAAAAAAAAHHHHHHHHHHHHH");
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
        //transform.parent.gameObject.AddComponent<GameOverScript>();
        //Debug.Log("U LOST");
        Time.timeScale = 0f;
        loseScreen.SetActive(true);
    }

    void fShoot(int mCurrentWeapon)
    {
        WeaponScript weapon = Weapons[mCurrentWeapon].GetComponent<WeaponScript>();
        if (weapon != null)
        {
            weapon.Attack(false);
        }

        BajonettScript bajonett = Weapons[mCurrentWeapon].GetComponent<BajonettScript>();
        if (bajonett != null)
        {
            bajonett.Attack(false);
        }
    }

	IEnumerator boostON(float duration)
    {
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
            if (WeaponSwitch != null)
                WeaponSwitch.Play();

            WeaponIsChanging = true;
            yield return new WaitForSeconds(mWeaponChangingDelay);

            WeaponSpriteRenderer = Weapons[CurrentWeapon].GetComponentInChildren<SpriteRenderer>();
            WeaponSpriteRenderer.enabled = false;

            CurrentWeapon = RequestedWeapon;
            WeaponSpriteRenderer = Weapons[CurrentWeapon].GetComponentInChildren<SpriteRenderer>();
            WeaponSpriteRenderer.enabled = true;

            Debug.Log("Changed Weapon to: " + CurrentWeapon);
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
                Instantiate(splashGO, new Vector2(transform.position.x, Y_WaterBorder), transform.rotation);
				//Play BubbleSplashFX
				if(bubbleSplashGO != null)
				{
					if(bubbleSplashGO.activeSelf == false)
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
	IEnumerator Drain(){
		
		if(IsPlayerUnderwater){
			if(drainGO != null)
			{
				//FX is looped, that's why we activate it just for 1 second
				drainGO.SetActive(true);
				yield return new WaitForSeconds(1);
				drainGO.SetActive(false);
			}		
		}
		yield return null;
	}
}
