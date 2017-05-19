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
    public TextMeshPro CenterScreenObj;


    public int MoveText = 0;

    private bool last = false, text = false, finish = false;
    const int size = 37;
    private string[] TextList = new string[size];


    private void Start()
    {
        int x = 0;
        TextList[x++] = "Vir-ed";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Virus Story Mode:" + "\n" + "     Josue Cortes";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Cell Story Mode:" + "\n" + "     Josue Cortes";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     SimonDNA:" + "\n" + "     Josue Cortes" + "\n" + "     Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Dodge Antibodies:" + "\n" + "     Josue Cortes" + "\n" + "     Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Trophy Room:" + "\n" + "     Josue Cortes" + "\n" + "     Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     cGamp Snatcher:" + "\n" + "     Sebastian Diaz Portillo" + "\n" + "     Michael Toronto";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Memory Game :" + "\n" + "     Sebastian Diaz Portillo" + "\n" + "     Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Main Menu:" + "\n" + "     Sebastian Diaz Portillo" + "\n" + "     Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     ATP/GTP Shooter:" + "\n" + "     Michael Toronto";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Fight Virus:" + "\n" + "     Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Destroy Cell:" + "\n" + "     Jayson Levario";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Strategy Game:" + "\n" + "     Jordan Sanderson";
        TextList[x++] = "Level / Scenario Design:" + "\n" + "     Main Menu:" + "\n" + "     Sebastian Diaz Portillo" + "\n" + "     Jordan Sanderson";
        TextList[x++] = "Writing / Dialogue / Story:" + "\n" + "     Josue Cortes" + "\n" + "     Robert Gregg" + "\n" + "     Jason Shoemaker";
        TextList[x++] = "Research:" + "\n" + "     Jason Shoemaker" + "\n" + "     Robert Gregg";
        TextList[x++] = "Programming:" + "\n" + "     Sebastian Diaz Portillo" + "\n" + "     Josue Cortes" + "\n" + "     Jordan Sanderson" + "\n" + "     Jayson Levario" + "\n" + "     Michael Toronto";
        TextList[x++] = "3D / Shaders Programming:" + "\n" + "     Jordan Sanderson";
        TextList[x++] = "Music / Sound Programming:" + "\n" + "     Sebastian Diaz Portillo";
        TextList[x++] = "Facebook API Programming" + "\n" + "     Sebastian Diaz Portillo";
        TextList[x++] = "Google Cardboard API Programming:" + "\n" + "     Jordan Sanderson" + "\n" + "     Sebastian Diaz Portillo";
        TextList[x++] = "Lead Artist:" + "\n" + "";
        TextList[x++] = "Graphics / Artwork:" + "\n" + "     Alexandre Thorpe";
        TextList[x++] = "Additional Graphics / Artwork:" + "\n" + "";
        TextList[x++] = "Animation" + "\n" + "";
        TextList[x++] = "3D Modeling" + "\n" + "     Alexandre Thorpe";
        TextList[x++] = "Movie Crew:" + "\n" + "";
        TextList[x++] = "Music:" + "\n" + "";
        TextList[x++] = "Sound:" + "\n" + "";
        TextList[x++] = "Acting / Voiceovers:" + "\n" + "     Josue Cortes";
        TextList[x++] = "Project Leader:" + "\n" + "";
        TextList[x++] = "Producers:" + "\n" + "     Jason Hinders" + "\n" + "     Haifa Maamar" + "\n" + "     Jason Shoemaker" + "\n" + "     Robert Gregg";
        TextList[x++] = "No Viruses or Cells got harmed during the making of this game..." + "\n" + "";
        TextList[x++] = "Immuno Systems Lab" + "\n" + "";
        TextList[x++] = "University Of Pittsburgh" + "\n" + "";

    }
    bool isInit = false;

    void Update()
    {
        //BannerScript.UnlockTrophy("")
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
            StartCoroutine(TurnTextOn(i,/* 4 * i + 4*/6));
        }
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
