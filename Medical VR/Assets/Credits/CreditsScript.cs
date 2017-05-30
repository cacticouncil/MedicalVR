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
    public GameObject CactiCouncilLogo;




    public int MoveText = 0;

    private bool text = false, finish = false;
    const int size = 24;
    private string[] TextList = new string[size];


    private void Start()
    {
        LabLogo.SetActive(false);
        UniversityLogo.SetActive(false);
        CactiCouncilLogo.SetActive(false);
        int x = 0;
        TextList[x++] = "Vir-ed";
        TextList[x++] = "Josue Cortes"            + "\n\n" + "Writing / Dialogue / Story" + "\n" + "Virus Story Mode" + "\n" + "Cell Story Mode" + "\n" + "SimonDNA" + "\n" + "Dodge Antibodies" + "\n" + "Trophy Room" + "\n" + "Programming" + "\n" + "Animation" + "\n" + "Acting / Voiceovers";
        TextList[x++] = "Jayson Levario"          + "\n\n" + "Fight Virus" + "\n" + "Destroy Cell" + "\n" + "Trophy Room" + "\n" + "SimonDNA" + "\n" + "Dodge Antibodies" + "\n" + "Programming" + "\n" + "Animation"   ;
        TextList[x++] = "Sebastian Diaz Portillo" + "\n\n" + "cGAMP Snatcher" + "\n" + "Main Menu" + "\n" + " DNA Memory Game" + "\n" + "Music / Sound Programming" + "\n" + "Facebook API Programming" + "\n" + "Programming" + "\n" + "GVR Programming" + "\n" + "Animation";
        TextList[x++] = "Michael Toronto"         + "\n\n" + "ATP/GTP Shooter" + "\n" + "cGAMP Snatcher" + "\n" + "Programming" + "\n" + "Animation" + "\n" + "Facebook API Programming";
        TextList[x++] = "Jordan Sanderson"        + "\n\n" + "Cell Colony" + "\n" + "3D / Shaders Programming" + "\n" + "GVR API Programming" + "\n" + "Main Menu" + "\n" + " DNA Memory Game" + "\n" + "Programming"  + "\n" + "Animation";
        TextList[x++] =  "Robert Gregg"           + "\n\n" + "Writing / Dialogue / Story" + "\n" + "Research" + "\n" + "Producer";
        TextList[x++] = "Jason Shoemaker"         + "\n\n" + "Writing / Dialogue / Story" + "\n" + "Research" + "\n" + "Producer";
        TextList[x++] = "Alexandre Thorpe"        + "\n\n" + "Graphics / Artwork" + "\n" + "3D Modeling";
        TextList[x++] = "Tommy Ascough"           + "\n\n" + "Music Producer and Artist";
        TextList[x++] = "Dwight Gifford"          + "\n\n" + "Music Artist";
        TextList[x++] = "Eric Nethery"            + "\n\n" + "Sound";
        TextList[x++] = "Armid Munoz"             + "\n\n" + "Sound";
        TextList[x++] = "Haifa Maamar"            + "\n\n" + "Producer";
        TextList[x++] = "Jason Hinders"           + "\n\n" + "Producer";
        TextList[x++] = "Kris Docote"            + "\n\n" + "Producer";
        TextList[x++] = "Jeffrey Malesky" + "\n\n" + "UX Lab Testing";
        TextList[x++] = "Brian Vazquez" + "\n\n" + "UX Lab Testing";
        TextList[x++] = "Cody Felts" + "\n\n" + "Expo UX Testing";
        TextList[x++] = "No Viruses or Cells got harmed during the making of this game...";
        TextList[x++] = "     ";
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

            if (index == 20)
            {
                LabLogo.SetActive(true);
                UniversityLogo.SetActive(false);
            }

        if (index == 21)
        {
            LabLogo.SetActive(false);
            UniversityLogo.SetActive(true);
        }

        if (index == 22)
        {
            LabLogo.SetActive(false);
            CactiCouncilLogo.SetActive(true);
        }

        if ( index == 23)
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
