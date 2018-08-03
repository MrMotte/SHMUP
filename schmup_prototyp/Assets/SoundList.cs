using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{

    [Header("Enemy")]
    public AudioSource Enemy_Formation_Appears;
    public AudioSource Enemy_Death_Air;
    public AudioSource Enemy_Death_Water;
    public AudioSource Enemy_Damage_Air;
    public AudioSource Enemy_Damage_Water;
    public AudioSource Enemy_Bomber_Drop;
    public AudioSource Enemy_Chaser_Rocket;
    public AudioSource Enemy_Sniper_Beam_Load;
    public AudioSource Enemy_Treasure_Teleport;

    [Header("Level")]
    public AudioSource End_Gate_opens;

    [Header("Menu")]
    public AudioSource Button_Click;
    public AudioSource Button_Move;
    public AudioSource Highscore_reached;
    public AudioSource Volume_Change_Feedback;
    public AudioSource Score_Count;

    [Header("Music")]
    public AudioSource Ingame_Music;
    public AudioSource Win_Music;
    public AudioSource Menu_Music;
    public AudioSource Game_Over_Jingle;

    [Header("Player")]
    [Header("Engine")]
    public AudioSource Player_Engine_Air;
    public AudioSource Player_Engine_Water;
    public AudioSource Player_Engine_Dash_Start;
    public AudioSource Player_Engine_Dash_on;

    [Header("Stats")]
    public AudioSource Player_Bullet_Damage;
    public AudioSource Player_Collision_Damage;
    public AudioSource Player_Death_Air;
    public AudioSource Player_Death_Water;

    [Header("Environment")]
    public AudioSource Air_Water_Switch_Eintauchen;
    public AudioSource Air_Water_Switch_Auftauchen;

    [Header("Weapon")]
    public AudioSource Weapon_Switch;
    public AudioSource Weapon_Lightning_Pistol_Shot_Air;
    public AudioSource Weapon_Lightning_Pistol_Shot_Water;
    public AudioSource Weapon_Lightning_Pistol_AOE_Water_Damage;
    public AudioSource Weapon_Bayonet_Attack_Air;
    public AudioSource Weapon_Bayonet_Enemy_Hit_Stun;
    public AudioSource Weapon_Bayonet_Fire_Substance;
    public AudioSource Weapon_Bayonet_Holddown_Water;
    public AudioSource Weapon_Bayonet_Fire_whole_Bayonet;
    public AudioSource Weapon_Bayonet_loaded;

    [Header("Shield")]
    public AudioSource Shield_Damage;
    public AudioSource Shield_Destroy;
    public AudioSource Shield_Cracks;
    public AudioSource Shield_Activation;
    public AudioSource Shield_Deactivation;

    public static SoundList soundList;


    void Awake()
    {
        if (soundList == null)
        {
            DontDestroyOnLoad(this.gameObject);
            soundList = this;
        }
        else if (soundList != this)
        {
            Destroy(this.gameObject);
        }
    }

}
