using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatHPBAR : MonoBehaviour
{
    public GameObject HPBar_PRE;
    public GameObject thisgameobj;
    public GameObject HPBar;
    public GameObject createpoint;
    public bool ishit;
    public float hiddentime;
    // Start is called before the first frame update
    void Start()
    {
        thisgameobj = this.gameObject;
        hiddentime = 0;
        HPBar= Instantiate(HPBar_PRE, createpoint.transform.position, thisgameobj.transform.rotation);
        HPBar.transform.SetParent(thisgameobj.transform);
        HPBar.SetActive(true);
        ishit = false;



    }
     void Update()
    {
        hideBar();

    }
    void hideBar()
    {
        hiddentime += Time.deltaTime;
        if (ishit)
        {
            HPBar.SetActive(true);
            ishit = false;
            hiddentime = 0;
        }
        if(hiddentime>=3)
        {
            HPBar.SetActive(false);
            ishit = false;
        }
        
    }
    public void setHit(bool hit)
    {
        ishit = hit;
    }

    // Update is called once per frame
}
