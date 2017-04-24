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

        MemoryUI.TutorialMode = false;
        SceneManager.LoadScene("MemoryGame");
    }

   public void GoToStoryCGampSnatcher()
    {
        if(Storebullets.arcadeMode == false)
        Storebullets.arcadeMode = false;
        else
            Storebullets.arcadeMode = true;

        Storebullets.TutorialMode = false;
        SceneManager.LoadScene("CGampSnatcher");
    }

}
