using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyCellScript : MonoBehaviour
{
    public int reproduction = 1;
    public int defense = 1;
    public int immunity = 1;
    public int repCap = 5, defCap = 5, immCap = 15;
    public TextMesh r;
    public TextMesh d;
    public TextMesh i;
    public TextMesh p;
    public Vector2 key;
    public bool targeted = false;
    public bool hosted = false;
    public int Treproduction = 10;

    public bool[] powerups = new bool[5];
    public short[] powerupDurations = new short[5];

    public int turnSpawned = 0;
    public int childrenSpawned = 0;
    public int immunitySpread = 0;
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
        if (!r || !d || !i || !p)
        {
            TextMesh[] arr = GetComponentsInChildren<TextMesh>(true);
            r = arr[3];
            d = arr[4];
            i = arr[5];
            p = arr[6];

            Debug.Log("TextMesh Set");
        }
    }

    void Start()
    {
        turnSpawned = transform.GetComponentInParent<StrategyCellManagerScript>().turnNumber;
    }

    public void IncreaseReproduction()
    {
        if (!hosted && reproduction < repCap)
        {
            reproduction++;
            if (r)
                r.text = "Reproduction: " + reproduction;
            else
            {
                Debug.Log("Error! Reproduction TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
            gameObject.transform.parent.GetComponent<StrategyCellManagerScript>().ActionPreformed();
        }
    }

    public void IncreaseDefense()
    {
        if (!hosted && defense < defCap)
        {
            defense++;
            if (d)
                d.text = "Defense: " + defense;
            else
            {
                Debug.Log("Error! Defense TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
            gameObject.transform.parent.GetComponent<StrategyCellManagerScript>().ActionPreformed();
        }
    }

    public void IncreaseDefenseToMax()
    {
        defense = defCap;
        if (d)
            d.text = "Defense: " + defense;
        else
        {
            Debug.Log("Error! Defense TextMesh not instantiated. Key: " + key.x + "_" + key.y);
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
                Debug.Log("Cell gained protein " + protein.ToString());
            }
            if (i)
                i.text = "Immunity: " + immunity;
            else
            {
                Debug.Log("Error! Immunity TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
            gameObject.transform.parent.GetComponent<StrategyCellManagerScript>().ActionPreformed();
        }
    }
    //Called from cell manager
    public bool AddImmunity()
    {
        bool ret = false;
        if (immunity < immCap)
        {
            immunity++;
            ret = true;
            if (immunity == immCap)
            {
                protein = (proteins)Random.Range(1, 7);
                p.text = "Protein: " + protein.ToString();
                Debug.Log("Cell gained protein " + protein.ToString());
            }
            if (i)
                i.text = "Immunity: " + immunity;
            else
            {
                Debug.Log("Error! Immunity TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
        }
        return ret;
    }

    public void UsePowerup(byte index, short duration)
    {
        switch (index)
        {
            case 0:
                {
                    //check for item
                    powerups[index] = true;
                    powerupDurations[index] += duration;
                }
                break;
            case 2:
                {
                    //check for item
                    powerups[index] = true;
                    powerupDurations[index] += duration;
                }
                break;
            case 3:
                {
                    if (defense != defCap) //and have item)
                        IncreaseDefenseToMax();
                }
                break;
            case 4:
                {
                    //check for item
                    powerups[index] = true;
                    powerupDurations[index] += duration;
                }
                break;
            case 5:
                {
                    //check for item
                    powerups[index] = true;
                    powerupDurations[index] += duration;
                }
                break;
            default:
                break;
        }
    }

    public void TurnUpdate()
    {
        if (!hosted)
        {
            Treproduction -= powerups[0] ? reproduction + 5 : reproduction;
            if (Treproduction <= 0)
            {
                //reproduce
                gameObject.transform.parent.GetComponent<StrategyCellManagerScript>().SelectCellSpawn(key);
                childrenSpawned++;
                Treproduction = 10 + Treproduction;
            }
        }
    }

    public void DelayedTurnUpdate()
    {
        if (immunity >= immCap)
        {
            //spread immunity
            immunitySpread += gameObject.transform.parent.GetComponent<StrategyCellManagerScript>().SpreadImmunity(key);
            if (powerups[5])
                immunitySpread += gameObject.transform.parent.GetComponent<StrategyCellManagerScript>().SpreadImmunity(key);
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

        for (int i = 0; i < powerupDurations.Length; i++)
        {
            if (powerupDurations[i] > 0)
            {
                powerupDurations[i]--;
                if (powerupDurations[i] == 0)
                {
                    powerups[i] = false;
                }
            }
        }
    }

    public void ToggleUI(bool b)
    {
        transform.GetChild(0).gameObject.SetActive(b);
    }
}