using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public Image ItemSprite;
    public Text Itemcount;
    public GameObject ThisGrid;
    public Item_SO item;
    public Bag bag;
    public Text iteminfo;

    public void Awake()
    {
        ThisGrid = this.gameObject;
        item = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUPItem>().thisitem;
        iteminfo =GameObject.Find("Iteminfo").GetComponent<Text>();
        bag = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUPItem>().bag;
    }
    public void Start()
    {
          ItemSprite.sprite = item.ItemSprite;
            Itemcount.text = item.ItemCount.ToString();
        
    }
    // Update is called once per frame
    public void Update()
    {
        ItemSprite.sprite = item.ItemSprite;
        Itemcount.text = item.ItemCount.ToString();
        
        

    }
    public void changetext()
    {
        iteminfo.text = item.ItemInfo;
        GameObject.Find("Bag").GetComponent<DestoryItem>().destroyitem = item;
        GameObject.Find("Bag").GetComponent<DestoryItem>().destroythisgrid = ThisGrid;
    }
    public void destoryitem()
    {
        Destroy(ThisGrid);
        bag.itemlist.Remove(item);
    }

}
