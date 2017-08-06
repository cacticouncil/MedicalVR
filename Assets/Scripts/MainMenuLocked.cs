using UnityEngine;
using System.Collections;
using TMPro;

public class MainMenuLocked : MonoBehaviour
{
    public GameObject CellGameplayGameObject;
    public GameObject TrophyRoomGameObject;

    public GameObject StrategyGameGameObject;
    public GameObject MiniGameGameObject;
    public GameObject CreditGameObject;

	void Awake ()
    {
        //Show that cell gameplay is locked 
        if (GlobalVariables.VirusGameplayCompleted == 0)
        {
            CellGameplayGameObject.GetComponentInChildren<TextMeshPro>().text = "Cell Story Mode - Locked" + "\n" + "Finish Virus Gameplay";
            CellGameplayGameObject.GetComponent<BoxCollider>().enabled = false;
            CellGameplayGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);

            TrophyRoomGameObject.GetComponentInChildren<TextMeshPro>().text = "Trophy Room - Locked" + "\n" + "Finish Virus Gameplay";
            TrophyRoomGameObject.GetComponent<BoxCollider>().enabled = false;
            TrophyRoomGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);
        }
        else
        {
            CellGameplayGameObject.GetComponentInChildren<TextMeshPro>().text = "Cell Story Mode";
            TrophyRoomGameObject.GetComponentInChildren<TextMeshPro>().text = "Trophy Room";
        }

        //Show that strategy and mini games are locked 
        if (GlobalVariables.CellGameplayCompleted == 0)
        {
            StrategyGameGameObject.GetComponentInChildren<TextMeshPro>().text = "Strategy Game - Locked" + "\n" + " Finish both Virus and Cell Gameplay";
            StrategyGameGameObject.GetComponent<BoxCollider>().enabled = false;
            StrategyGameGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);

            MiniGameGameObject.GetComponentInChildren<TextMeshPro>().text = "Minigames - " + "\n" + "Locked" + "\n" + "Finish both Virus and Cell Gameplay";
            MiniGameGameObject.GetComponent<BoxCollider>().enabled = false;
            MiniGameGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);

            CreditGameObject.GetComponentInChildren<TextMeshPro>().text = "Credits - " + "\n" + "Locked" + "\n" + "Finish both Virus and Cell Gameplay";
            CreditGameObject.GetComponent<BoxCollider>().enabled = false;
            CreditGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);
        }
        else
        {
            StrategyGameGameObject.GetComponentInChildren<TextMeshPro>().text = "Strategy Game";
            MiniGameGameObject.GetComponentInChildren<TextMeshPro>().text = "Minigames";
            CreditGameObject.GetComponentInChildren<TextMeshPro>().text = "Credits";
        }
    }
}
