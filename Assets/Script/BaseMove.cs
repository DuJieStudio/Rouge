using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMove : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    private Rigidbody2D rigidbody_of_player;
    private Animator animator_of_player;
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
    public CharacterStats PlayerData;//获取playdata里的数据
    public Slider Healthline;
    public BoxCollider2D enemy;

    private SpriteRenderer sr;//SpriteRenderer
    private List<GameObject> ghostList = new List<GameObject>();//残影列表
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
        speed =PlayerData.MoveSpeed;
        rigidbody_of_player = GetComponent<Rigidbody2D>();
        animator_of_player = GetComponent<Animator>();
        collder_of_player = GetComponent<BoxCollider2D>();
        PlayerData.CurrentHealth = 100;


    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Isground();
        change_health_line();
        test1();

    }
    private void Move()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.transform.Translate(Vector3.left * speed, Space.Self);
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
                rigidbody_of_player.velocity = new Vector2(0, Mathf.Lerp(5, 10, 0.6f));
                animator_of_player.Play("Jump");
                jumpcount--;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && toward != 0 && shift_or_not == true)
        {

            if (toward == 1)
            {
                Player.transform.position = new Vector2(Player.transform.position.x + 6f, Player.transform.position.y);
                animator_of_player.Play("shift");


            }
            if (toward == -1)
            {

                Player.transform.position = new Vector2(Player.transform.position.x - 6f, Player.transform.position.y);
                animator_of_player.Play("shift");
            }
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
    private void change_health_line()
    {
        Healthline.value = PlayerData.CurrentHealth/100;
    }
    private void test1()//用于测试血条是否变更，无意义可删除
    {
        if (rigidbody_of_player.IsTouching(enemy))
        {
            PlayerData.CurrentHealth -= 10;
            rigidbody_of_player.AddForce(new Vector2(-1, 0) * 10f);
        }
    }




}