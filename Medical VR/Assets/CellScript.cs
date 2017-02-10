using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CellScript : MonoBehaviour
{
    public int reproduction = 1;
    public int defense = 1;
    public int immunity = 1;

    private int repCap = 5, defCap = 5, immCap = 10;

    public Text r;
    public Text d;
    public Text i;

    public Vector2 key;

    private int Treproduction = 10;
    private int entered = 0, exited = 0;
    private float timeOut = 0.0f;

    void Awake()
    {
        if (!r || !d || !i)
        {
            Text[] arr = GetComponentsInChildren<Text>(true);
            r = arr[3];
            d = arr[4];
            i = arr[5];

            Debug.Log("Text Set");
        }
    }

    void FixedUpdate()
    {
        if (entered == exited)
            timeOut += Time.fixedDeltaTime;
        if (timeOut > 5.0f)
            ToggleUI(false);
    }

    public void IncreaseReproduction()
    {
        if (reproduction < repCap)
        {
            reproduction++;
            if (r)
                r.text = "Reproduction: " + reproduction;
            else
            {
                Debug.Log("Error! Reproduction Text not instantiated. Key: " + key.x + "_" + key.y);
            }
            gameObject.GetComponentInParent<CellManagerScript>().ActionPreformed();
        }
    }

    public void IncreaseDefense()
    {
        if (defense < defCap)
        {
            defense++;
            if (d)
                d.text = "Defense: " + defense;
            else
            {
                Debug.Log("Error! Defense Text not instantiated. Key: " + key.x + "_" + key.y);
            }
            gameObject.GetComponentInParent<CellManagerScript>().ActionPreformed();
        }
    }
    //Called from cell's UI
    public void IncreaseImmunity()
    {
        //Debug.Log("Attempting to increase Immunity");
        if (immunity < immCap)
        {
            immunity++;
            if (i)
                i.text = "Immunity: " + immunity;
            else
            {
                Debug.Log("Error! Immunity Text not instantiated. Key: " + key.x + "_" + key.y);
            }
            gameObject.GetComponentInParent<CellManagerScript>().ActionPreformed();
        }
    }
    //Called from cell manager
    public void AddImmunity()
    {
        //Debug.Log("Attempting to add Immunity");
        if (immunity < immCap)
        {
            immunity++;
            if (i)
                i.text = "Immunity: " + immunity;
            else
            {
                Debug.Log("Error! Immunity Text not instantiated. Key: " + key.x + "_" + key.y);
            }
        }
    }

    public void TurnUpdate()
    {
        Treproduction -= reproduction;
        if (Treproduction <= 0)
        {
            //reproduce
            gameObject.GetComponentInParent<CellManagerScript>().SpawnCell(key);
            Treproduction = 10 + Treproduction;
        }
    }

    public void DelayedTurnUpdate()
    {
        if (immunity >= immCap)
        {
            //spread immunity
            gameObject.GetComponentInParent<CellManagerScript>().SpreadImmunity(key);
        }
    }

    public void Infect()
    {

    }

    public void ToggleUI(bool b)
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(b);
        timeOut = 0.0f;
    }

    public void Enter()
    {
        entered++;
        ResetTimeout();
    }

    public void Exit()
    {
        exited++;
    }

    public void ResetTimeout()
    {
        timeOut = 0.0f;
    }
}