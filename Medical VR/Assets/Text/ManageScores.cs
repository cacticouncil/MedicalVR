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
	void Update () {

        MemoryScoreText.GetComponent<TextMeshPro>().text = "Memory Game: " + MemoryUI.finalScore.ToString();
        SimonDNAScoreText.GetComponent<TextMeshPro>().text = "Simon DNA: ";
        cGAMPScoreText.GetComponent<TextMeshPro>().text = "cGAMP Snatcher: " + Storebullets.finalScore.ToString();
        DodgeScoreText.GetComponent<TextMeshPro>().text = "ATP/GTP Shooter: " + _TGameController.finalATPScore.ToString();
        VirusScoreText.GetComponent<TextMeshPro>().text = "";
        CellScoreText.GetComponent<TextMeshPro>().text = "";
        StrategyScoreText.GetComponent<TextMeshPro>().text = "";
        ATPScoreText.GetComponent<TextMeshPro>().text = "";

        FBscript.GlobalScore = ((int)MemoryUI.finalScore + (int)Storebullets.finalScore + (int)_TGameController.finalATPScore) % 1000;

    }
}
