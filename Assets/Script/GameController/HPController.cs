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
    [Header("受击反馈")]
    public int shakerate = 10;//闪烁次数
    private float shaketime;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        shaketime = 1f / shakerate;
        playerdata.currenthealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        shake();
        invincible();
        changeHPline();
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
        ishurt = Physics2D.OverlapCircle(Player.transform.position, 0.5f, trap);
        if (ishurt)
        {
            if (Isinvincible == false)
            {
                playerdata.currenthealth -= 10;
                invincibletimeleft = invincibletime;

            }



        }
        if (invincibletimeleft > 0)
        {
            Isinvincible = true;
            invincibletimeleft -= Time.deltaTime;
            shake();
        }
        else
        {
            Isinvincible = false;
        }
    }

    public void changeHPline()
    {
        HPline.fillAmount = playerdata.currenthealth / playerdata.maxhealth;
    }
}
