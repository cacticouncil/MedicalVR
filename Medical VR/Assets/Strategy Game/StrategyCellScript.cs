using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

public class StrategyCellScript : MonoBehaviour
{
    //Statics
    public static int reproductionReset = 50;
    public static Color
        //None
        proteinYellow = new Color32(200, 200, 0, 255),
        //RNase_L, PKR
        proteinPurple = new Color32(135, 45, 135, 255),
        //TRIM22, IFIT
        proteinRed = new Color32(215, 25, 25, 255),
        //CH25H, MX1
        proteinBlue = new Color32(30, 30, 150, 255);
    public static float rBonus = 15, dBonus = 5, iBonus = 10;
    public static int powerupDuration = 25;

    //In inspector
    public float reproduction = 0, defense = 0, immunity = 0;
    public Proteins protein = Proteins.None;
    public Renderer proteinRen;
    public Renderer render;
    public GameObject itemWhiteBloodCellPrefab;
    public ParticleSystem deathParticles;
    public Vector2 key;
    public bool targeted = false, hosted = false;
    public TMPro.TextMeshPro r, d, i, p;
    public TMPro.TextMeshPro[] texts;

    //Not in inspector
    [System.NonSerialized]
    public float Treproduction = 50;
    [System.NonSerialized]
    public int RDur = 0, I2Dur = 0;
    [System.NonSerialized]
    public StrategyVirusScript virus;
    [System.NonSerialized]
    public int turnSpawned = 0, childrenSpawned = 0;
    [System.NonSerialized]
    public float immunitySpread = 0, startSpeed = 15.0f;

    //Private
    private float tImmunity, camOffset = 1.5f, scaledDistance = 1.3f;

    void Awake()
    {
        if (!r || !d || !i || !p)
        {
            TMPro.TextMeshPro[] arr = GetComponentsInChildren<TMPro.TextMeshPro>(true);
            r = arr[3];
            d = arr[4];
            i = arr[5];
            p = arr[6];

            Debug.Log("Cell TextMesh Set");
        }
    }

    void Start()
    {
        turnSpawned = StrategyCellManagerScript.instance.turnNumber;
        StrategyCellManagerScript.instance.cells.Add(this);
        StrategyCellManagerScript.instance.cellsSpawned++;
        r.text = "Reproduction: " + reproduction;
        d.text = "Defense: " + defense;
        i.text = "Immunity: " + (int)immunity;
        p.text = "Protein: " + protein.ToString();
        ChangeProteinColor();
        startSpeed = Random.Range(1.0f, 30.0f);
        tImmunity = immunity;
    }

    #region IncreasingStats
    public void IncreaseReproduction()
    {
        if (!hosted)
        {
            reproduction++;
            if (RDur > 0)
            {
                r.text = "Reproduction: " + (reproduction + rBonus);
            }
            else
            {
                r.text = "Reproduction: " + reproduction;
            }
            StrategyCellManagerScript.instance.ActionPreformed();
        }
    }

    public void IncreaseDefense()
    {
        if (!hosted)
        {
            defense++;
            d.text = "Defense: " + defense;
            StrategyCellManagerScript.instance.ActionPreformed();
        }
    }

    public void IncreaseDefenseGreatly()
    {
        defense += dBonus;
        d.text = "Defense: " + defense;
    }

    //Called from cell's UI
    public void IncreaseImmunity()
    {
        if (!hosted)
        {
            immunity++;
            if (protein == Proteins.None && immunity >= 10f)
            {
                protein = (Proteins)Random.Range(1, 7);
                ChangeProteinColor();
                p.text = "Protein: " + protein.ToString();
            }
            i.text = "Immunity: " + (int)immunity;
            StrategyCellManagerScript.instance.ActionPreformed();
        }
    }
    //Called from cell manager
    public void AddImmunity(float imm)
    {
        immunity += imm;
        if (protein == Proteins.None && immunity >= 10f)
        {
            protein = (Proteins)Random.Range(1, 7);
            ChangeProteinColor();
            p.text = "Protein: " + protein.ToString();
        }
        i.text = "Immunity: " + (int)immunity;
    }

    public void IncreaseImmunityGreatly()
    {
        immunity += iBonus;
        if (protein == Proteins.None && immunity >= 10f)
        {
            protein = (Proteins)Random.Range(1, 7);
            ChangeProteinColor();
            p.text = "Protein: " + protein.ToString();
        }
        i.text = "Immunity: " + (int)immunity;
    }
    #endregion

    #region PowerUps
    public void UseR()
    {
        //check for item
        if (StrategyCellManagerScript.instance.inventory[0].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[0].count--;
            RDur += powerupDuration;
            r.color = Color.blue;
            r.text = "Reproduction: " + (reproduction + rBonus);
            RefreshUI();
        }
    }
    public void UseR2()
    {
        //check for item
        if (StrategyCellManagerScript.instance.inventory[1].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[1].count--;
            StrategyCellManagerScript.instance.duplicate = true;
            StrategyCellManagerScript.instance.SelectCellSpawn(key);
            RefreshUI();
        }
    }
    public void UseD()
    {
        //check for item
        if (StrategyCellManagerScript.instance.inventory[2].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[2].count--;
            IncreaseDefenseGreatly();
            RefreshUI();
        }
    }
    public void UseI()
    {
        //check for item
        if (StrategyCellManagerScript.instance.inventory[3].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[3].count--;
            IncreaseImmunityGreatly();
            RefreshUI();
        }
    }
    public void UseI2()
    {
        //check for item
        if (StrategyCellManagerScript.instance.inventory[4].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[4].count--;
            I2Dur += powerupDuration;
            i.color = Color.red;
            RefreshUI();
        }
    }
    public void UseP()
    {
        //check for item
        if (protein != Proteins.None && StrategyCellManagerScript.instance.inventory[5].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[5].count--;
            Proteins prev = protein;
            while (protein == prev)
            {
                protein = (Proteins)Random.Range(1, 7);
            }
            ChangeProteinColor();
            p.text = "Protein: " + protein.ToString();
            RefreshUI();
        }
    }
    public void UseV()
    {
        //check for item
        if (hosted && StrategyCellManagerScript.instance.inventory[6].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[6].count--;
            ToggleUI(false);
            Vector3 camPos = Camera.main.GetComponent<Transform>().position;
            Vector3 dir = camPos - transform.position;
            dir.Normalize();
            MoveCamera.instance.SetDestination(camPos + dir);
            Invoke("SpawnW", .5f);
        }
    }

    void SpawnW()
    {
        GameObject w = Instantiate(itemWhiteBloodCellPrefab, Camera.main.GetComponent<Transform>().position, Quaternion.identity, transform.parent) as GameObject;
        w.GetComponent<StrategyItemWhiteBloodCell>().target = virus;
        w.GetComponent<StrategyItemWhiteBloodCell>().enabled = true;
    }
    #endregion

    #region Tutorials
    public void Reproduction()
    {
        StrategyCellManagerScript.instance.str.enabled = true;
    }
    public void Defense()
    {
        StrategyCellManagerScript.instance.std.enabled = true;
    }
    public void Immunity()
    {
        StrategyCellManagerScript.instance.sti.enabled = true;
    }
    #endregion

    #region Turns
    public void TurnUpdate()
    {
        tImmunity = immunity;
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
            Treproduction -= Mathf.Sqrt(reproduction * 10 + 1);
            while (Treproduction <= 0)
            {
                //reproduce
                StrategyCellManagerScript.instance.SelectCellSpawn(key);
                childrenSpawned++;
                Treproduction = reproductionReset + Treproduction;
            }
        }
    }

    public void DelayedTurnUpdate()
    {
        if (!hosted)
        {
            //spread immunity
            if (tImmunity >= 1.0f)
            {
                float im = tImmunity * .01f;
                if (I2Dur > 0)
                {
                    immunitySpread += StrategyCellManagerScript.instance.SpreadImmunity(key, im * 2.0f);
                    I2Dur--;
                    if (I2Dur == 0)
                    {
                        i.color = Color.white;
                    }
                }
                else
                {
                    immunitySpread += StrategyCellManagerScript.instance.SpreadImmunity(key, im);
                }
            }
        }

        if (hosted || targeted)
        {
            if (render.material.color != Color.black)
            {
                StopCoroutine("ChangeColorOverTime");
                StartCoroutine(ChangeColorOverTime(render.material, Color.black));
            }
        }
        else
        {
            if (render.material.color != Color.grey)
            {
                StopCoroutine("ChangeColorOverTime");
                StartCoroutine(ChangeColorOverTime(render.material, Color.grey));
            }
        }

        if (transform.GetChild(0).gameObject.activeSelf)
        {
            RefreshUI();
        }
    }
    #endregion

    #region UI
    public void ToggleUI(bool b)
    {
        transform.GetChild(0).gameObject.SetActive(b);
        if (b)
        {
            RefreshUI();
        }
    }

    public void RefreshUI()
    {
        for (int i = 0; i < 6; i++)
        {
            texts[i].text = StrategyCellManagerScript.instance.inventory[i].count.ToString();
            if (StrategyCellManagerScript.instance.inventory[i].count > 0)
            {
                texts[i].color = Color.white;
            }
            else
            {
                texts[i].color = Color.red;
            }
        }
        if (hosted && StrategyCellManagerScript.instance.inventory[6].count > 0)
        {
            texts[6].color = Color.white;
        }
        else
        {
            texts[6].color = Color.red;
        }
    }
    #endregion

    private void ChangeProteinColor()
    {
        switch (protein)
        {
            case Proteins.None:
                if (StrategyCellManagerScript.instance.selected == key)
                    StartCoroutine(ChangeColorOverTime(proteinRen.material, proteinYellow));
                else
                    proteinRen.material.color = proteinYellow;
                break;
            case Proteins.RNase_L:
            case Proteins.PKR:
                if (StrategyCellManagerScript.instance.selected == key)
                    StartCoroutine(ChangeColorOverTime(proteinRen.material, proteinPurple));
                else
                    proteinRen.material.color = proteinPurple;
                break;
            case Proteins.TRIM22:
            case Proteins.IFIT:
                if (StrategyCellManagerScript.instance.selected == key)
                    StartCoroutine(ChangeColorOverTime(proteinRen.material, proteinRed));
                else
                    proteinRen.material.color = proteinRed;
                break;
            case Proteins.CH25H:
            case Proteins.Mx1:
                if (StrategyCellManagerScript.instance.selected == key)
                    StartCoroutine(ChangeColorOverTime(proteinRen.material, proteinBlue));
                else
                    proteinRen.material.color = proteinBlue;
                break;
        }
    }

    IEnumerator ChangeColorOverTime(Material material, Color c)
    {
        float startTime = Time.time;
        float t = Time.time - startTime;
        Color start = material.color;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            material.color = Color.Lerp(start, c, t);
            yield return 0;
        }
    }

    public void MoveTo()
    {
        if ((StrategyCellManagerScript.instance.viewingStats && StrategyCellManagerScript.instance.selected != key) || (!StrategyCellManagerScript.instance.viewingStats && StrategyCellManagerScript.instance.selected == key))
        {
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;
            direction.Normalize();
            direction *= scaledDistance;
            if (direction.magnitude == 0)
            {
                direction = new Vector3(1.3f, 0, 0);
            }

            Vector3 finalPos = transform.position - direction;

            transform.GetChild(0).transform.LookAt(finalPos);

            //This is the new target position
            MoveCamera.instance.SetDestination(finalPos);
            StrategyCellManagerScript.instance.SetSelected(key);
            ToggleUI(true);
            StrategyCellManagerScript.instance.viewingStats = true;
        }
        else if (!StrategyCellManagerScript.instance.viewingStats)
        {
            MoveCamera.instance.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
            StrategyCellManagerScript.instance.SetSelected(key);
            StrategyCellManagerScript.instance.viewingStats = false;
        }
    }

    public void Back()
    {
        MoveCamera.instance.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
        StrategyCellManagerScript.instance.SetSelected(key);
        StrategyCellManagerScript.instance.viewingStats = false;
    }

    public IEnumerator Die()
    {
        transform.GetChild(1).GetComponent<Collider>().enabled = false;
        float startTime = Time.time;
        Color c = render.material.color;
        Color o = render.material.GetColor("_OutlineColor");
        deathParticles.Play();

        float t = 0;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = Mathf.Lerp(1, 0, t);
            render.material.color = c;
            o.a = Mathf.Lerp(1, 0, t);
            render.material.SetColor("_OutlineColor", o);
            yield return 0;
        }
        Destroy(gameObject);
    }
}