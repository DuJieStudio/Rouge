using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Enemy_Solider Data")]
public class SoliderData_SO : ScriptableObject
{
    [Header("Êı¾İÉèÖÃ")]
    public float maxhealth;
    public float currenthealth;
    public float damage;
    public float attackrate;
    public int force;
    public float offset;

}
