using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="itempool",menuName ="createitempool")]
public class itempool_SO : ScriptableObject 
{
    public List<Item_SO> itemlist = new List<Item_SO>();
}
