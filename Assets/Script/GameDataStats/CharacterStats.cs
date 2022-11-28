using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public PlayerData_SO PlayerData;

    public float MaxHealth 
    {
        get
        { if (PlayerData != null)
            { return PlayerData.maxhealth; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.maxhealth = value; 

        }
    }
    public float CurrentHealth
    {
        get
        {
            if (PlayerData != null)
            { return PlayerData.currenthealth; }
            else
            { return 0; }
        }
        
        set
        {

             PlayerData.currenthealth = value; 

        }
    }
    public float BaseDefence
    {
        get
        {
            if (PlayerData != null)
            { return PlayerData.baseDefence; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.baseDefence = value;
        }
    }
    public float MoveSpeed
    {
        get
        {
            if (PlayerData != null)
            { return PlayerData.moveSpeed; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.moveSpeed = value;
        }
    }

    public PlayerDataofAttack_SO Attackdata;
    public float MinDamage
    {
        get
        {
            if (PlayerData != null)
            { return Attackdata.MinDamage; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.maxhealth = value;
        }
    }
    public float MaxDamage
    {
        get
        {
            if (PlayerData != null)
            { return Attackdata.MaxDamage; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.maxhealth = value;
        }
    }
    public float CoolDown
    {
        get
        {
            if (PlayerData != null)
            { return Attackdata.CoolDown; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.maxhealth = value;
        }
    }
    public float Critical_Multiplierh
    {
        get
        {
            if (PlayerData != null)
            { return Attackdata.Critical_Multiplier; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.maxhealth = value;
        }
    }
    public float Critical_Chance
    {
        get
        {
            if (PlayerData != null)
            { return Attackdata.Critical_Chance; }
            else
            { return 0; }
        }
        set
        {
            PlayerData.maxhealth = value;
        }
    }

   // public 
}
