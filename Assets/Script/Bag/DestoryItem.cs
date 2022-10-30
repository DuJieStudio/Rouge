using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryItem : MonoBehaviour
{
    public Item_SO destroyitem;
    public GameObject destroythisgrid;
    public Bag bag;
    public void destorygrid()
    {
        bag.itemlist.Remove(destroyitem);
        Destroy(destroythisgrid);
        }

}
