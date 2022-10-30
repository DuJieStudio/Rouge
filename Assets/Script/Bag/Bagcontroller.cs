using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bagcontroller : MonoBehaviour
{
    public GameObject Bag;
    public bool IsUsingBag;
    // Start is called before the first frame update
    void Start()
    {
        Bag = GameObject.FindGameObjectWithTag("Bag");
        IsUsingBag = false;
         Bag.SetActive(IsUsingBag);
    }

    // Update is called once per frame
    void Update()
    {

        if (Bag.active == false)
        {
            IsUsingBag = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsUsingBag = !IsUsingBag;
            Bag.SetActive(IsUsingBag);
        }
        
            
        
        
    }
}
