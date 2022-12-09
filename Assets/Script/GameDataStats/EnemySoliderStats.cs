using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoliderStats : MonoBehaviour
{
    public SoliderData_SO SoliderData;
    //public PlayerData_SO PlayerData;

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

    public float attackrate
    {
        get
        {
            if (SoliderData != null)
            { return SoliderData.attackrate; }
            else
            { return 0; }
        }
        set
        {
            SoliderData.attackrate = value;
        }

    }
    public float force
    {
        get
        {
            if (SoliderData != null)
            { return SoliderData.force; }
            else
            { return 0; }
        }
        set
        {
            SoliderData.attackrate = value;
        }

    }

   
}
