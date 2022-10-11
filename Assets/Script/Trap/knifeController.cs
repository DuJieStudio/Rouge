using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeController : MonoBehaviour
{
    public GameObject rope;
    public GameObject knife;
    private Vector2 direction;
    private float scale_direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(0, -0.02f);
        scale_direction = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        knife.transform.Translate(direction);
        rope.transform.localScale = new Vector3(1, rope.transform.localScale.y+scale_direction, 1);
        if (knife.transform.position.y >= 5)
        { 
            direction = new Vector2(0, -0.02f);
            scale_direction = 0.02f;

            
        }
        if (knife.transform.position.y <= 0)
        { direction = new Vector2(0, 0.02f);
            scale_direction = -0.02f;
        }



    }
}
