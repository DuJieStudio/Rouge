using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Character Data")]
public class PlayerData_SO : ScriptableObject
{
    [Header("��������")]
    public float maxhealth;
    public float currenthealth;
    public float baseDefence;
    public float moveSpeed;

}