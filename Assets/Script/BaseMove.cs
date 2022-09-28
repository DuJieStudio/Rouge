using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMove : MonoBehaviour
{
    public GameObject Player;
    public  float speed;
    private Rigidbody2D rigidbody_of_player;
    private Animator animator_of_player;
    public Animator anim2;
    public Image CDImage;
    public PlayerData_SO playerdata;
    [Header("环境检测")]
    public LayerMask IsGround;//地面图层
    public Collider2D coll;
    public int jumpcount;
    public bool onGround;
    public Transform GroundCheck;
    private float shifttime;//shift计时器


     [Header("跳跃相关")]
    public AnimationCurve curve;
    public float Totaltime = 1f;
    public float fallMultiplier;
    public float JumpForce;//跳跃相关

    public float Dashtime;
    private float DashTimeLetf;
    //public float DashSpeed;
    private float LastDash = -10;
    public float DashCoolDown;
    public  bool isDashing;
    public AudioSource DashAudio;
 

    private void Awake()
    {

    }

    void Start()
    {
        shifttime = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
        rigidbody_of_player = GetComponent<Rigidbody2D>();
        animator_of_player = GetComponent<Animator>();
        speed = playerdata.moveSpeed;
      //  jumpcount = 1;
        Debug.Log(IsGround);
    }


    void Update()
    {   Move();   
        Dash();
        Jump();
        SwitchAnim();
        cdline();
        onGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, IsGround);
        
    }

    private void Move()
    {  
        float horizontalmove = Input.GetAxis("Horizontal"); //定义浮点型变量horizontalmove获取Axi中Horizontal（控制移动方向，值为1，0，-1及中间小数）的数值     
        float facedirection = Input.GetAxisRaw("Horizontal");//GetAxisRaw与GetAxis的区别，前者直接获取10-1三个数，后者可获取中间小数

        //rigidbody_of_player.velocity = new Vector2(horizontalmove * speed, rigidbody_of_player.velocity.y);
        if (horizontalmove != 0) //角色移动
        {
            rigidbody_of_player.velocity = new Vector2(horizontalmove * speed, rigidbody_of_player.velocity.y);
            //transform.localScale = new Vector3(horizontalmove, 1, 1);
            //rigidbody_of_player.velocity = new Vector2(horizontalmove * speed, rigidbody_of_player.velocity.y);
            animator_of_player.SetFloat("running", Mathf.Abs(facedirection));//让Animator中的running获取速度数值即facedirection，用mathf保证数值为正
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);//获取player中的transform中的scale这个控制方向的变量
        }

    
   
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Time.time >= (LastDash + DashCoolDown))
            {
                ReadyToDash();
            }
            
        }
        //CDImage.fillAmount -= 1.0f / DashCoolDown * Time.deltaTime;
    }

    void SwitchAnim()
    {
        //animator_of_player.SetBool("idle",false);
        if (animator_of_player.GetBool("jumping")) 
        {
            if (rigidbody_of_player.velocity.y < 0)
            {
                animator_of_player.SetBool("jumping", false);
                animator_of_player.SetBool("falling", true);               
            }
             
        }
        else if (rigidbody_of_player.IsTouchingLayers(IsGround))
        {
            animator_of_player.SetBool("falling", false);
            animator_of_player.SetBool("idle", true);          
        }
    }
     

    void ReadyToDash()
    {
        isDashing = true;
        DashTimeLetf = Dashtime;
        LastDash = Time.time;
        anim2.SetBool("effect", true);
        
    }

    void Dash()
    {
        if (isDashing)
        {
            if (DashTimeLetf > 0)
            {

                Player.transform.position = new Vector2(Player.transform.position.x + 0.15f * rigidbody_of_player.transform.localScale.x, Player.transform.position.y);
               // rigidbody_of_player.velocity = new Vector2(DashSpeed * rigidbody_of_player.transform.localScale.x, rigidbody_of_player.velocity.y);               
                animator_of_player.SetBool("shift",true);
                //anim2.SetBool("effect", true);
                DashTimeLetf -= Time.deltaTime;
                ShadowPool.instance.GetFormPool();
            }
            if (DashTimeLetf <= 0)
            {
                isDashing = false;
                anim2.SetBool("effect", false);

                if (rigidbody_of_player.IsTouchingLayers(IsGround))
                {
                    animator_of_player.SetBool("shift", false);
                    animator_of_player.SetBool("idle", true);
                }
                else
                {
                    animator_of_player.SetBool("shift", false);
                    animator_of_player.SetBool("falling", true);
                }
               
            }
        }
    }

    void Jump()
    {
        if (onGround)
        {
            jumpcount = 1;
        }


        if (Input.GetKeyDown(KeyCode.Space)&&jumpcount>0)
        {          
            StartCoroutine(StartCurve());
            jumpcount--;
           // animator_of_player.SetBool("jumping", true);          
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpcount == 0 && onGround)
        {
            StartCoroutine(StartCurve());
            animator_of_player.SetBool("jumping", true);
        }


        if (rigidbody_of_player.velocity.y < 0)
        {
            rigidbody_of_player.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        IEnumerator StartCurve()
        {
            float time = 0;
            while (time <= Totaltime)
            {
                float normalizedTime = (time / Totaltime);
                time += Time.deltaTime;
                float curveValue = curve.Evaluate(normalizedTime);
                rigidbody_of_player.velocity = new Vector2(rigidbody_of_player.velocity.x, JumpForce * curveValue);
                animator_of_player.SetBool("jumping", true);
                yield return null;
            }
        }

    }

    void cdline()
    {
        CDImage.fillAmount = (Time.time - LastDash) / DashCoolDown;
    }
}