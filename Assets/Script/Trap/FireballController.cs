using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public Rigidbody2D rig;
    public PlayerData_SO playerdata;
    public LayerMask player;
    private bool ishit;
    [Header("�����ٶ�")]
    public float bulletspeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rig.velocity =  Vector2.left * bulletspeed;
        Destroy(GameObject.FindGameObjectWithTag("fireball"), 1);
        
    }
     void Update()
    {
        if (Physics2D.OverlapCircle(gameObject.transform.position, 0.2f, player))
        {
            Destroy(GameObject.FindGameObjectWithTag("fireball"));
        }
    }



}
