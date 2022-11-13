using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    public Image HPline;
    public LayerMask trap;
    public GameObject Player;
    public PlayerData_SO playerdata;
    public SpriteRenderer sprite;
    [Header("无敌时间参数")]
    public bool ishurt;
    public bool Isinvincible = false;//是否为无敌状态
    public float invincibletime = 1;//无敌时间
    public float invincibletimeleft = 0;//无敌时间剩余时间
    public float ThisTimeHP;//当前时间血量
    public float LastTimeHP;//上一间隔时血量
    public float RateOfHP;//记录生命值的间隙
    public float passtime;
    [Header("受击反馈")]
    public int shakerate = 10;//闪烁次数
    private float shaketime;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        shaketime = 1f / shakerate;
        playerdata.currenthealth = playerdata.maxhealth;
        RateOfHP = 0.01f;
        ThisTimeHP = playerdata.maxhealth;
        LastTimeHP = playerdata.maxhealth;
        Isinvincible = false;
        passtime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        IsLosingHP();
        invincible();
        changeHPline();
        istouchingTrap();
    }
    public void shake()
    {
        if (invincibletimeleft % shaketime >= shaketime / 2)
        {
            sprite.color = new Color(1, 1, 1, 0);
        }
        if (invincibletimeleft % shaketime < shaketime / 2)
        {
            sprite.color = new Color(1, 1, 1, 1);
        }
    }
    public void invincible()
    {
        if (ishurt)
        {
            if (Isinvincible == false)
            {
                invincibletimeleft = invincibletime;

            }
        }
        if (invincibletimeleft > 0)
        {
            Isinvincible = true;
            invincibletimeleft -= Time.deltaTime;
            playerdata.currenthealth = ThisTimeHP;
            LastTimeHP = ThisTimeHP;
            shake();
        }
        if (invincibletimeleft <= 0)
        {
            Isinvincible = false;
            ishurt = false;
        }
    }

    public void changeHPline()
    {
        HPline.fillAmount = playerdata.currenthealth / playerdata.maxhealth;
    }
    public void IsLosingHP()
    {

        bool i = true;
        if (ThisTimeHP > LastTimeHP)
        {
            float a = ThisTimeHP;
            ThisTimeHP = LastTimeHP;
            LastTimeHP = a;
        }
        if (ThisTimeHP < LastTimeHP && ishurt == false)
        {
            ishurt = true;
        }
        if (passtime > RateOfHP)
        {
            passtime = 0;
            i = !i;
        }
        if (Isinvincible == false)
        {
            if (i == true)
            {
                ThisTimeHP = playerdata.currenthealth;

            }
            if (i == false)
            {
                LastTimeHP = playerdata.currenthealth;

            }
            passtime += 0.001f;
        }







    }
    public void istouchingTrap()
    {
        if (Physics2D.OverlapCircle(Player.transform.position, 0.5f, trap) && Isinvincible == false)
        {
            playerdata.currenthealth -= 3;
        }
    }
}

