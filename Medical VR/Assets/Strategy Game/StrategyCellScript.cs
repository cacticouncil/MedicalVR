using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyCellScript : MonoBehaviour
{
    public int reproduction = 1;
    public int defense = 1;
    public int immunity = 1;
    public int repCap = 5, defCap = 5, immCap = 15;
    public TMPro.TextMeshPro r;
    public TMPro.TextMeshPro d;
    public TMPro.TextMeshPro i;
    public TMPro.TextMeshPro p;
    public Renderer render;
    public Vector2 key;
    public bool targeted = false;
    public bool hosted = false;
    public int Treproduction = 10;
    public int reproductionReset = 50;
    public int rBonus = 5;
    public int powerupDuration = 25;

    [System.NonSerialized]
    public GameObject virus;

    public int turnSpawned = 0;
    public int childrenSpawned = 0;
    public int immunitySpread = 0;

    public int RDur = 0;
    public int I2Dur = 0;

    private StrategyCellManagerScript parent;
    public enum Proteins
    {
        None,
        RNase_L,
        PKR,
        TRIM22,
        IFIT,
        CH25H,
        Mx1
    }
    public Proteins protein = Proteins.None;

    void Awake()
    {
        if (!r || !d || !i || !p)
        {
            TMPro.TextMeshPro[] arr = GetComponentsInChildren<TMPro.TextMeshPro>(true);
            r = arr[3];
            d = arr[4];
            i = arr[5];
            p = arr[6];

            Debug.Log("TextMesh Set");
        }
    }

    void Start()
    {
        parent = transform.parent.GetComponent<StrategyCellManagerScript>();
        turnSpawned = parent.turnNumber;
        parent.cells.Add(this);
        r.text = "Reproduction: " + reproduction;
        d.text = "Defense: " + defense;
        i.text = "Immunity: " + immunity;
        p.text = "Protein: " + protein.ToString();
    }

    public void IncreaseReproduction()
    {
        if (!hosted && reproduction < repCap)
        {
            reproduction++;
            if (r)
            {
                if (RDur > 0)
                {
                    r.text = "Reproduction: " + reproduction + rBonus;
}
                else
                {
                    r.text = "Reproduction: " + reproduction;
                }
            }
            else
            {
                Debug.Log("Error! Reproduction TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
            parent.ActionPreformed();
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
            parent.ActionPreformed();
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
                protein = (Proteins)Random.Range(1, 7);
                p.text = "Protein: " + protein.ToString();
                Debug.Log("Cell gained protein " + protein.ToString());
            }
            if (i)
                i.text = "Immunity: " + immunity;
            else
            {
                Debug.Log("Error! Immunity TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
            parent.ActionPreformed();
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
                protein = (Proteins)Random.Range(1, 7);
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

    public void IncreaseImmunityToMax()
    {
        immunity = immCap;
        protein = (Proteins)Random.Range(1, 7);
        p.text = "Protein: " + protein.ToString();
        Debug.Log("Cell gained protein " + protein.ToString());
        if (i)
            i.text = "Immunity: " + immunity;
        else
        {
            Debug.Log("Error! Immunity TextMesh not instantiated. Key: " + key.x + "_" + key.y);
        }
    }

    public void UseR()
    {
        //check for item
        if (parent.inventory[0].count > 0)
        {
            parent.inventory[0].count--;
            RDur += powerupDuration;
            r.color = Color.blue;
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
        }
    }
    public void UseR2()
    {
        //check for item
        if (parent.inventory[1].count > 0)
        {
            parent.inventory[1].count--;
            parent.DuplicateCell(key, new Vector4(reproduction, defense, immunity, (int)protein));
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
        }
    }
    public void UseD()
    {
        //check for item
        if (defense != defCap && parent.inventory[2].count > 0)
        {
            parent.inventory[2].count--;
            IncreaseDefenseToMax();
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
        }
    }
    public void UseI()
    {
        //check for item
        if (immunity != immCap && parent.inventory[3].count > 0)
        {
            parent.inventory[3].count--;
            IncreaseImmunityToMax();
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
        }
    }
    public void UseI2()
    {
        //check for item
        if (parent.inventory[4].count > 0)
        {
            parent.inventory[4].count--;
            I2Dur += powerupDuration;
            i.color = Color.red;
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
        }
    }
    public void UseP()
    {
        //check for item
        if (protein != Proteins.None && parent.inventory[5].count > 0)
        {
            parent.inventory[5].count--;
            Proteins prev = protein;
            while (protein == prev)
                protein = (Proteins)Random.Range(1, 7);
            p.text = "Protein: " + protein.ToString();
            Debug.Log("Cell gained protein " + protein.ToString());
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
        }
    }
    public void UseV()
    {
        //check for item
        if (hosted && parent.inventory[6].count > 0)
        {
            parent.inventory[6].count--;
            Destroy(virus);
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
        }
    }

    public void TurnUpdate()
    {
        if (!hosted)
        {
            if (RDur > 0)
            {
                Treproduction -= rBonus;
                RDur--;
                if (RDur == 0)
                {
                    r.color = Color.white;
                }
            }
            Treproduction -= reproduction;
            if (Treproduction <= 0)
            {
                //reproduce
                parent.SelectCellSpawn(key);
                childrenSpawned++;
                Treproduction = reproductionReset + Treproduction;
            }
        }
    }

    public void DelayedTurnUpdate()
    {
        if (immunity >= immCap)
        {
            //spread immunity
            immunitySpread += parent.SpreadImmunity(key);
            if (I2Dur > 0)
            {
                immunitySpread += parent.SpreadImmunity(key);
                I2Dur--;
                if (I2Dur == 0)
                {
                    i.color = Color.white;
                }
            }
        }

        if (hosted)
        {
            render.material.color = Color.red;
        }
        else if (targeted)
        {
            render.material.color = Color.green;
        }
        else
        {
            render.material.color = Color.white;
        }

        if (transform.GetChild(0).gameObject.activeSelf)
            transform.GetChild(0).GetComponent<StrategyUIScript>().Refresh();
    }

    public void ToggleUI(bool b)
    {
        transform.GetChild(0).gameObject.SetActive(b);
    }

    void OnDestroy()
    {
        parent.cells.Remove(this);
    }
}