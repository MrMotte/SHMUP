﻿using System.Collections;
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
    public AudioSource Enemy_Chaser_Speed_Movement;
    public AudioSource Enemy_Chaser_Explosion;
    public AudioSource Enemy_Chaser_Proximity;
    public AudioSource Enemy_Sniper_Energie_Suchrakete;

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

    [Header("Stats")]
    public AudioSource Player_Bullet_Damage_Air;
    public AudioSource Player_Bullet_Damage_Water;
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

    [Header("Ability")]
    public AudioSource Activation;
    public AudioSource Deactivation;
    public AudioSource DurationFeedback;
    public AudioSource CooldownOver;

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
