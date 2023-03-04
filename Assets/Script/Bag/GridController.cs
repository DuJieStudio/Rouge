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




    }
    public void changetext()
    {
        iteminfo.text = item.ItemInfo;
        GameObject.Find("Bag").GetComponent<ChooseItem>().item = item;
        GameObject.Find("Bag").GetComponent<ChooseItem>().thisgrid = ThisGrid;
        isequip = !isequip;
        equipitem();
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
            additemdata();
        }
        else
        {
            equip.SetActive(false);
            removeitemdata();
        }
    }
    public void additemdata()
    {
        if (item.ID == 0)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Attackdata.Power += 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackStats>().playerDataAttack.Power += 1;
        }
        if (item.ID == 1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().MaxHealth += 10;

        }
        if (item.ID == 2)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().MoveSpeed += 0.1f;
        }
        if (item.ID == 3)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Attackdata.MinDamage += 1;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Attackdata.MaxDamage += 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackStats>().playerDataAttack.MinDamage += 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackStats>().playerDataAttack.MaxDamage += 1;
        }
        

    }
    public void removeitemdata()
    {
        if (item.ID == 0)
        {
            // GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Attackdata.Power -= 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackStats>().playerDataAttack.Power -= 1;
        }
        if (item.ID == 1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().MaxHealth -= 10;

        }
        if (item.ID == 2)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().MoveSpeed -= 0.1f;
        }
        if (item.ID == 3)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Attackdata.MinDamage -= 1;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Attackdata.MaxDamage -= 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackStats>().playerDataAttack.MinDamage -= 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackStats>().playerDataAttack.MaxDamage -= 1;
        }
    }

}
