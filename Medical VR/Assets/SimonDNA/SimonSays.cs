using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimonSays : MonoBehaviour
{
    public static float finalScore;
 
    public GameObject yellow;
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject sign, UI, scoreBoard, theScore, theLives, polyLeft, GO, gameTimer, Username, ProfilePic;

    public List<AudioClip> sounds;
    public int score = 0, polys = 0, polysDone;
    Color cY, cR, cB, cG;

    int round = 1, inputed = 0, shownSign = 0, scoreCombo = 1;

    float timer = 0, theTimer, goTimer =0;

    //Vector3 orgPos;
    bool makeInput = false, buttonPressed = false, showStuff = false, waitAsec = false, showGo = false;
    ///[System.Serializable]
    public enum theColors
    {
        YELLOW,
        RED,
        BLUE,
        GREEN
    }

    public theColors selectedColor;
    public bool makeNuclei = false;
    public int lives = 3;
    List<theColors> theList = new List<theColors>();
    List<theColors> inputedColors = new List<theColors>();
    void Start ()
    {
        sign.GetComponent<MeshRenderer>().enabled = false;
        sign.GetComponentInChildren<TMPro.TextMeshPro>().text = " ";
        //orgPos = transform.position;
        cY = yellow.GetComponent<Renderer>().material.color;
        cR = red.GetComponent<Renderer>().material.color;
        cB = blue.GetComponent<Renderer>().material.color;
        cG = green.GetComponent<Renderer>().material.color;
        selectedColor = (theColors)5;
        theScore.GetComponent<TMPro.TextMeshPro>().text = "Score: " + score.ToString();
        theLives.GetComponent<TMPro.TextMeshPro>().text = "Lives: " + lives.ToString();
        gameTimer.GetComponent<TMPro.TextMeshPro>().text = "TIMER " + theTimer.ToString();
        theTimer = 30;
        polys = 21;
        polysDone = 0;
        if(GlobalVariables.arcadeMode == false)
        {
            polyLeft.SetActive(true);
            polyLeft.GetComponent<TMPro.TextMeshPro>().text = "Nucleids Done: " + polysDone.ToString();
            theScore.GetComponent<TMPro.TextMeshPro>().text = "Goal: " + polys.ToString();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (lives > 0)
        {
            if(score >= 500)
            {
                BannerScript.UnlockTrophy("Nucleus");
            }
            timer += Time.deltaTime;
            if(makeInput == true)
            {
                if(showGo == false)
                theTimer -= Time.deltaTime;
                int tmp = (int)theTimer;
                gameTimer.GetComponent<TMPro.TextMeshPro>().text = "TIMER: " + tmp.ToString();
                if (tmp < 1)
                {
                    SignalIncorrect();
                    // ResetStuff();
                    waitAsec = true;
                    timer = 0;
                    showStuff = false;
                    makeInput = false;
                }
            }
            WaitForTime();
            theGO();
            if (waitAsec == false)
            {
                if (showStuff == false && makeInput == false && timer > 3)
                {
                    ResetStuff();
                    GeneratePattern();
                    showStuff = true;
                }
                if (makeInput)
                {
                    sign.GetComponent<MeshRenderer>().enabled = false;
                    sign.GetComponentInChildren<TMPro.TextMeshPro>().text = " ";
                    TakeInput();
                }
                if (showStuff)
                {
                    ShowPattern();
                    timer = 0;
                    waitAsec = true;
                }
            }

            if (shownSign == theList.Count && showStuff)
            {
                makeInput = true;
                showStuff = false;
                showGo = true;
                goTimer = 0;
            }
            else
                return;
        }

    }
    void theGO()
    {
        goTimer += Time.deltaTime;
        if(goTimer >= 2 && showGo == true)
        {
            GO.SetActive(true);
            goTimer = 0;
            showGo = false;
        }
        else if(goTimer >= 1 && showGo == false)
        {
            GO.SetActive(false);
        }
    }
   
    void WaitForTime()
    {
        if(waitAsec)
        {
            if (timer > 1.0)
            {
                sign.GetComponent<MeshRenderer>().enabled = false;
                sign.GetComponentInChildren<TMPro.TextMeshPro>().text = " ";
                TurnOffLights();
            }
            if (timer > 2.0)
                waitAsec = false;
        }
    }
    void GeneratePattern()
    {
        theTimer = 30;
        gameTimer.GetComponent<TMPro.TextMeshPro>().text = "TIMER: " + theTimer.ToString();
        for (int i = 0; i < round; i++)
        {
            theColors tmpColor = new theColors();
            tmpColor = (theColors)Random.Range(0, 4);
            theList.Add(tmpColor);
        }
    }
    void ShowPattern()
    {
       
        switch (theList[shownSign])
        {
            case theColors.YELLOW:
                sign.GetComponent<Renderer>().material.color = Color.yellow;
                sign.GetComponentInChildren<TMPro.TextMeshPro>().text = "C";
                break;
            case theColors.RED:
                sign.GetComponent<Renderer>().material.color = Color.red;
                sign.GetComponentInChildren<TMPro.TextMeshPro>().text = "A";
                break;
            case theColors.BLUE:
                sign.GetComponent<Renderer>().material.color = Color.blue;
                sign.GetComponentInChildren<TMPro.TextMeshPro>().text = "U";
                break;
            case theColors.GREEN:
                sign.GetComponent<Renderer>().material.color = Color.green;
                sign.GetComponentInChildren<TMPro.TextMeshPro>().text = "G";
                break;
            default:
                break;
        }

        SoundManager.PlaySFX(sounds[0].name);
        sign.GetComponent<MeshRenderer>().enabled = true;
        shownSign++;
    }

    void TakeInput()
    {
        if (buttonPressed)
        {
            SoundManager.PlaySFX(sounds[0].name);
            GO.SetActive(false);
            switch (selectedColor)
            {
                case theColors.YELLOW:
                    yellow.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case theColors.RED:
                    red.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case theColors.BLUE:
                    blue.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case theColors.GREEN:
                    green.GetComponent<Renderer>().material.color = Color.green;
                    break;
                default:
                    break;
            }

            inputedColors.Add(selectedColor);
            buttonPressed = false;
            inputed++;
            makeNuclei = true;
            CheckInput();
        }          
    }

    void CheckInput()
    {
        bool correct = true;
        for (int i = 0; i < inputed; i++)
        {
            if (inputedColors[i] != theList[i])
            {
                correct = false;
                break;
            }
        }
        if (correct)
        {
            if (inputed == round)
            {
                TurnOnLights();
                round++;
                waitAsec = true;
                timer = 0;
                //ResetStuff();
                showStuff = false;
                makeInput = false;
            }

        }
        else
        {
            SignalIncorrect();
            // ResetStuff();
            waitAsec = true;
            timer = 0;
            showStuff = false;
            makeInput = false;
        }
    }
    void ShowScore()
    {
        if(score > finalScore)
        {
            finalScore = score;
        }
        if (finalScore > PlayerPrefs.GetFloat("SimonScore"))
            PlayerPrefs.SetFloat("SimonScore", finalScore);
        else
            finalScore = PlayerPrefs.GetFloat("SimonScore");
        UI.SetActive(false);
       
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .5f);
        //scoreBoard.GetComponent<ScoreBoardScript>().GenerateScore();
        transform.position = new Vector3(transform.position.x, transform.position.y, 10000);
        Username.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + score.ToString(); //FacebookManager.Instance.GlobalScore;
        if (FacebookManager.Instance.ProfilePic != null)
            ProfilePic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
        round = 1;
    }
    void ResetStuff()
    {
        theList.Clear();
        inputedColors.Clear();
        makeInput = false;
        inputed = 0;
        shownSign = 0;
        TurnOffLights();
    }
   public void ResetGame()
    {
        SceneManager.LoadScene("SimonDNA");
    }
    void SignalIncorrect()
    {
        SoundManager.PlaySFX(sounds[2].name);
        yellow.GetComponent<Renderer>().material.color = Color.black;
        red.GetComponent<Renderer>().material.color = Color.black;
        blue.GetComponent<Renderer>().material.color = Color.black;
        green.GetComponent<Renderer>().material.color = Color.black;
        lives--;
        scoreCombo = 1;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "Lives: " + lives.ToString();
        if (lives < 1)
        {
            if (GlobalVariables.arcadeMode == true)
            {
                ShowScore();
            }
            else
            {
                SceneManager.LoadScene("SimonDNA");
            }
            
        }
    }
    void TurnOnLights()
    {
        SoundManager.PlaySFX(sounds[1].name);
        yellow.GetComponent<Renderer>().material.color = Color.yellow;
        red.GetComponent<Renderer>().material.color = Color.red;
        blue.GetComponent<Renderer>().material.color = Color.blue;
        green.GetComponent<Renderer>().material.color = Color.green;
        score += round * scoreCombo;
        if (GlobalVariables.arcadeMode == true)
        {
            score += round * scoreCombo;
            scoreCombo++;
            theScore.GetComponent<TMPro.TextMeshPro>().text = "Score: " + score.ToString();
        }
        else
        {
            polysDone = polysDone + round;
            polyLeft.GetComponent<TMPro.TextMeshPro>().text = "Nucleids Done: " + polysDone.ToString();
            if (polysDone == polys)
            {
               
            }
        }
    }
  public void TurnOffLights()
    {
        yellow.GetComponent<Renderer>().material.color = cY;
        red.GetComponent<Renderer>().material.color = cR;
        blue.GetComponent<Renderer>().material.color = cB;
        green.GetComponent<Renderer>().material.color = cG;
       
    }
   public void selectYellow()
    {   if(makeInput)
        {
            selectedColor = theColors.YELLOW;
            buttonPressed = true;
            TurnOffLights();
        }
    }

    public void selectRed()
    {
        if (makeInput)
        {
            selectedColor = theColors.RED;
            buttonPressed = true;
            TurnOffLights();
        }
    }

    public void selectBlue()
    {
        if (makeInput)
        {
            selectedColor = theColors.BLUE;
            buttonPressed = true;
            TurnOffLights();
        }
    }

    public void selectGreen()
    {
        if (makeInput)
        {
            selectedColor = theColors.GREEN;
            buttonPressed = true;
            TurnOffLights();
        }
    }
}
