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

        MemoryScoreText.GetComponent<TextMeshPro>().text = "Memory Game: "; //MemoryUI.score.ToString();
        SimonDNAScoreText.GetComponent<TextMeshPro>().text = "";
        cGAMPScoreText.GetComponent<TextMeshPro>().text = "cGAMP Snatcher: " + Storebullets.score.ToString();
        DodgeScoreText.GetComponent<TextMeshPro>().text = "";
        VirusScoreText.GetComponent<TextMeshPro>().text = "";
        CellScoreText.GetComponent<TextMeshPro>().text = "";
        StrategyScoreText.GetComponent<TextMeshPro>().text = "";
        ATPScoreText.GetComponent<TextMeshPro>().text = "";

        //FBscript.GlobalScore = ((int)MemoryUI.score + (int)Storebullets.score) % 1000;

    }
}
