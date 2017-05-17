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
    private string[] TextList = new string[32];


    private void Start()
    {
        TextList[0] = "Medical VR";
        TextList[1] = "Director:" + "\n" + "";
        TextList[2] = "Lead Designer:" + "\n" + "";
        TextList[3] = "Designer:" + "\n" + "";
        TextList[4] = "Level / Scenario Design:" + "\n" + "";
        TextList[5] = "Writing / Dialogue / Story:" + "\n" + "";
        TextList[6] = "Research:" + "\n" + "";
        TextList[7] = "Technical Director:" + "\n" + "";
        TextList[8] = "Lead Programming:" + "\n" + "";
        TextList[9] = "Programming:" + "\n" + "";
        TextList[10] = "Additional Programming:" + "\n" + "";
        TextList[11] = "Game Engie / Development System:" + "\n" + "";
        TextList[12] = "3D / Graphics Programming:" + "\n" + "";
        TextList[13] = "Music / Sound Programming:" + "\n" + "";
        TextList[14] = "Libraries / Utilities" + "\n" + "";
        TextList[15] = "AI Programming:" + "\n" + "";
        TextList[16] = "Art Director:" + "\n" + "";
        TextList[17] = "Lead Artisr:" + "\n" + "";
        TextList[18] = "Graphics / Artwork:" + "\n" + "";
        TextList[19] = "Additional Graphics / Artwork:" + "\n" + "";
        TextList[20] = "Animation" + "\n" + "";
        TextList[21] = "3D Modeling" + "\n" + "";
        TextList[22] = "Movie Crew:" + "\n" + "";
        TextList[23] = "Music:" + "\n" + "";
        TextList[24] = "Sound:" + "\n" + "";
        TextList[25] = "Acting / Voiceovers:" + "\n" + "";
        TextList[26] = "Project Leader:" + "\n" + "";
        TextList[27] = "Producer:" + "\n" + "";
        TextList[28] = "Executive Producer:" + "\n" + "";
        TextList[29] = "Associate Producer:" + "\n" + "";
        TextList[30] = "Assistant Producer:" + "\n" + "";
        TextList[31] = "Product Manager:" + "\n" + "";
    }

    void Update()
    {
        if (!text)
                Texticles();
    }

    private void Texticles()
        {
        switch (WhatToRead)
        {
            case 0:
                StartCoroutine(TurnTextOn(0,4));

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
