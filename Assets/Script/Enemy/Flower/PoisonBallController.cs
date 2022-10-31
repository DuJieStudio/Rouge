using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBallController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
