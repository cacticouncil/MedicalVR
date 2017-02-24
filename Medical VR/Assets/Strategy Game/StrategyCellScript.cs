using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyCellScript : MonoBehaviour
{
    public int reproduction = 1;
    public int defense = 1;
    public int immunity = 1;
    public int repCap = 5, defCap = 5, immCap = 15;
    public Text r;
    public Text d;
    public Text i;
    public Text p;
    public Vector2 key;
    public bool targeted = false;
    public bool hosted = false;
    public int Treproduction = 10;
    public enum proteins
    {
        None,
        RNase_L,
        PKR,
        TRIM22,
        IFIT,
        CH25H,
        Mx1
    }
    public proteins protein = proteins.None;

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

    public void IncreaseReproduction()
    {
        if (!hosted || immunity >= immCap)
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
        if (!hosted || immunity >= immCap)
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
        if (!hosted && immunity < immCap)
        {
            immunity++;
            if (immunity == immCap)
            {
                protein = (proteins)Random.Range(1, 7);
                p.text = "Protein: " + protein.ToString();
                Debug.Log("Cell has gained Immunity");
            }
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
            if (immunity == immCap)
            {
                protein = (proteins)Random.Range(1, 7);
                p.text = "Protein: " + protein.ToString();
                Debug.Log("Cell has gained Immunity");
            }
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
        if (!hosted || immunity >= immCap)
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
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (targeted)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
			GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void ToggleUI(bool b)
    {
        transform.GetChild(0).gameObject.SetActive(b);
    }
}