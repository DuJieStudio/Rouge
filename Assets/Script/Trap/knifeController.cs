using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeController : MonoBehaviour
{
    public GameObject knife;
    private Vector2 direction;
    private float scale_direction;
    public Vector3 startarea;
    public GameObject trigger;
    public LayerMask player;
    public bool isfall;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GameObject.FindGameObjectWithTag("trigger");
        startarea = knife.transform.position;
        isfall = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(trigger.transform.position, 0.2f, player))
        { 
            isfall = true;
        }
        if (!isfall)
        { knife.transform.position = startarea; }
        else
        {

            knife.transform.Translate(new Vector2(0, Mathf.Lerp(-1, 0.05f, -5)));
            Destroy(knife, 2);
        }     
    }
}
