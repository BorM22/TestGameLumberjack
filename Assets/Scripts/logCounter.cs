using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logCounter : MonoBehaviour
{
    public Text logCountText;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(logTrigger.logPlus)
        {
            logCountText.text = "" + logTrigger.logCount;
        }
    }
}
