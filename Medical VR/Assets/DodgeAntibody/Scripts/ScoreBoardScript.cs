using UnityEngine;
using System.Collections;

public class ScoreBoardScript : MonoBehaviour {

    public GameObject score1, score2, score3, score4, score5, currentScore, EnterName;
    public GameObject objectWithScore;
    public string currName, minigameTag;
    int[] topScores = new int[5] { 0, 0, 0, 0, 0 };
    string[] topNames = new string[5] { "AAA", "AAA", "AAA", "AAA", "AAA" };

    int score, savedIndex;
    bool makeName;
    // Use this for initialization
    void Start ()
    {
        
	}
	public void GenerateScore()
    {
        switch (minigameTag)
        {
            case "Dodge":
                score = (int)objectWithScore.GetComponent<MovingCamera>().score;
                break;
            case "Transcription":
                score = (int)objectWithScore.GetComponent<MemoryUI>().score;
                break;
            case "Simon":
                score = (int)objectWithScore.GetComponent<SimonSays>().score;
                break;
            case "FightVirus":
                score = (int)objectWithScore.GetComponent<Player>().CurrentScore;
                    break;
            default:
                break;
        }
        
        LoadScore();
        SortScore();
          
    }
	// Update is called once per frame
	void Update ()
    {
	    
	}
    void SortScore()
    {
        makeName = false;
        for(int i = 0; i < 5; i++)
        {
            if(score > topScores[i])
            {
                makeName = true;
                for(int j = 4; j >= i; j--)
                {
                    if(j == i)
                    {
                        topScores[j] = score;
                        savedIndex = j;
                    }
                    else
                    {
                        topScores[j] = topScores[j - 1];
                        topNames[j] = topNames[j - 1];
                    }
                }
                break;
            }
        }
        if(makeName == true)
        {
            EnterName.transform.position = transform.position;
            transform.position = new Vector3(10000, 10000, 10000);
            EnterName.SetActive(true);
            
        }
        else
        {
            DisplayScore();
        }
        
    }
    public void PlaceName()
    {
        topNames[savedIndex] = currName;
        DisplayScore();
    }
    void SaveScore()
    {
        for(int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(minigameTag+"_Score" + i.ToString(), topScores[i]);
            PlayerPrefs.SetString(minigameTag+"_Name"+i.ToString(), topNames[i]);
        }    
    }
    void LoadScore()
    {
        for (int i = 0; i < 5; i++)
        {
            topScores[i] = PlayerPrefs.GetInt(minigameTag + "_Score" + i.ToString());
            topNames[i] = PlayerPrefs.GetString(minigameTag + "_Name" + i.ToString());
        }
    }
    void DisplayScore()
    {
        
        score1.GetComponent<TMPro.TextMeshPro>().text = "1. " + topNames[0] + ": " + topScores[0];
        score2.GetComponent<TMPro.TextMeshPro>().text = "2. " + topNames[1] + ": " + topScores[1];
        score3.GetComponent<TMPro.TextMeshPro>().text = "3. " + topNames[2] + ": " + topScores[2];
        score4.GetComponent<TMPro.TextMeshPro>().text = "4. " + topNames[3] + ": " + topScores[3];
        score5.GetComponent<TMPro.TextMeshPro>().text = "5. " + topNames[4] + ": " + topScores[4];
        currentScore.GetComponent<TMPro.TextMeshPro>().text = "Your Score: " + score.ToString();
        SaveScore();
    }
}
