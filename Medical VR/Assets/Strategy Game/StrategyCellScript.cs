using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyCellScript : MonoBehaviour
{
    public int reproduction = 1;
    public int defense = 1;
    public int immunity = 1;
    public Text r;
    public Text d;
    public Text i;
    public Vector2 key;
    public bool targeted = false;
    public bool hosted = false;
    public int Treproduction = 10;

    private int repCap = 5, defCap = 5, immCap = 10;
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

    void Update()
    {
        if (!hosted)
        {
            if (entered == exited)
                timeOut += Time.deltaTime;
            if (timeOut > 5.0f)
                ToggleUI(false);
        }
        else
            ToggleUI(false);
    }

    public void IncreaseReproduction()
    {
        if (!hosted || immunity >= 10)
            if (reproduction < repCap)
            {
                reproduction++;
                if (r)
                    r.text = "Reproduction: " + reproduction;
                else
                {
                    Debug.Log("Error! Reproduction Text not instantiated. Key: " + key.x + "_" + key.y);
                }
                gameObject.GetComponentInParent<StrategyCellManagerScript>().ActionPreformed();
            }
    }

    public void IncreaseDefense()
    {
        if (!hosted || immunity >= 10)
            if (defense < defCap)
            {
                defense++;
                if (d)
                    d.text = "Defense: " + defense;
                else
                {
                    Debug.Log("Error! Defense Text not instantiated. Key: " + key.x + "_" + key.y);
                }
                gameObject.GetComponentInParent<StrategyCellManagerScript>().ActionPreformed();
            }
    }
    //Called from cell's UI
    public void IncreaseImmunity()
    {
        if (!hosted || immunity >= 10)
            if (immunity < immCap)
            {
                immunity++;
                if (i)
                    i.text = "Immunity: " + immunity;
                else
                {
                    Debug.Log("Error! Immunity Text not instantiated. Key: " + key.x + "_" + key.y);
                }
                gameObject.GetComponentInParent<StrategyCellManagerScript>().ActionPreformed();
            }
    }
    //Called from cell manager
    public void AddImmunity()
    {
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
        if (!hosted || immunity >= 10)
        {
            Treproduction -= reproduction;
            if (Treproduction <= 0)
            {
                //reproduce
                gameObject.GetComponentInParent<StrategyCellManagerScript>().SelectCellSpawn(key);
                Treproduction = 10 + Treproduction;
            }
        }
    }

    public void DelayedTurnUpdate()
    {
        if (immunity >= immCap)
        {
            //spread immunity
            gameObject.GetComponentInParent<StrategyCellManagerScript>().SpreadImmunity(key);
        }

        if (hosted)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (targeted)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
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