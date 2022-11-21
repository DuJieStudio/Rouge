using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatHPBAR : MonoBehaviour
{
    public GameObject HPBar_PRE;
    public GameObject thisgameobj;
    // Start is called before the first frame update
    void Start()
    {
        thisgameobj = this.gameObject;
        Instantiate(HPBar_PRE, thisgameobj.transform.position, thisgameobj.transform.rotation).transform.SetParent(thisgameobj.transform);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
