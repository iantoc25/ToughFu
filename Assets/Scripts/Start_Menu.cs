using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Start_Menu : MonoBehaviour {
    
   
  

    public void quit()
    {
        Application.Quit();
    }

    public void volum ()
    {
        AudioListener.pause = !AudioListener.pause; 
        
    }

      public void meniu ()
    {
        SceneManager.LoadScene("Start menu");

    }

   
}
