using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SimonDNATutorial : MonoBehaviour
{
    public GameObject Subtitles, RNA, press;
    public GameObject Player;
    public GameObject UI;

    public float LightTimer = 0.0f;

    int AnimationCases = 1;
    float AnimationTimer = 0.0f;
    bool CheckAnimation = true;
    public bool prevState;

    //For text
    public int MoveText = 0;
    public bool CanIRead = true;
    public TextMeshPro Text;
    private string[] TextList = new string[10];
    private bool last = false, text = false, finish = false;

    void Start()
    {
       //GlobalVariables.tutorial = true;
        if (GlobalVariables.tutorial == true)
        {
            prevState = GlobalVariables.arcadeMode;
            GlobalVariables.arcadeMode = false;
            Subtitles.SetActive(true);
            Player.GetComponent<SimonSays>().GO.SetActive(false);
            Player.GetComponent<SimonSays>().lives = 0;
            UI.SetActive(false);

            TextList[0] = "Listen closely human, here is your objective. ";
            TextList[1] = "Nucleotides will pop up on the screen in a certain order, you will always have four nucleotides around you which represent all the types of nucleotides that could pop up.";
            TextList[2] = "Once the GO! sign appears, use the four nucleotides to press them in the same order that the previous nucleotides showed up.";
            TextList[3] = "Get it correct to build up the messenger RNA, getting it incorrect will cancel out the part you are building.";
            TextList[4] = "Build a RNA Messenger witht the amount of nucleotides displayed on the goal to complete the messeger, Good Luck!.";
            TextForTutorial();
        }
        else
            enabled = false;
    }

    void Update()
    {
        bool held = Input.GetButton("Fire1");
        if (held && !last)
        {
            if (text)
                finish = true;

            else
            {
                press.SetActive(false);
                TextForTutorial();
            }
        }

        last = held;

        if (MoveText == 2)
        {
            Player.GetComponent<SimonSays>().TurnOffLights();
            LightTimer += Time.deltaTime;

            if (LightTimer < 2.0f)
                Player.GetComponent<SimonSays>().green.GetComponent<Renderer>().material.color = Color.green;

            if (LightTimer >= 8.0f)
            {
                press.SetActive(true);
                LightTimer = 0.0f;
                MoveText += 1;
            }

            else if (LightTimer >= 6.0f)
                Player.GetComponent<SimonSays>().blue.GetComponent<Renderer>().material.color = Color.blue;

            else if (LightTimer >= 4.0f)
                Player.GetComponent<SimonSays>().yellow.GetComponent<Renderer>().material.color = Color.yellow;

            else if (LightTimer >= 2.0f)
                Player.GetComponent<SimonSays>().red.GetComponent<Renderer>().material.color = Color.red;
        }


        if (MoveText == 5)
        {
            AnimationTimer += Time.deltaTime;

            if (CheckAnimation == true)
            {
                switch (AnimationCases)
                {
                    case 1:
                        Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.RED;
                        RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                        AnimationCases += 1;
                        CheckAnimation = false;
                        break;

                    case 2:
                        Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.BLUE;
                        RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                        AnimationCases += 1;
                        CheckAnimation = false;
                        break;

                    case 3:
                        Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.YELLOW;
                        RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                        AnimationCases += 1;
                        CheckAnimation = false;
                        break;

                    case 4:
                        Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.GREEN;
                        RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                        AnimationCases += 1;
                        CheckAnimation = false;
                        MoveText += 1;
                        press.SetActive(true);
                        break;
                }
            }

            if (AnimationTimer >= 2.0f)
            {
                AnimationTimer = 0.0f;
                CheckAnimation = true;
            }
        }
    }

    void TextForTutorial()
    {
        switch (MoveText)
        {
            case 0:
                SoundManager.PlaySFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-001");
                StartCoroutine(TurnTextOn(0));
                break;

            case 1:
                SoundManager.stopSFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-001");
                SoundManager.PlaySFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-002");
                StartCoroutine(TurnTextOn(1));
                break;

            case 2:
                break;

            case 3:
                SoundManager.stopSFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-002");
                SoundManager.PlaySFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-003");
                Player.GetComponent<SimonSays>().GO.SetActive(true);
                StartCoroutine(TurnTextOn(2));
                break;

            case 4:
                SoundManager.stopSFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-003");
                SoundManager.PlaySFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-004");
                StartCoroutine(TurnTextOn(3));
                break;

            case 5:
                break;

            case 6:
                SoundManager.stopSFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-004");
                SoundManager.PlaySFX("SimonDNA/Medical_VR_Tutorial_VO_Simon_DNA-005");
                StartCoroutine(TurnTextOn(4));
                break;

            case 7:
                VirusGameplayScript.loadCase = 2;
                GlobalVariables.tutorial = false;
                GlobalVariables.arcadeMode = prevState;
                SceneManager.LoadScene("SimonDNA");
                break;

            default:
                break;
        }
    }

    IEnumerator TurnTextOn(int index)
    {
        while (text)
            yield return 0;

        text = true;
        Text.text = " ";

        while (Text.text != TextList[index] && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (Text.text.Length == TextList[index].Length)
                Text.text = TextList[index];

            else
                Text.text = Text.text.Insert(Text.text.Length - 1, TextList[index][Text.text.Length - 1].ToString());
        }

        Text.text = TextList[index];
        finish = false;
        text = false;
        press.SetActive(true);
        MoveText += 1;
    }
}
