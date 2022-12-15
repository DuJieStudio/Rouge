 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="New Item")]
public class Item_SO : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
    [TextArea]
    public string ItemInfo;
}
    