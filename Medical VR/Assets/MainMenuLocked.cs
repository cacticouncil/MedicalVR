using UnityEngine;
using System.Collections;
using TMPro;
public class MainMenuLocked : MonoBehaviour
{
    bool DisableAllScenes = true;
    public GameObject CellGameplayScene;
    public GameObject CellGameplayGameObject;

    public GameObject MiniGameScene;
    public GameObject MiniGameGameObject;

    public GameObject StrategyGameScene;
    public GameObject StrategyGameGameObject;

	void Start ()
    {
        ////Debug to start the scenes locked or unlocked
        //if (DisableAllScenes == true)
        //{
        //    Lock();
        //}

        //else if (DisableAllScenes == false)
        //{
        //    Unlock();
        //}

        //Show that cell gameplay is locked 
        if (GlobalVariables.VirusGameplayCompleted == 0)
        {
            CellGameplayScene.GetComponent<TextMeshPro>().text = "Cell Story Mode - Locked" + "\n" + "Finish Virus Gameplay";
            CellGameplayGameObject.GetComponent<BoxCollider>().enabled = false;
            CellGameplayGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);
        }

        else if (GlobalVariables.VirusGameplayCompleted == 1)
        {
            CellGameplayScene.GetComponent<TextMeshPro>().text = "Cell Story Mode";
        }

        //Show that strategy and mini games are locked 
        if (GlobalVariables.CellGameplayCompleted == 0)
        {
            StrategyGameScene.GetComponent<TextMeshPro>().text = "Strategy Game - Locked" + "\n" + " Finish both Virus and Cell Gameplay";
            StrategyGameGameObject.GetComponent<BoxCollider>().enabled = false;
            StrategyGameGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);

            MiniGameScene.GetComponent<TextMeshPro>().text = "Minigames - " + "\n" + "Locked" + "\n" + "Finish both Virus and Cell Gameplay";
            MiniGameGameObject.GetComponent<BoxCollider>().enabled = false;
            MiniGameGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);
        }

        else if (GlobalVariables.CellGameplayCompleted == 1)
        {
            StrategyGameScene.GetComponent<TextMeshPro>().text = "Strategy Game";
            MiniGameScene.GetComponent<TextMeshPro>().text = "Minigames";
        }
    }

    void Lock()
    {
        GlobalVariables.VirusGameplayCompleted = 0;
        GlobalVariables.CellGameplayCompleted = 0;
    }

    void Unlock()
    {
        GlobalVariables.VirusGameplayCompleted = 1;
        GlobalVariables.CellGameplayCompleted = 1;
    }
}
