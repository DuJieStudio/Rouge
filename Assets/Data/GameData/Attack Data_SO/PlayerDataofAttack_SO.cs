using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Data",menuName ="Attack Data")]
public class PlayerDataofAttack_SO : ScriptableObject
{
    [Header("¹¥»÷Êý¾Ý")]
    public float MinDamage;
    public float MaxDamage;
    public float SkillDamage;
    public float CoolDown;
    public float Critical_Multiplier;
    public float Critical_Chance;
    public float Power;
}
