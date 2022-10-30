using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="BAG",menuName ="CreateNewBag")]
public class Bag : ScriptableObject
{
    public List<Item_SO> itemlist = new List<Item_SO>();
}
