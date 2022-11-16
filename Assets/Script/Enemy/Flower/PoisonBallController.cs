using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PoisonBallController : MonoBehaviour
{

    public float keepTime = 0;
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject flower;
    private GameObject player;
    [HideInInspector]
    public bool isStart;
    public Transform father;
    
    void Start()
    {

        father = gameObject.transform.parent;
        flower = GameObject.Find("Enemy_Flower");
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        BulletMove();

    }

    // Update is called once per frame
    void Update()
    {

        SwitchStats();

    }

    //改变球的状态
    void SwitchStats()
    {

        anim.SetBool("Start", isStart);
        
        if(anim.GetBool("Start"))
        {
            keepTime += Time.deltaTime;
        }

        if(keepTime >= 2f)
        {
            anim.SetTrigger("End");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fog_End") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
            {
                Destroy(this.gameObject);
            }
        }

    }

    //识别球的运动方向
    void BulletMove()
    {

        rb.velocity = new Vector2(-flower.transform.localScale.x * 5f, 0);
       
    }

    //碰撞地形或玩家
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (this.gameObject.CompareTag("enemyAttack"))
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ground"))
            {
                isStart = true;
                rb.velocity = new Vector2(0, 0);
                this.gameObject.tag = "enemy";
            }
            else if (collision.gameObject.CompareTag("playerBlock"))
            {
                this.gameObject.tag = "playerAttack";
                rb.velocity = new Vector2(this.transform.localScale.x * 5, 0);
            }
        }
        else if (this.gameObject.tag == "playerAttack")
        {
            if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("ground"))
            {
                isStart = true;
                rb.velocity = new Vector2(0, 0);
            }
        }
    }
}
