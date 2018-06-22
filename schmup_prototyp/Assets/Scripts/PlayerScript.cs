using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript playerInstance = null;

    //for engine animation
    private Animator animatorEngine;
    private Animator animatorWeapon;

    // Movement
    public Vector2 speed = new Vector2(50, 50);

    // Weapons
    [Header("First Half Air, second Water Weapons. Array should always be divisible by 2!")]
    public GameObject[] Weapons;
    int CurrentWeapon = 1;
    bool WeaponIsChanging = false;
    [Header("Delay for changing weapon")]
    public float WeaponChangingDelay = 0;
    int RequestedWeapon = 1;
    SpriteRenderer WeaponSpriteRenderer;

    // Air - Water Switch
    public float Y_WaterBorder = 0;
    private bool IsPlayerUnderwater = false;

    //Save and Show Currency and Score
    public float Score = 0;
    public float Currency = 0;
    public Text text;

    // Update is called once per frame

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (!playerInstance)
            playerInstance = this;

        if (playerInstance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        animatorEngine = gameObject.transform.Find("Engine").GetComponent<Animator>();
        animatorWeapon = gameObject.transform.Find("Weapon 1").GetComponentInChildren<Animator>();
        Score = 0;
        Currency = 0;

        //GetComponentInChildren<Animator>();

    }

    void Update()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        //text.text = Currency.ToString();
        print(Currency);

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

    public void UpdateCurrency(float m_Currency, float m_Score)
    {
        Currency = Currency + m_Currency;
        Score = Score + m_Score;
        text.text = Currency.ToString();
    }
}
