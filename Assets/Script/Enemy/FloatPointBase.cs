using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPointBase : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
