using UnityEngine;
using System.Collections;
using TMPro;

public class ManageScores : MonoBehaviour {

    public GameObject MemoryScoreText;
    public GameObject SimonDNAScoreText;
    public GameObject cGAMPScoreText;
    public GameObject DodgeScoreText;
    public GameObject VirusScoreText;
    public GameObject CellScoreText;
    public GameObject StrategyScoreText;
    public GameObject ATPScoreText;


    // Use this for initialization
    void Start () {
	
	}

    
	
	// Update is called once per frame
	void OnEnable () {

        MemoryScoreText.GetComponent<TextMeshPro>().text = "Memory Game: " + MemoryUI.finalScore.ToString();
        SimonDNAScoreText.GetComponent<TextMeshPro>().text = "Simon DNA: " + SimonSays.finalScore.ToString();
        cGAMPScoreText.GetComponent<TextMeshPro>().text = "cGAMP Snatcher: " + Storebullets.finalScore.ToString();
        DodgeScoreText.GetComponent<TextMeshPro>().text = "Dodge Antibodies: " + MovingCamera.finalScore.ToString();
        VirusScoreText.GetComponent<TextMeshPro>().text = "Fight Virus: " + Player.BestScoreForFightVirus.ToString();
        CellScoreText.GetComponent<TextMeshPro>().text = "Destroy Cell: " + VirusPlayer.FinalScore.ToString();
        StrategyScoreText.GetComponent<TextMeshPro>().text = "Strategy Game: " + StrategyCellManagerScript.finalScore.ToString();
        ATPScoreText.GetComponent<TextMeshPro>().text = "ATP/GTP Shooter: " + _TGameController.finalATPScore.ToString();

        FBscript.GlobalScore = ((int)MemoryUI.finalScore + (int)Storebullets.finalScore + (int)_TGameController.finalATPScore + (int)SimonSays.finalScore + (int)MovingCamera.finalScore + (int)Player.BestScoreForFightVirus + (int)VirusPlayer.FinalScore + StrategyCellManagerScript.finalScore);

    }
}
