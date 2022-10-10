using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imprison_TrapController : MonoBehaviour
{
    public bool istrap;
    public LayerMask player;
    public GameObject mainplayer;
    public GameObject trap;
    public PlayerData_SO playerdata;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        istrap = Physics2D.OverlapCircle(trap.transform.position, 0.2f, player);
        if (istrap)
        { 
            mainplayer.transform.position = trap.transform.position;
            Invoke("Destroy",5); 
        }
        

        
    }
    public void Destroy()
    {
        istrap = false;
        Destroy(trap);
        

    }


}
