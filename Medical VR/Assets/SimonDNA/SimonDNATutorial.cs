using UnityEngine;
using System.Collections;

public class SimonDNATutorial : MonoBehaviour
{
    public GameObject Subtitles, RNA;
    public GameObject Player;
    float LightTimer = 0.0f;
    void Start()
    {
        Player.GetComponent<SimonSays>().GO.SetActive(false);
    }

    void Update()
    {
        if (SimonSays.TutorialMode == true)
        {
            switch ((int)Subtitles.GetComponent<SubstitlesScript>().theTimer)
            {
                //Show them all 4 flashing nucleotides
                case 12:
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
                case 16:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    Player.GetComponent<SimonSays>().GO.SetActive(true);
                    Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                    Subtitles.GetComponent<SubstitlesScript>().Continue();
                    break;

                case 20:
                    Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.RED;
                    RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                    Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.BLUE;
                    RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                    Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.YELLOW;
                    RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                    Player.GetComponent<SimonSays>().selectedColor = SimonSays.theColors.GREEN;
                    RNA.GetComponent<RnaMessengerScript>().AddNuclei();
                    Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                    Subtitles.GetComponent<SubstitlesScript>().Continue();
                    break;
                default:
                    break;
            }
        }
    }
}
