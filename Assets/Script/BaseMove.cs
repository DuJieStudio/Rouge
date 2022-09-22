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

    private BoxCollider2D collder_of_player;
    private int toward;
    [Header("环境检测")]
    public float checkRadius;//检测地面偏移
    public LayerMask whatIsGround;//地面图层
    public int jumpcount;
    public Slider shiftline;
    public bool isGround;//是否在地面
    private bool shift_or_not;
    private float shifttime;//shift计时器


    [Header("显示残影的持续时间")]
    public float durationTime;
    [Header("生成残影与残影之间的时间间隔")]
    public float spawnTimeval;
    private float spawnTimer;//生成残影的时间计时器

    [Header("残影颜色")]
    public Color ghostColor;
    [Header("残影层级")]
    public int ghostSortingOrder;

    private SpriteRenderer sr;//SpriteRenderer
    private List<GameObject> ghostList = new List<GameObject>();//残影列表

    /// <summary>
    /// ///////////////////////////////////
    /// </summary>
     [Header("跳跃相关")]
    public AnimationCurve curve;
    public float Totaltime = 1f;
    public float fallMultiplier;
    public float JumpForce;//跳跃相关

    public float Dashtime;
    public AudioSource DashAudio;
    /// <summary>
    /// ////////////////////////////////////
    /// </summary>

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        shifttime = 0;
        shift_or_not = true;
        jumpcount = 2;
        toward = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
        speed = 0.03f;
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
        //float horizontalmove = Input.GetAxis("Horizontal"); //定义浮点型变量horizontalmove获取Axi中Horizontal（控制移动方向，值为1，0，-1及中间小数）的数值
        //float facedirection = Input.GetAxisRaw("Horizontal");//GetAxisRaw与GetAxis的区别，前者直接获取10-1三个数，后者可获取中间小数
        //if (horizontalmove != 0) //角色移动
        //{
        //    rigidbody_of_player.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rigidbody_of_player.velocity.y);
        //   // anim.SetFloat("running", Mathf.Abs(facedirection));//让Animator中的running获取速度数值即facedirection，用mathf保证数值为正
        //}
        //if (facedirection != 0)
        //{
        //    transform.localScale = new Vector3(facedirection, 1, 1);//获取player中的transform中的scale这个控制方向的变量
        //}
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed, Space.Self);
            Player.transform.localScale = new Vector3(-1, 1, 1);
            toward = -1;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed, Space.Self);
            Player.transform.localScale = new Vector3(1, 1, 1);
            toward = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpcount > 1)
            {
                //rigidbody_of_player.velocity = new Vector2(0, Mathf.Lerp(5, 10, 0.6f));
                //animator_of_player.Play("Jump");
                //jumpcount--;
                StartCoroutine(StartCurve());
                animator_of_player.Play("Jump");
                jumpcount--;
            }
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

                yield return null;
            }
        }

        IEnumerator dashtimer()
        {
            float time = 0;
            while (time <= Dashtime)
            {
                time += Time.deltaTime;
                if (toward == 1)
                {
                    Player.transform.position = new Vector2(Player.transform.position.x + 0.15f, Player.transform.position.y);
                }
                if (toward == -1)
                {
                    Player.transform.position = new Vector2(Player.transform.position.x - 0.15f, Player.transform.position.y);
                }              
                yield return null;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && toward != 0 && shift_or_not == true)
        {
            DashAudio.Play();
            StartCoroutine(dashtimer());
            animator_of_player.Play("shift");
            anim2.SetBool("effect",true);
            //if (toward == 1)
            //{
            //    Player.transform.position = new Vector2(Player.transform.position.x + 6f, Player.transform.position.y);
            //    animator_of_player.Play("shift");


            //}
            //if (toward == -1)
            //{

            //    Player.transform.position = new Vector2(Player.transform.position.x - 6f, Player.transform.position.y);
            //    animator_of_player.Play("shift");
            //}
            shift_or_not = false;

        }
        if (shift_or_not == false)
        {
            shifttime += Time.deltaTime;
            shiftline.value = shifttime / 5;
            if (shifttime >= 5)
            {
                shift_or_not = true;
                shifttime = 0;
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
    private void DrawGhost()
    {
        if (spawnTimer >= spawnTimeval)
        {
            spawnTimer = 0;

            GameObject _ghost = new GameObject();
            ghostList.Add(_ghost);
            _ghost.name = "ghost";
            _ghost.AddComponent<SpriteRenderer>();
            _ghost.transform.position = transform.position;
            _ghost.transform.localScale = transform.localScale;
            SpriteRenderer _sr = _ghost.GetComponent<SpriteRenderer>();
            _sr.sprite = sr.sprite;
            _sr.sortingOrder = ghostSortingOrder;
            _sr.color = ghostColor;

            
        }
        spawnTimer += Time.deltaTime;

    }


    private void Fade()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            SpriteRenderer ghostSR = ghostList[i].GetComponent<SpriteRenderer>();
            if (ghostSR.color.a <= 0)
            {
                GameObject tempGhost = ghostList[i];
                ghostList.Remove(tempGhost);
                Destroy(tempGhost);
            }
            else
            {
                float fadePerSecond = (ghostColor.a / durationTime);
                Color tempColor = ghostSR.color;
                tempColor.a -= fadePerSecond * Time.deltaTime;
                ghostSR.color = tempColor;
            }
        }
    }

    public void RunAudio()
    {


    }
}