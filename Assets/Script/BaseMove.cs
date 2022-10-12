using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BaseMove : MonoBehaviour
{
    public GameObject Player;
    public  float speed;
 //   public float currentSpeed;
    public float acceration=8f;
    public float jumpspeed;
    private Rigidbody2D rigidbody_of_player;
    private Animator animator_of_player;
    public Animator anim2;
    public Image CDImage;
    public Image HPline;
    public PlayerData_SO playerdata;
    public CharacterStats Characterstats;
    [Header("环境检测")]
    public LayerMask IsGround;//地面图层
    public Collider2D coll;
    public int jumpcount;
    public bool onGround;
    public Transform GroundCheck;
    public float currectHealth;
    private float shifttime;//shift计时器


     [Header("跳跃相关")]
    public AnimationCurve curve;
    public float Totaltime = 1f;
    public float fallMultiplier;
    public float JumpForce;//跳跃相关

    public float Dashtime;
    private float DashTimeLetf;
    //public float DashSpeed;
    //private float LastDash = -10;
    public float DashCoolDown;
    public bool canDash = true;
    public bool isDashing;
    public float DashPower;

    //public float shiftcheck = 2f;
    //public float DashSpeed;
    public AudioSource DashAudio;
<<<<<<< HEAD
<<<<<<< HEAD
    public bool IsinvIncible;
    public float InvincibleTime=2.0f;
    public float CurrectInvincibleTime;
=======
    public float horizontalmove;


>>>>>>> abfd55594285412f6a1245bcdb8d8befbd469473

=======
    public float horizontalmove;
    public float facedirection;

    private CharacterStats characterStats;
   // public float MoveSpeed => Mathf.Abs(rigidbody_of_player.velocity.x);
    
    


    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
    }
>>>>>>> 7bf8f79a9c553aca976b11fcf54b5b2b067607b4

    void Start()
    {
        shifttime = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
        rigidbody_of_player = GetComponent<Rigidbody2D>();
        animator_of_player = GetComponent<Animator>();
        speed = playerdata.moveSpeed;
      //  jumpcount = 1;
        //Debug.Log(IsGround);
        characterStats.MaxHealth = 10;
    }


    void Update()
    {
        if (isDashing)
        {
            return;
        }
      
        Move();
        //Dash();
        Jump();
        SwitchAnim();
<<<<<<< HEAD
<<<<<<< HEAD
        cdline();
        onGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, IsGround);
        
        
=======
        //ShiftCheck();
        onGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, IsGround);       
>>>>>>> abfd55594285412f6a1245bcdb8d8befbd469473
=======
        //shadow();
        //ShiftCheck();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            //DashTimeLetf = Dashtime;
        }
        
        //if (isDashing)
        //{
        //    if (DashTimeLetf > 0)
        //    {
        //        DashTimeLetf -= Time.deltaTime;
        //        ShadowPool.instance.GetFormPool();
        //    }
        //}

        onGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, IsGround);       
>>>>>>> 7bf8f79a9c553aca976b11fcf54b5b2b067607b4
    }

    //private void FixedUpdate()
    //{
    //    if (isDashing)
    //    {
    //        return;
    //    }
    //    Move();
    //    //Dash();
    //    Jump();
    //    SwitchAnim();
    //}


    private void Move()
    {   
        horizontalmove = Input.GetAxis("Horizontal"); //定义浮点型变量horizontalmove获取Axi中Horizontal（控制移动方向，值为1，0，-1及中间小数）的数值     
        facedirection = Input.GetAxisRaw("Horizontal");//GetAxisRaw与GetAxis的区别，前者直接获取10-1三个数，后者可获取中间小数

        //rigidbody_of_player.velocity = new Vector2(horizontalmove * speed, rigidbody_of_player.velocity.y);
        if (horizontalmove != 0) //角色移动
        {
            //currentSpeed = Player.MoveSpeed;
            if (!onGround)
            {
                rigidbody_of_player.velocity = new Vector2(horizontalmove * jumpspeed, rigidbody_of_player.velocity.y);
               
            }
            else
            {
                if (!animator_of_player.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
                {
                    rigidbody_of_player.velocity = new Vector2(horizontalmove * speed, rigidbody_of_player.velocity.y);
                }
                //rigidbody_of_player.velocity = new Vector2(Mathf.MoveTowards(rigidbody_of_player.velocity.x, horizontalmove * speed, 5f * Time.deltaTime), rigidbody_of_player.velocity.y);
                //currentSpeed = Mathf.MoveTowards(currentSpeed, speed, acceration * Time.deltaTime);

                if (facedirection == 0)
                {
                    //rigidbody_of_player.velocity = new Vector2(3f*horizontalmove, rigidbody_of_player.velocity.y);
                    rigidbody_of_player.velocity = new Vector2(Mathf.MoveTowards(horizontalmove, 1, 4f * Time.deltaTime), rigidbody_of_player.velocity.y);
                   
                }
               
            }
            //transform.localScale = new Vector3(horizontalmove, 1, 1);
            //rigidbody_of_player.velocity = new Vector2(horizontalmove * speed, rigidbody_of_player.velocity.y);
            animator_of_player.SetFloat("running", Mathf.Abs(facedirection));//让Animator中的running获取速度数值即facedirection，用mathf保证数值为正
        }
        

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);//获取player中的transform中的scale这个控制方向的变量
        }
   

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
                animator_of_player.SetBool("idle", false);
            }
             
        }
        else if (rigidbody_of_player.IsTouchingLayers(IsGround))
        {
            animator_of_player.SetBool("falling", false);
            animator_of_player.SetBool("idle", true);
            
        }
    }

   
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
<<<<<<< HEAD
        DashTimeLetf = Dashtime;
        LastDash = Time.time;
<<<<<<< HEAD
        //anim2.SetBool("effect", true);
        
=======
        CDImage.fillAmount = 1;
        //   anim2.SetBool("effect", true);
        Debug.Log("111111111111111");
>>>>>>> abfd55594285412f6a1245bcdb8d8befbd469473
    }
=======
>>>>>>> 7bf8f79a9c553aca976b11fcf54b5b2b067607b4


        float dashingGravity = rigidbody_of_player.gravityScale;
        rigidbody_of_player.gravityScale = 0f;

        rigidbody_of_player.velocity = new Vector2(transform.localScale.x * DashPower, 0);
        animator_of_player.SetBool("shift", true);
        //if (DashTimeLetf > 0)
        //{
        //    DashTimeLetf -= Time.deltaTime;
       // ShadowPool.instance.GetFormPool();
        //}
        //ShadowPool.instance.GetFormPool();
        //if (isDashing )
        //{
        //    ShadowPool.instance.GetFormPool();
        //}
        yield return new WaitForSeconds(Dashtime);

        rigidbody_of_player.gravityScale = dashingGravity;

        isDashing = false;

        if (rigidbody_of_player.IsTouchingLayers(IsGround))
        {
<<<<<<< HEAD
          
            if (DashTimeLetf > 0)
            {

                //transform.DOMoveX(Player.transform.position.x + 1 * rigidbody_of_player.transform.localScale.x,Dashtime);
                Player.transform.position = new Vector2(Player.transform.position.x + 0.07f * rigidbody_of_player.transform.localScale.x, Player.transform.position.y);
                //rigidbody_of_player.velocity = new Vector2(12f * rigidbody_of_player.transform.localScale.x, rigidbody_of_player.velocity.y);               
                
                animator_of_player.SetBool("shift", true);
              //  anim2.SetBool("effect", true);
                DashTimeLetf -= Time.deltaTime;
               
                ShadowPool.instance.GetFormPool();

                //RaycastHit2D hit;

                //Vector2 p1 = transform.position;
                //Vector2 p2 = p1 + Vector2.up * 0.5f;
                //if(Physics2D.CapsuleCast(p1,p2,0f,))
                //Physics2D.CapsuleCast()
                //if (Physics2D.CapsuleCast(p1, p2, 0f, transform.forward, out hit, 0.2f))
                //{

                //}
                //RaycastHit2D LeftshiftCheck = Raycast(new Vector2(rigidbody_of_player.transform.position.x, 0f), Vector2.left, 0.2f, IsGround);
                //RaycastHit2D RightshiftCheck = Raycast(new Vector2(rigidbody_of_player.transform.position.x, 0f), Vector2.right, 0.2f, IsGround);
                //if (LeftshiftCheck || RightshiftCheck)
                //{
                //    rigidbody_of_player.velocity = new Vector2(0, 0);
                //}
            }
            if (DashTimeLetf <= 0)
            {
                isDashing = false;
<<<<<<< HEAD
                //anim2.SetBool("effect", false);
=======
                Debug.Log("33333333333333");
       //         anim2.SetBool("effect", false);
>>>>>>> abfd55594285412f6a1245bcdb8d8befbd469473

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
=======
            animator_of_player.SetBool("shift", false);
            animator_of_player.SetBool("idle", true);
>>>>>>> 7bf8f79a9c553aca976b11fcf54b5b2b067607b4
        }
        else
        {
            animator_of_player.SetBool("shift", false);
            animator_of_player.SetBool("falling", true);
        }
        yield return new WaitForSeconds(DashCoolDown);
        canDash = true;
       

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
            //speed = jumpspeed;
            //if (!onGround)
            //{
            //    speed = jumpspeed;
            //}
            jumpcount--;
            animator_of_player.SetBool("jumping", true);          
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpcount == 0 && onGround)
        {
            StartCoroutine(StartCurve());
           // rigidbody_of_player.AddForce(new Vector2(-25f * rigidbody_of_player.transform.localScale.x, 0));
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

   
    //void shadow()
    //{
        
    //    DashTimeLetf = Dashtime;
    //    if (isDashing)
    //    {
    //        if (DashTimeLetf > 0)
    //        {
    //            DashTimeLetf -= Time.deltaTime;
    //            ShadowPool.instance.GetFormPool();
    //        }
    //    }
    //}
    //RaycastHit2D Raycast(Vector2 rayDiraction,float length,LayerMask layer)
    //{
    //    Vector2 pos = transform.position;

<<<<<<< HEAD
<<<<<<< HEAD
    void cdline()
    {
        CDImage.fillAmount = (Time.time - LastDash) / DashCoolDown;
        HPline.fillAmount = playerdata.currenthealth / playerdata.maxhealth;
    }

    public void Isinvincible()
    {
        if (IsinvIncible)
        {
            CurrectInvincibleTime -= Time.deltaTime;

        }
        if (CurrectInvincibleTime < 0)
        {
            IsinvIncible = false;

        }
    }
    public void changehp()
    {
        currectHealth = playerdata.currenthealth;
        if (IsinvIncible)
        {
            playerdata.currenthealth = currectHealth;
            
        }
    }


=======
=======
>>>>>>> 7bf8f79a9c553aca976b11fcf54b5b2b067607b4
    //    RaycastHit2D hit = Physics2D.Raycast(pos , rayDiraction, length, layer);
        
    //    Debug.DrawRay(pos , rayDiraction * length);

    //    return hit;
    //}
    //void ShiftCheck()
    //{
    //    RaycastHit2D LeftshiftCheck = Raycast( Vector2.left, 0.8f, IsGround);
    //    RaycastHit2D RightshiftCheck = Raycast( Vector2.right, 0.8f, IsGround);
    //    if (LeftshiftCheck || RightshiftCheck)
    //    {
            
    //        //rigidbody_of_player.velocity = new Vector2(0, 0);
    //    }
    //}
<<<<<<< HEAD
>>>>>>> abfd55594285412f6a1245bcdb8d8befbd469473
=======
>>>>>>> 7bf8f79a9c553aca976b11fcf54b5b2b067607b4
}