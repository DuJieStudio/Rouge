using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public Rigidbody2D rig;
    public PlayerData_SO playerdata;
    public LayerMask player;
    [Header("»ðÇòËÙ¶È")]
    public float bulletspeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("fireball"),1);
        rig.velocity =  Vector2.left * bulletspeed;
        
    }
     void Update()
    {
        if (Physics2D.OverlapCircle(gameObject.transform.position, 0.2f, player))
        {
            Destroy(GameObject.FindGameObjectWithTag("fireball"));
        }
    }



}
