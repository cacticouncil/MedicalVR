using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float Score;
    public float Health;
    float RuleTimer;
    bool DisplayRules;
    GameObject ScoreObj;
    GameObject RulesObj;

    void Start()
    {
        Score = 0.0f;
        Health = 100.0f;
        RuleTimer = 0.0f;
        ScoreObj = GameObject.Find("Score");
        RulesObj = GameObject.Find("Rules");
        DisplayRules = true;
    }

    void Update()
    {
        ScoreObj.GetComponent<TextMesh>().text = "Score " + Score.ToString();
        if (RuleTimer >= 20.0f)
            DisplayRules = false;

        if (DisplayRules)
            RulesObj.GetComponent<TextMesh>().text = "Defeat the Virus";
        else
            RulesObj.GetComponent<TextMesh>().text = "";


        RuleTimer += Time.time;
    }
}
