using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatNewItem : MonoBehaviour
{
    public GameObject PlaceOFGird;
    public GameObject gridPerfab;
    public PickUPItem pickupitem;
    public void CreatGrid()
    {
        
        GameObject newitems= Instantiate(gridPerfab, PlaceOFGird.transform.position,Quaternion.identity);
        newitems.transform.SetParent(PlaceOFGird.transform, false);
        
        
    }
 
}
