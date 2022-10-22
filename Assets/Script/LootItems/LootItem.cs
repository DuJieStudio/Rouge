using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    //protected Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUp();
    }

    protected virtual void PickUp()
    {

    }
}
