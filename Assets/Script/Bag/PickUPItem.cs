using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUPItem : MonoBehaviour
{
    public Item_SO thisitem;
    public Bag bag;
    public CreatNewItem cni;
    public bool ispick=false;

    public void Start()
    {
        for (int i = 0; i< 7; i++)
        {
            thisitem = bag.itemlist[i];
            cni.CreatGrid();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Item")
        {

           
            thisitem = collision.gameObject.GetComponent<ChooseData>().item;
            AddItem();
            

        }
    } 
    public void AddItem()
    {
        if (!bag.itemlist.Contains(thisitem))
        {
            bag.itemlist.Add(thisitem);
            cni.CreatGrid();
            
        }
        else
        {
            thisitem.ItemCount +=1;
            
        }

    }
   


}
