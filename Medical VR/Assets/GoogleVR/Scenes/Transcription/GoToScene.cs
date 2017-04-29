using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour {
   public void GoToStoryMemory()
    {
        
        if(MemoryUI.arcadeMode == false)
        MemoryUI.arcadeMode = false;
        else
        MemoryUI.arcadeMode = true;

        GlobalVariables.tutorial = false;
        SceneManager.LoadScene("MemoryGame");
    }

   public void GoToStoryCGampSnatcher()
    {
        if(Storebullets.arcadeMode == false)
        Storebullets.arcadeMode = false;
        else
            Storebullets.arcadeMode = true;

        GlobalVariables.tutorial = false;
        SceneManager.LoadScene("CGampSnatcher");
    }

}
