using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrophyRoomScript : MonoBehaviour
{
    public TextMeshPro Text;
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
        
        Station1Pedestal.enabled = false;
        Station2Pedestal.enabled = false;
        TextList[0] = "Welcome to the Trophy Room";
        TextList[1] = "This is where all of your achievements are shown.";
        TextList[2] = "You can see all the trophies with descriptions that you can unlock.";
        TextList[3] = "You must play mini games in order to do so.";
        TextList[4] = "You can click on the other pedestals to move around in the room.";
        TextList[5] = "You can see the trophies on the shelves are all parts of the cell.";
        TextList[6] = "Click on any trophy to bring it to the pedestal.";
        TextList[7] = "You can see there is a description button that gives you more information about it.";
        TextList[8] = "Click back on the trophy to put it back.";
        TextForTutorial();
    }

    void Update()
    {
        bool held = Input.GetButton("Fire1");
        if (held && !last)
        {
            if (text)
                finish = true;
            
            else
                TextForTutorial();
        }

        last = held;
    }

    void TextForTutorial()
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
                StartCoroutine(TurnTextOn(4));
                break;

            case 5:
                Station1Pedestal.enabled = true;
                Station2Pedestal.enabled = true;
                break;

            case 6:
                GVRSystem.SetActive(false);
                StartCoroutine(TurnTextOn(5));
                break;

            case 7:
                StartCoroutine(TurnTextOn(6));
                break;

            case 8:
                StartCoroutine(TurnTextOn(7));
                break;

            case 9:
                StartCoroutine(TurnTextOn(8));
                break;

            case 10:
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
