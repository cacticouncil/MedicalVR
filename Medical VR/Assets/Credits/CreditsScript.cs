using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsScript : MonoBehaviour
{
    float TutorialTimer = 0.0f;
    int WhatToRead = 0;
    float BeatGameTimer = 0.0f;
    public GameObject EventSystem;
    public TMPro.TextMeshPro CenterScreenObj;


    public int MoveText = 0;
    public bool CanIRead = true;

    private bool last = false, text = false, finish = false;
    const int size = 32;
    private string[] TextList = new string[size];


    private void Start()
    {
        int x = 0;
        TextList[x++] = "Vir-ed";
        TextList[x++] = "Director:" + "\n" + "";
        TextList[x++] = "Lead Designer:" + "\n" + "";
        TextList[x++] = "Designer:" + "\n" + "";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Virus Story Mode:" + "\n" + "Josue Cortes";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Cell Story Mode:" + "\n" + "Josue Cortes";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     SimonDNA:" + "\n" + "Josue Cortes" + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Dodge Antibodies:" + "\n" + "Josue Cortes" + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Trophy Room:" + "\n" + "Josue Cortes" + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     cGamp Snatcher:" + "\n" + "Sebastian Diaz Portillo" + "\n" + "Michael Toronto";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Memory Game :" + "\n" + "Sebastian Diaz Portillo" + "\n" + "Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Main Menu:" + "\n" + "Sebastian Diaz Portillo" + "\n" + "Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     ATP/GTP Shooter:" + "\n" + "Michael Toronto";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Fight Virus:" + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Destroy Cell:" + "\n" + "Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Strategy Game:" + "\n" + "Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Main Menu:" + "\n" + "Sebastian Diaz Portillo" + "\n" + "Jordan Sanderson";








        TextList[x++] = "Level / Scenario Design:" + "\n" + "Strategy Game: Josue Cortes" + "\n" + "Main Menu: Josue Cortes" + "\n" + "Virus Story Mode: Josue Cortes" + "\n" + "Cell Story Mode: Josue Cortes";
        TextList[x++] = "Writing / Dialogue / Story:" + "\n" + "";
        TextList[x++] = "Research:" + "\n" + "";
        TextList[x++] = "Technical Director:" + "\n" + "";
        TextList[x++] = "Lead Programming:" + "\n" + "";
        TextList[x++] = "Programming:" + "\n" + "";
        TextList[x++] = "Additional Programming:" + "\n" + "";
        TextList[x++] = "Game Engie / Development System:" + "\n" + "";
        TextList[x++] = "3D / Graphics Programming:" + "\n" + "";
        TextList[x++] = "Music / Sound Programming:" + "\n" + "";
        TextList[x++] = "Libraries / Utilities" + "\n" + "";
        TextList[x++] = "AI Programming:" + "\n" + "";
        TextList[x++] = "Art Director:" + "\n" + "";
        TextList[x++] = "Lead Artisr:" + "\n" + "";
        TextList[x++] = "Graphics / Artwork:" + "\n" + "";
        TextList[x++] = "Additional Graphics / Artwork:" + "\n" + "";
        TextList[x++] = "Animation" + "\n" + "";
        TextList[x++] = "3D Modeling" + "\n" + "";
        TextList[x++] = "Movie Crew:" + "\n" + "";
        TextList[x++] = "Music:" + "\n" + "";
        TextList[x++] = "Sound:" + "\n" + "";
        TextList[x++] = "Acting / Voiceovers:" + "\n" + "";
        TextList[x++] = "Project Leader:" + "\n" + "";
        TextList[x++] = "Producer:" + "\n" + "Jason Hinders";
        TextList[x++] = "Executive Producer:" + "\n" + "";
        TextList[x++] = "Associate Producer:" + "\n" + "";
        TextList[x++] = "Assistant Producer:" + "\n" + "";
        TextList[x++] = "No Viruses or Cells got harmed during the making of this game..." + "\n" + "";
    }
    bool isInit = false;

    void Update()
    {
        if (!text)
            OptimizedStuff();
        //   Texticles();
    }


    void OptimizedStuff()
    {
        if (isInit)
            return;
        isInit = true;
        for (int i = 0; i < TextList.Length; ++i)
            StartCoroutine(TurnTextOn(i, 4 * i + 4));
    }

    private void Texticles()
    {
        switch (WhatToRead)
        {
            case 0:
                StartCoroutine(TurnTextOn(0, 4));

                break;

            case 1:
                StartCoroutine(TurnTextOn(1, 4));

                break;
            case 2:
                StartCoroutine(TurnTextOn(2, 4));

                break;

            case 3:
                StartCoroutine(TurnTextOn(3, 4));

                break;

            case 4:
                StartCoroutine(TurnTextOn(4, 4));

                break;
            case 5:
                StartCoroutine(TurnTextOn(5, 4));

                break;

            case 6:
                StartCoroutine(TurnTextOn(6, 4));

                break;

            case 7:
                StartCoroutine(TurnTextOn(7, 4));

                break;

            case 8:
                StartCoroutine(TurnTextOn(8, 4));

                break;
            case 9:
                StartCoroutine(TurnTextOn(9, 4));

                break;

            case 10:
                StartCoroutine(TurnTextOn(10, 4));

                break;

            case 11:
                StartCoroutine(TurnTextOn(11, 4));

                break;

            case 12:
                StartCoroutine(TurnTextOn(12, 4));

                break;

            case 13:
                StartCoroutine(TurnTextOn(13, 4));

                break;

            case 14:
                StartCoroutine(TurnTextOn(14, 4));

                break;

            case 15:
                StartCoroutine(TurnTextOn(15, 4));

                break;

            case 16:
                StartCoroutine(TurnTextOn(16, 4));

                break;

            case 17:
                StartCoroutine(TurnTextOn(17, 4));

                break;

            case 18:
                StartCoroutine(TurnTextOn(18, 4));

                break;

            case 19:
                StartCoroutine(TurnTextOn(19, 4));

                break;

            case 20:
                StartCoroutine(TurnTextOn(20, 4));

                break;

            case 21:
                StartCoroutine(TurnTextOn(21, 4));

                break;

            case 22:
                StartCoroutine(TurnTextOn(22, 4));

                break;

            case 23:
                StartCoroutine(TurnTextOn(23, 4));

                break;

            case 24:
                StartCoroutine(TurnTextOn(24, 4));

                break;

            case 25:
                StartCoroutine(TurnTextOn(25, 4));

                break;

            case 26:
                StartCoroutine(TurnTextOn(26, 4));

                break;

            case 27:
                StartCoroutine(TurnTextOn(27, 4));

                break;

            case 28:
                StartCoroutine(TurnTextOn(28, 4));

                break;

            case 29:
                StartCoroutine(TurnTextOn(29, 4));

                break;

            case 30:
                StartCoroutine(TurnTextOn(30, 4));

                break;

            case 31:
                StartCoroutine(TurnTextOn(31, 4));

                break;

            default:
                CenterScreenObj.text = "";
                break;
        }
        WhatToRead += 1;
    }


    IEnumerator TurnTextOn(int index, int delay)
    {
        while (text)
            yield return 0;

        text = true;
        CenterScreenObj.text = " ";

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
