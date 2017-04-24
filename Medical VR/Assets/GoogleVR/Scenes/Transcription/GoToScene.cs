using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour {
   public void GoToStoryMemory()
    {
        MemoryUI.arcadeMode = false;
        MemoryUI.TutorialMode = false;
        SceneManager.LoadScene("MemoryGame");
    }

   public void GoToStoryCGampSnatcher()
    {
        Storebullets.arcadeMode = false;
        Storebullets.TutorialMode = false;
        SceneManager.LoadScene("CGampSnatcher");
    }

}
