using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{

    public PlayerAttackCheck normalAttackCheck;

    void Start()
    {
        normalAttackCheck = GameObject.Find("AttackCheck").GetComponent<PlayerAttackCheck>();
    }


    void Update()
    {
        
    }

    public void normalAttack()
    {
        normalAttackCheck.NormalAttack();      
    }

    public void skillShortAttack()
    {
        normalAttackCheck.SkillShortAttack();
    }

    public void skillLongAttack()
    {
        normalAttackCheck.SkillLongAttack();
    }
}
