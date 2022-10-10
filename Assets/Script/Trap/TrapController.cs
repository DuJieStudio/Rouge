using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public GameObject fireball;
    [Header("Éä»÷ÊôÐÔ")]
    public float firerate = 1f;
    public float nextfire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            Instantiate(fireball, transform.position, transform.rotation);
        }
        
    }
}
