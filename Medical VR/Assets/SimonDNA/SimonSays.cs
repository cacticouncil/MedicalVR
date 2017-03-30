using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SimonSays : MonoBehaviour {

    // Use this for initialization
    public GameObject yellow;
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject sign, UI, scoreBoard, theScore, theLives, polyLeft, GO, gameTimer;

    public int score = 0, polys = 0, polysDone;
    Color cY, cR, cB, cG;

    int round = 1, inputed = 0, shownSign = 0, scoreCombo = 1;

    float timer = 3, theTimer;

    Vector3 orgPos;
    public static bool arcadeMode = false;
    bool makeInput = false, buttonPressed = false, showStuff = false, waitAsec = false;
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
        orgPos = transform.position;
        cY = yellow.GetComponent<Renderer>().material.color;
        cR = red.GetComponent<Renderer>().material.color;
        cB = blue.GetComponent<Renderer>().material.color;
        cG = green.GetComponent<Renderer>().material.color;
        selectedColor = (theColors)5;
        theScore.GetComponent<TMPro.TextMeshPro>().text = "Score: " + score.ToString();
        theLives.GetComponent<TMPro.TextMeshPro>().text = "Lives: " + lives.ToString();
       gameTimer.GetComponent<TMPro.TextMeshPro>().text = "TIMER " + theTimer.ToString();
        theTimer = 30;
        polys = 36;
        polysDone = 0;
        if(arcadeMode == false)
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
            timer += Time.deltaTime;
            if(makeInput == true)
            {
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

            if (waitAsec == false)
            {
                if (showStuff == false && makeInput == false)
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
                GO.SetActive(true);
            }
            else
                return;
        }

    }
    void ArcadeMode()
    {
        
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
        sign.GetComponent<MeshRenderer>().enabled = true;
        shownSign++;
    }
    void TakeInput()
    {
        if (buttonPressed)
        {
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
        UI.SetActive(false);
       
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
        scoreBoard.GetComponent<ScoreBoardScript>().GenerateScore();
        transform.position = new Vector3(transform.position.x, transform.position.y, 10000);
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
        ResetStuff();
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        transform.position = orgPos;
        lives = 3;
        score = 0;
    }
    void SignalIncorrect()
    {
        yellow.GetComponent<Renderer>().material.color = Color.black;
        red.GetComponent<Renderer>().material.color = Color.black;
        blue.GetComponent<Renderer>().material.color = Color.black;
        green.GetComponent<Renderer>().material.color = Color.black;
        lives--;
        scoreCombo = 1;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "Lives: " + lives.ToString();
        if (lives < 1)
        {
            if (arcadeMode == true)
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
        yellow.GetComponent<Renderer>().material.color = Color.yellow;
        red.GetComponent<Renderer>().material.color = Color.red;
        blue.GetComponent<Renderer>().material.color = Color.blue;
        green.GetComponent<Renderer>().material.color = Color.green;
        score += round * scoreCombo;
        if (arcadeMode == true)
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
    void TurnOffLights()
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
