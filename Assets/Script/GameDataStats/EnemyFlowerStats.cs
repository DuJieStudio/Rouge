using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlowerStats : MonoBehaviour
{
    public SoliderData_SO FlowerData;

    public float MaxHealth
    {
        get
        {
            if (FlowerData != null)
            { return FlowerData.maxhealth; }
            else
            { return 0; }
        }
        set
        {
            FlowerData.maxhealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            if (FlowerData != null)
            { return FlowerData.currenthealth; }
            else
            { return 0; }
        }
        set
        {
            FlowerData.currenthealth = value;
        }
    }

}
