using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseData : MonoBehaviour
{

    public Item_SO item;

    public string ItemName
    {
        get
        {
            if (item != null)
            { return item.ItemName; }
            else
            { return null; }
        }
    }
    public Sprite ItemSprite
    {
        get
        {
            if (item != null)
            { return item.ItemSprite; }
            else
            { return null; }
        }
    }



    public string ItemInfo
    {
        get
        {
            if (item != null)
            { return item.ItemInfo; }
            else
            { return null; }
        }
    }



}
