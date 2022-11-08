using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : MonoBehaviour
{

    public GameObject Ghost;
    public GameObject startPoint;
    public float generateTime =2f;
    private Animator anim;
    private Rigidbody2D rb;
    private int generatecount;

    void Start()
    {
        anim.Play("Appear");
        anim = Ghost.GetComponent<Animator>();
        rb = Ghost.GetComponent<Rigidbody2D>();
        generatecount = 0;
    }

    void Update()
    {

        generateTime -= Time.deltaTime;
        GenerateEnemy();
        
    }

    void GenerateEnemy()
    {

        if (generatecount < 3)
        {
            if (generateTime <= 0)
            {
                Instantiate(Ghost, startPoint.transform.position, transform.rotation);
                generateTime = 2;

            }
        }
        if (generateTime == 2)
        {
            generatecount += 1;
        }

        
        
    }

    
}
