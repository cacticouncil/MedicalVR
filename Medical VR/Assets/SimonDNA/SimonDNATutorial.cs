using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SimonDNATutorial : MonoBehaviour
{
    public GameObject Subtitles, RNA;
    public GameObject Player;
    public GameObject UI;

    float LightTimer = 0.0f;

    int AnimationCases = 1;
    float AnimationTimer = 0.0f;
    bool CheckAnimation = true;
    void Start()
    {
        if (SimonSays.TutorialMode == true)
        {
            Subtitles.SetActive(true);
            Player.GetComponent<SimonSays>().GO.SetActive(false);
            UI.SetActive(false);
        }
    }

    void Update()
    {
        if (SimonSays.TutorialMode == true)
        {
            switch ((int)Subtitles.GetComponent<SubstitlesScript>().theTimer)
            {
                //Show them all 4 flashing nucleotides
                case 14:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    Player.GetComponent<SimonSays>().TurnOffLights();

                    LightTimer += Time.deltaTime;

                    if (LightTimer < 2.0f)
                        Player.GetComponent<SimonSays>().green.GetComponent<Renderer>().material.color = Color.green;

                    if (LightTimer >= 8.0f)
                    {
                        Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                        Subtitles.GetComponent<SubstitlesScript>().Continue();
                    }

                    else if (LightTimer >= 6.0f)
                        Player.GetComponent<SimonSays>().blue.GetComponent<Renderer>().material.color = Color.blue;

                    else if (LightTimer >= 4.0f)
                        Player.GetComponent<SimonSays>().yellow.GetComponent<Renderer>().material.color = Color.yellow;

                    else if (LightTimer >= 2.0f)
                        Player.GetComponent<SimonSays>().red.GetComponent<Renderer>().material.color = Color.red;

                    break;

                //Display the GO 
                case 20:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    Player.GetComponent<SimonSays>().GO.SetActive(true);
                    Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                    Subtitles.GetComponent<SubstitlesScript>().Continue();
                    break;

                //Animation
                case 24:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
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
                                Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                                Subtitles.GetComponent<SubstitlesScript>().Continue();
                                break;
                        }
                    }

                    if (AnimationTimer >= 2.0f)
                    {
                        AnimationTimer = 0.0f;
                        CheckAnimation = true;
                    }

                    break;


                case 30:
                    //Send back to main menu
                    //if (true)
                    //{

                    //}

                    //Otherwise continue to story mode
                    VirusGameplayScript.loadCase = 2;
                    SimonSays.TutorialMode = false;
                    SimonSays.arcadeMode = false;
                    SceneManager.LoadScene("SimonDNA");

                    break;

                default:
                    break;
            }
        }
    }
}
