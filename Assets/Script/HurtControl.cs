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
    [Header("�޵�ʱ�����")]
    public bool ishurt;
    public bool Isinvincible=false;//�Ƿ�Ϊ�޵�״̬
    public float invincibletime=1;//�޵�ʱ��
    public float invincibletimeleft=0;//�޵�ʱ��ʣ��ʱ��
    [Header("�ܻ�����")]
    public int shakerate=10;//��˸����
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
