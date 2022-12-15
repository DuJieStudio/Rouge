using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public Image ItemSprite;
    public GameObject ThisGrid;
    public Item_SO item;
    public Bag bag;
    public Text iteminfo;
    public GameObject equip;
    public bool isequip;
    public void Start()
    {
        ThisGrid = this.gameObject;
        iteminfo =GameObject.Find("Iteminfo").GetComponent<Text>();
        bag = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUPItem>().bag;
        ItemSprite.sprite = item.ItemSprite;
        isequip = false;
    }
    // Update is called once per frame
    public void Update()
    {
        equipitem();




    }
    public void changetext()
    {
        iteminfo.text = item.ItemInfo;
        GameObject.Find("Bag").GetComponent<ChooseItem>().item = item;
        GameObject.Find("Bag").GetComponent<ChooseItem>().thisgrid = ThisGrid;
        isequip = !isequip;
    }
    //public void destoryitem()
    //{
    //    Destroy(ThisGrid);
    //    bag.itemlist.Remove(item);
    //}
    public void equipitem()
    {
        if (isequip)
        {
            equip.SetActive(true);
        }
        else
        {
            equip.SetActive(false);
        }
    }

}
