using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public GameObject EventSystem;
    public TextMeshPro CenterScreenObj;
    public GameObject LabLogo;
    public GameObject UniversityLogo;



    public int MoveText = 0;

    private bool text = false, finish = false;
    const int size = 32;
    private string[] TextList = new string[size];


    private void Start()
    {
        LabLogo.SetActive(false);
        UniversityLogo.SetActive(false);
        int x = 0;
        TextList[x++] = "Vir-ed";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Virus Story Mode:"       + "\n" + "Josue Cortes";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Cell Story Mode:"        + "\n" + "Josue Cortes";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "SimonDNA:"               + "\n" + "Josue Cortes"            + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Dodge Antibodies:"       + "\n" + "Josue Cortes"            + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Trophy Room:"            + "\n" + "Josue Cortes"            + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "cGamp Snatcher:"         + "\n" + "Sebastian Diaz Portillo" + "\n" + "Michael Toronto";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Memory Game :"           + "\n" + "Sebastian Diaz Portillo" + "\n" + "Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Main Menu:"              + "\n" + "Sebastian Diaz Portillo" + "\n" + "Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "ATP/GTP Shooter:"        + "\n" + "Michael Toronto";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Fight Virus:"            + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Destroy Cell:"           + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Strategy Game:"          + "\n" + "Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design"           + "\n\n" + "Main Menu:"              + "\n" + "Sebastian Diaz Portillo" + "\n" + "Jordan Sanderson";
        TextList[x++] = "Writing / Dialogue / Story"        + "\n\n" + "Josue Cortes"            + "\n" + "Robert Gregg"            + "\n" + "Jason Shoemaker";
        TextList[x++] = "Research"                          + "\n\n" + "Jason Shoemaker"         + "\n" + "Robert Gregg";
        TextList[x++] = "Programming"                       + "\n\n" + "Sebastian Diaz Portillo" + "\n" + "Josue Cortes"            + "\n" + "Jordan Sanderson" + "\n" + "Jayson Levario" + "\n" + "Michael Toronto";
        TextList[x++] = "3D / Shaders Programming"          + "\n\n" + "Jordan Sanderson";
        TextList[x++] = "Music / Sound Programming"         + "\n\n" + "Sebastian Diaz Portillo";
        TextList[x++] = "Facebook API Programming"          + "\n\n" + "Sebastian Diaz Portillo";
        TextList[x++] = "Google Cardboard API Programming"  + "\n\n" + "Jordan Sanderson"        + "\n" + "Sebastian Diaz Portillo";
        TextList[x++] = "Graphics / Artwork"                + "\n\n" + "Alexandre Thorpe";
        TextList[x++] = "Animation"                         + "\n\n" + "Sebastian Diaz Portillo" + "\n" + "Josue Cortes"            + "\n" + "Jordan Sanderson" + "\n" + "Jayson Levario" + "\n" + "Michael Toronto";
        TextList[x++] = "3D Modeling"                       + "\n\n" + "Alexandre Thorpe";
        TextList[x++] = "Music"                             + "\n\n" + "Tommy Ascough"           + "\n" + "Dwight Gifford";
        TextList[x++] = "Sound"                             + "\n\n" + "Eric Nethery"            + "\n" + "Armin Munoz";
        TextList[x++] = "Acting / Voiceovers"               + "\n\n" + "Josue Cortes";
        TextList[x++] = "Producers"                         + "\n\n" + "Jason Hinders"           + "\n" + "Haifa Maamar"            + "\n" + "Jason Shoemaker"  + "\n" + "Robert Gregg";
        TextList[x++] = "No Viruses or Cells got harmed during the making of this game...";
        TextList[x++] = "     ";
        TextList[x++] = "     ";

    }
    bool isInit = false;

    void Update()
    {
        if (!text)
            OptimizedStuff();
    }


    void OptimizedStuff()
    {
        if (isInit)
            return;
        isInit = true;
        for (int i = 0; i < TextList.Length; ++i)
        {
            StartCoroutine(TurnTextOn(i,2));
        }
    }


    IEnumerator TurnTextOn(int index, int delay)
    {
        while (text)
            yield return 0;

        text = true;
        CenterScreenObj.text = " ";

            if (index == 29)
            {
                LabLogo.SetActive(true);
                UniversityLogo.SetActive(false);
            }

        if (index == 30)
        {
            LabLogo.SetActive(false);
            UniversityLogo.SetActive(true);
        }

        if( index == 31)
         {
             SceneManager.LoadScene("MainMenu");
         }

        while (CenterScreenObj.text != TextList[index] && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (CenterScreenObj.text.Length == TextList[index].Length)
            {
                CenterScreenObj.text = TextList[index];
            }
            else
            {
                CenterScreenObj.text = CenterScreenObj.text.Insert(CenterScreenObj.text.Length - 1, TextList[index][CenterScreenObj.text.Length - 1].ToString());
            }
        }



        CenterScreenObj.text = TextList[index];
        yield return new WaitForSeconds(delay);
        finish = false;
        text = false;
        MoveText += 1;
    }
}
