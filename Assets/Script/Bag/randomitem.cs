using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomitem : MonoBehaviour
{
    public itempool_SO itempool;
    public Item_SO thisitem;
    public int i;
    public int level;
    public SpriteRenderer sprite;
    public LayerMask ground;
    public bool istouching;
    // Start is called before the first frame update
    public void Start()
    {
        i = Random.Range(0, itempool.itemlist.Count);
        level = Random.Range(1, 5);
        thisitem = itempool.itemlist[i];
        GetComponent<ChooseData>().item = thisitem;
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = thisitem.ItemSprite;

    }
    // Update is called once per frame
    void Update()
    {

        isground();
    }
    void isground()
    {
        if (Physics2D.OverlapCircle(this.transform.position, 0.8f, ground)==false)
        { this.gameObject.transform.Translate(new Vector2(0, -2f) * Time.deltaTime * Mathf.Lerp(1, 2, 0.5f)); }
        else if (!istouching)
        { this.gameObject.transform.Translate(new Vector2(0, -0.3f) * Time.deltaTime * Mathf.Lerp(1, 2, 0.5f)); }//Ä£ÄâÏÂÂä
        else if (istouching)
        { this.gameObject.transform.Translate(new Vector2(0, 0.3f) * Time.deltaTime * Mathf.Lerp(1, 2, 0.5f)); }

        if (Physics2D.OverlapCircle(this.transform.position, 0.5f, ground))
        {
            istouching = true;
        }
        if (Physics2D.OverlapCircle(this.transform.position, 0.8f, ground)==false)
        {
            istouching = false;
        }
    }
    
}
