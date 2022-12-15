using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUPItem : MonoBehaviour
{
    public Item_SO thisitem;
    public Bag bag;
    public bool ispick=false;
    public GameObject PlaceOFGird;
    public GameObject gridPerfab;
    public GameObject pre_grid;

    public void Start()
    {
        for (int i = 0; i<=bag.itemlist.Count-1; i++)
        {
            if (bag.itemlist[i] != null)
            { thisitem = bag.itemlist[i]; }
            if (thisitem != null)
            { CreateGrid();} 
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Item"&&bag.itemlist.Count<6)
        {
            

            thisitem = collision.gameObject.GetComponent<ChooseData>().item;
            AddItem();
            Destroy(collision.gameObject);
            

        }
    } 
    public void AddItem()
    {

            bag.itemlist.Add(thisitem);
            CreateGrid();

        

    }
    public void CreateGrid()
    {
        pre_grid = Instantiate(gridPerfab, PlaceOFGird.transform.position, Quaternion.identity);
        pre_grid.GetComponent<GridController>().item = thisitem;
        pre_grid.transform.SetParent(PlaceOFGird.transform, false);

    }
   


}
