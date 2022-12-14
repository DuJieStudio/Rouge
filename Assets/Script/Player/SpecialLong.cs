using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLong : MonoBehaviour
{
    public GameObject Target;
    public Collider2D[] collider2ds;//overlapÅö×²Ìå´æ·Å
    public Vector3 specialAttack;
    public Vector2 SpecialCheck;
    public float y_special;

    void Start()
    {
        specialAttack = new Vector3(transform.position.x, transform.position.y + y_special, transform.position.z);
    }


    void Update()
    {
      //  SpecialLongAttack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(specialAttack, SpecialCheck);
    }

    public void SpecialLongAttack()
    {
        collider2ds = Physics2D.OverlapBoxAll(specialAttack, SpecialCheck, 0);
        foreach (var target in collider2ds)
        {
            if (target.CompareTag("enemy"))
            {
                Target = target.gameObject;
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Solider)
                {
                   // Target.GetComponent<Enemy_Solider>().SpecialDamage();
                    Target.GetComponent<Enemy_Solider>().GetHit();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Flower)
                {
                    Target.GetComponent<Enemy_Flower>().SpecialDamage();
                    Target.GetComponent<Enemy_Flower>().GetHit();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Ghost)
                {
                    Target.GetComponent<Ghost>().SpecialDamage();
                    Target.GetComponent<Ghost>().GetHit();
                }
                if (Target.GetComponent<Enemy_AI>().enemyName == EnemyName.Light)
                {
                    Target.GetComponent<Enemy_Light>().SpecialDamage();
                    Target.GetComponent<Enemy_Light>().GetHit();
                }
                Debug.Log(Target.name);
            }
        }
    }


    void Destory()
    {
        Destroy(gameObject);
    }
}
