using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour
{
    public GameObject Player;
    private float speed;
    private Rigidbody2D rigidbody_of_player;
    private Animator animator_of_player;
    private BoxCollider2D collder_of_player;
    private int toward;
    [Header("环境检测")]
    public float checkRadius;//检测地面偏移
    public LayerMask whatIsGround;//地面图层
    public int jumpcount;

    public bool isGround;//是否在地面
    // Start is called before the first frame update
    void Start()
    {
        jumpcount = 2;
        toward = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
        speed = 0.15f;
        rigidbody_of_player = GetComponent<Rigidbody2D>();
        animator_of_player = GetComponent<Animator>();
        collder_of_player = GetComponent<BoxCollider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Isground();
        
    }
    private void Move()
    {

        rigidbody_of_player.velocity = new Vector2(0, rigidbody_of_player.velocity.y);
        collder_of_player.isTrigger = false;
        if (Input.GetKey(KeyCode.A))
        {
            transform.transform.Translate(Vector3.left *speed, Space.Self);
            Player.transform.localScale = new Vector3(-1, 1, 1);
            toward = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.transform.Translate(Vector3.right * speed, Space.Self);
            Player.transform.localScale = new Vector3(1, 1, 1);
            toward = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            {
            if (jumpcount > 1)
            {
                rigidbody_of_player.velocity=new Vector2(0,Mathf.Lerp(5, 10, 0.6f));
                animator_of_player.Play("Jump");
                jumpcount--;
            }
            }
        if (Input.GetKey(KeyCode.LeftShift)&&toward!=0)
        {
            if (toward == 1)
            { Player.transform.position = new Vector2(Player.transform.position.x + 0.2f, Player.transform.position.y);
                animator_of_player.Play("shift");
                collder_of_player.isTrigger = true;

            }
            if (toward == -1)
            { Player.transform.position = new Vector2(Player.transform.position.x - 0.2f, Player.transform.position.y);
                animator_of_player.Play("shift");
                collder_of_player.isTrigger = true;
            }

            
        }
        
    }
    private void Isground()
    {
        if (rigidbody_of_player.IsTouchingLayers(whatIsGround))
        {
            animator_of_player.SetBool("fall", true);
            jumpcount = 2;
        }
        else
        {
            animator_of_player.SetBool("fall", false);
        }
    }
}
    