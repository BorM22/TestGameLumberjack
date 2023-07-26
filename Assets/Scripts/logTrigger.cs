using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logTrigger : MonoBehaviour
{
    public GameObject logObject;
    public static bool logPlus = false;
     public static int logCount = 0;

 void Start()
    {
        
       
      
    }



     void OnCollisionEnter(Collision collision)
    {
     if(collision.gameObject.CompareTag("Player"))
     {
        logCount++;
        Destroy(logObject);
        logPlus = true;
     }
    }


    void ResetLogCount()
    {
      logCount = 0;
      
    }

   
}
