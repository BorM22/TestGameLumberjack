using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Trigger : MonoBehaviour
{
    public static bool trigger = false;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        
        trigger = true;
        
        }

       
    }

   
}
