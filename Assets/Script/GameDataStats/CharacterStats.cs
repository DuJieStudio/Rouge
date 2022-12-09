using System;
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

  
}
