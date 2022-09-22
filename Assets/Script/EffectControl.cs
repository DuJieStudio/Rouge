using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{

    public Animator anim;
    void Start()
    {
        

    }

    
    void Update()
    {
      
    }

   

    public void StopEffect()
    {

        anim.SetBool("effect",false);
        
    }
}
