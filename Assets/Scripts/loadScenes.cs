using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScenes : MonoBehaviour
{
     public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

     public void ExitScene()
    {
        SceneManager.LoadScene("Demo Circle Menu", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
