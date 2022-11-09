using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Enemy_Flower Data")]
public class FlowerData_SO : ScriptableObject
{
    [Header(" ˝æ›…Ë÷√")]
    public float maxhealth;
    public float currenthealth;
    public float damage;
}
