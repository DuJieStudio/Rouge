using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoliderStats : MonoBehaviour
{
    public SoliderData_SO SoliderData;

    public float MaxHealth
    {
        get
        {
            if (SoliderData != null)
            { return SoliderData.maxhealth; }
            else
            { return 0; }
        }
        set
        {
            SoliderData.maxhealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            if (SoliderData != null)
            { return SoliderData.currenthealth; }
            else
            { return 0; }
        }
        set
        {
            SoliderData.currenthealth = value;
        }
    }

    public float Damage
    {
        get
        {
            if (SoliderData != null)
            { return SoliderData.damage; }
            else
            { return 0; }
        }
        set
        {
            SoliderData.damage = value;
        }

    }
}
