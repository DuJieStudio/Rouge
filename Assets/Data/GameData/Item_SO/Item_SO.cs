 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="New Item")]
public class Item_SO : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
    public int ItemCount;
    [TextArea]
    public string ItemInfo;
    public bool equip;

}
    