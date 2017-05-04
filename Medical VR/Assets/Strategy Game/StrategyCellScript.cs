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
    public float reproduction = 0, defense = 0, immunity = 0;
    public Proteins protein = Proteins.None;
    public TMPro.TextMeshPro r;
    public TMPro.TextMeshPro d;
    public TMPro.TextMeshPro i;
    public TMPro.TextMeshPro p;
    public Renderer render;
    public Vector2 key;
    public bool targeted = false;
    public bool hosted = false;
    public float Treproduction = 50;
    public int reproductionReset = 50;
    public float rBonus = 15, dBonus = 5, iBonus = 10;
    public int powerupDuration = 25;

    [System.NonSerialized]
    public GameObject virus;
    public GameObject itemWhiteBloodCellPrefab;

    public int turnSpawned = 0;
    public int childrenSpawned = 0;
    public float immunitySpread = 0;
    public float startSpeed = 15.0f;

    public int RDur = 0;
    public int I2Dur = 0;

    public StrategyCellManagerScript parent;
    public ParticleSystem deathParticles;

    public TMPro.TextMeshPro[] texts;

    private float tImmunity;

    private MoveCamera mainCamera;
    private float camOffset = 1.5f;
    private float scaledDistance = 1.3f;


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
        if (mainCamera == null)
        {
            mainCamera = Camera.main.GetComponent<MoveCamera>();
        }
        turnSpawned = parent.turnNumber;
        parent.cells.Add(this);
        parent.cellsSpawned++;
        r.text = "Reproduction: " + reproduction;
        d.text = "Defense: " + defense;
        i.text = "Immunity: " + (int)immunity;
        p.text = "Protein: " + protein.ToString();
        startSpeed = Random.Range(1.0f, 30.0f);
        tImmunity = immunity;
    }

    #region IncreasingStats
    public void IncreaseReproduction()
    {
        if (!hosted)
        {
            reproduction++;
            if (r)
            {
                if (RDur > 0)
                {
                    r.text = "Reproduction: " + (reproduction + rBonus);
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
        if (!hosted)
        {
            defense++;
            if (d)
            {
                d.text = "Defense: " + defense;
            }
            else
            {
                Debug.Log("Error! Defense TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
            parent.ActionPreformed();
        }
    }

    public void IncreaseDefenseGreatly()
    {
        defense += dBonus;
        if (d)
        {
            d.text = "Defense: " + defense;
        }
        else
        {
            Debug.Log("Error! Defense TextMesh not instantiated. Key: " + key.x + "_" + key.y);
        }
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
                p.text = "Protein: " + protein.ToString();
                Debug.Log("Cell gained protein " + protein.ToString());
            }
            if (i)
            {
                i.text = "Immunity: " + (int)immunity;
            }
            else
            {
                Debug.Log("Error! Immunity TextMesh not instantiated. Key: " + key.x + "_" + key.y);
            }
            parent.ActionPreformed();
        }
    }
    //Called from cell manager
    public void AddImmunity(float imm)
    {
        immunity += imm;
        if (protein == Proteins.None && immunity >= 10f)
        {
            protein = (Proteins)Random.Range(1, 7);
            p.text = "Protein: " + protein.ToString();
        }
        if (i)
        {
            i.text = "Immunity: " + (int)immunity;
        }
        else
        {
            Debug.Log("Error! Immunity TextMesh not instantiated. Key: " + key.x + "_" + key.y);
        }
    }

    public void IncreaseImmunityGreatly()
    {
        immunity += iBonus;
        if (protein == Proteins.None && immunity >= 10f)
        {
            protein = (Proteins)Random.Range(1, 7);
            p.text = "Protein: " + protein.ToString();
        }
        if (i)
        {
            i.text = "Immunity: " + (int)immunity;
        }
        else
        {
            Debug.Log("Error! Immunity TextMesh not instantiated. Key: " + key.x + "_" + key.y);
        }
    }
    #endregion

    #region PowerUps
    public void UseR()
    {
        //check for item
        if (!hosted && parent.inventory[0].count > 0)
        {
            parent.inventory[0].count--;
            RDur += powerupDuration;
            r.color = Color.blue;
            r.text = "Reproduction: " + (reproduction + rBonus);
            RefreshUI();
        }
    }
    public void UseR2()
    {
        //check for item
        if (!hosted && parent.inventory[1].count > 0)
        {
            parent.inventory[1].count--;
            parent.duplicate = true;
            parent.SelectCellSpawn(key);
            RefreshUI();
        }
    }
    public void UseD()
    {
        //check for item
        if (!hosted && parent.inventory[2].count > 0)
        {
            parent.inventory[2].count--;
            IncreaseDefenseGreatly();
            RefreshUI();
        }
    }
    public void UseI()
    {
        //check for item
        if (!hosted && parent.inventory[3].count > 0)
        {
            parent.inventory[3].count--;
            IncreaseImmunityGreatly();
            RefreshUI();
        }
    }
    public void UseI2()
    {
        //check for item
        if (!hosted && parent.inventory[4].count > 0)
        {
            parent.inventory[4].count--;
            I2Dur += powerupDuration;
            i.color = Color.red;
            RefreshUI();
        }
    }
    public void UseP()
    {
        //check for item
        if (!hosted && protein != Proteins.None && parent.inventory[5].count > 0)
        {
            parent.inventory[5].count--;
            Proteins prev = protein;
            while (protein == prev)
            {
                protein = (Proteins)Random.Range(1, 7);
            }
            p.text = "Protein: " + protein.ToString();
            RefreshUI();
        }
    }
    public void UseV()
    {
        //check for item
        if (hosted && parent.inventory[6].count > 0)
        {
            parent.inventory[6].count--;
            ToggleUI(false);
            //RefreshUI();
            Vector3 camPos = Camera.main.GetComponent<Transform>().position;
            Vector3 dir = camPos - transform.position;
            dir.Normalize();
            Camera.main.GetComponent<MoveCamera>().SetDestination(camPos + dir);
            Invoke("SpawnW", .5f);
        }
    }

    void SpawnW()
    {
        GameObject w = Instantiate(itemWhiteBloodCellPrefab, Camera.main.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
        w.GetComponent<StrategyItemWhiteBloodCell>().target = virus.GetComponent<StrategyVirusScript>();
        w.GetComponent<StrategyItemWhiteBloodCell>().enabled = true;
    }
    #endregion

    #region Tutorials
    public void Reproduction()
    {
        parent.str.enabled = true;
    }
    public void Defense()
    {
        parent.std.enabled = true;
    }
    public void Immunity()
    {
        parent.sti.enabled = true;
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
                parent.SelectCellSpawn(key);
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
                    immunitySpread += parent.SpreadImmunity(key, im * 2.0f);
                    I2Dur--;
                    if (I2Dur == 0)
                    {
                        i.color = Color.white;
                    }
                }
                else
                {
                    immunitySpread += parent.SpreadImmunity(key, im);
                }
            }
        }

        if (hosted || targeted)
        {
            if (render)
            {
                if (render.material.color != Color.black)
                {
                    StopCoroutine("ChangeColorOverTime");
                    StartCoroutine(ChangeColorOverTime(Color.black));
                }
            }
            else
            {
                Debug.Log("Cell " + gameObject.name + " does not have a renderer.");
            }
        }
        else
        {
            if (render.material.color != Color.grey)
            {
                StopCoroutine("ChangeColorOverTime");
                StartCoroutine(ChangeColorOverTime(Color.grey));
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
        for (int i = 0; i < 7; i++)
        {
            texts[i].text = parent.inventory[i].count.ToString();
            if (parent.inventory[i].count > 0)
            {
                texts[i].color = Color.white;
            }
            else
            {
                texts[i].color = Color.red;
            }
        }
        if (hosted)
        {
            foreach (TMPro.TextMeshPro text in texts)
            {
                text.color = Color.red;
            }
            if (parent.inventory[6].count > 0)
            {
                texts[6].color = Color.white;
            }
        }
    }
    #endregion

    IEnumerator ChangeColorOverTime(Color c)
    {
        float startTime = Time.time;
        float t = Time.time - startTime;
        Color start = render.material.color;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            render.material.color = Color.Lerp(start, c, t);
            yield return 0;
        }
    }


    public void MoveTo()
    {
        if ((parent.viewingStats && parent.selected != transform.GetComponent<StrategyCellScript>().key) || (!parent.viewingStats && parent.selected == transform.GetComponent<StrategyCellScript>().key))
        {
            Vector3 direction = mainCamera.transform.forward;
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
            mainCamera.SetDestination(finalPos);
            parent.SetSelected(transform.GetComponent<StrategyCellScript>().key);
            gameObject.GetComponent<StrategyCellScript>().ToggleUI(true);
            parent.viewingStats = true;
        }
        else if (!parent.viewingStats)
        {
            mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
            parent.SetSelected(transform.GetComponent<StrategyCellScript>().key);
            parent.viewingStats = false;
        }
    }

    public void Back()
    {
        mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
        parent.SetSelected(transform.GetComponent<StrategyCellScript>().key);
        parent.viewingStats = false;
    }

    public IEnumerator Die()
    {
        GetComponent<Collider>().enabled = false;
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