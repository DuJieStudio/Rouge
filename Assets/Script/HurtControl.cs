using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtControl : MonoBehaviour
{
    public PlayerData_SO playerdata;
    public GameObject player;
    
    public LayerMask trap;
    public SpriteRenderer sprite;
    [Header("无敌时间参数")]
    public bool ishurt;
    public bool Isinvincible=false;//是否为无敌状态
    public float invincibletime=1;//无敌时间
    public float invincibletimeleft=0;//无敌时间剩余时间
    [Header("受击反馈")]
    public int shakerate=10;//闪烁次数
    private float shaketime;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        shaketime = 1f / shakerate;
        
    }
    void Update()
    {
        
        ishurt = Physics2D.OverlapCircle(player.transform.position, 0.5f, trap);
        if (ishurt)
        {
            if(Isinvincible==false)
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
    public void shake()
    {
        if (invincibletimeleft % shaketime >= shaketime/2)
        {
            sprite.color = new Color(1, 1, 1, 0);
        }
        if (invincibletimeleft % shaketime < shaketime/2)
        { 
            sprite.color = new Color(1, 1, 1, 1);
        }





    }

}
