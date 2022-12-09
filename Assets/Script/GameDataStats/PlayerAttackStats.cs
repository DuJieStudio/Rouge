using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStats : MonoBehaviour
{
    public PlayerDataofAttack_SO playerDataAttack;

    public float MinDamage
    {
        get
        {
            if (playerDataAttack != null)
            { return playerDataAttack.MinDamage; }
            else
            { return 0; }
        }
        set
        {
            playerDataAttack.MinDamage = value;
        }
    }

    public float MaxDamage
    {
        get
        {
            if (playerDataAttack != null)
            { return playerDataAttack.MaxDamage; }
            else
            { return 0; }
        }
        set
        {
            playerDataAttack.MaxDamage = value;
        }
    }
    public float SkillDamage
    {
        get
        {
            if (playerDataAttack != null)
            { return playerDataAttack.SkillDamage; }
            else
            { return 0; }
        }
        set
        {
            playerDataAttack.SkillDamage = value;
        }
    }

    public float CoolDown
    {
        get
        {
            if (playerDataAttack != null)
            { return playerDataAttack.CoolDown; }
            else
            { return 0; }
        }
        set
        {
            playerDataAttack.CoolDown = value;
        }
    }

    public float Power
    {
        get
        {
            if (playerDataAttack != null)
            { return playerDataAttack.Power; }
            else
            { return 0; }
        }
        set
        {
            playerDataAttack.Power = value;
        }
    }
}
