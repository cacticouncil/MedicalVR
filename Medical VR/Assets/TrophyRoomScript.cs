using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrophyRoomScript : MonoBehaviour
{
    public TextMeshPro Text;
    public MeshCollider TrophyPedestal;
    public MeshCollider Station1Pedestal;
    public MeshCollider Station2Pedestal;
    public GameObject GVRSystem;
    public int MoveText = 0;
    public bool CanIRead = true;

    private bool last = false, text = false, finish = false;
    private string[] TextList = new string [10];
    void Start()
    {
        if (GlobalVariables.tutorial == false)
        {
            this.enabled = false;
            return;
        }
        
        TrophyPedestal.enabled = false;
        Station1Pedestal.enabled = false;
        Station2Pedestal.enabled = false;
        TextList[0] = "Welcome to the Trophy Room";
        TextList[1] = "This is where all of your achievements are shown.";
        TextList[2] = "If you click on the pedestal in front of the big board.";
        TextList[3] = "You can see all the trophies with descriptions that you can unlock.";
        TextList[4] = "You must play mini games in order to do so.";
        TextList[5] = "You can click on the other pedestals to move around in the room.";
        TextList[6] = "You can see the trophies on the shelves are all parts of the cell.";
        TextList[7] = "Click on any trophy to bring it to the pedestal.";
        TextList[8] = "You can see there is a description button that gives you more information about it.";
        TextList[9] = "Click back on the trophy to put it back.";
        Texticles();
    }

    void Update()
    {
        bool held = Input.GetButton("Fire1");
        if (held && !last)
        {
            if (text)
                finish = true;
            
            else
                Texticles();
        }

        last = held;
    }

    void Texticles()
    {
        switch (MoveText)
        {
            case 0:
                StartCoroutine(TurnTextOn(0));
                break;

            case 1:
                StartCoroutine(TurnTextOn(1));
                break;

            case 2:
                StartCoroutine(TurnTextOn(2));
                break;

            case 3:
                StartCoroutine(TurnTextOn(3));
                break;

            case 4:
                TrophyPedestal.enabled = true;
                break;

            case 5:
                StartCoroutine(TurnTextOn(4));
                break;

            case 6:
                StartCoroutine(TurnTextOn(5));
                break;

            case 7:
                Station1Pedestal.enabled = true;
                Station2Pedestal.enabled = true;
                break;

            case 8:
                GVRSystem.SetActive(false);
                StartCoroutine(TurnTextOn(6));
                break;

            case 9:
                StartCoroutine(TurnTextOn(7));
                break;

            case 10:
                StartCoroutine(TurnTextOn(8));
                break;

            case 11:
                StartCoroutine(TurnTextOn(9));
                break;

            case 12:
                GVRSystem.SetActive(true);
                Text.text = " ";
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
            {
                Text.text = TextList[index];
            }
            else
            {
                Text.text = Text.text.Insert(Text.text.Length - 1, TextList[index][Text.text.Length - 1].ToString());
            }
        }

        Text.text = TextList[index];
        finish = false;
        text = false;
        MoveText += 1;
    }
}
