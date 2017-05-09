using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class StrategyCellManagerScript : MonoBehaviour
{
    #region Variables
    [Header("Prefabs")]
    public GameObject cellPrefab;
    public GameObject whiteCellPrefab;
    [SerializeField]
    private GameObject virusPrefab1;
    [SerializeField]
    private GameObject virusPrefab2;
    [SerializeField]
    private GameObject virusPrefab3;
    public GameObject transporter;
    public GameObject particleToTarget;
    [Space(2)]

    [Header("Instances")]
    public StrategyBox mysteryBox;
    public SimulateSun sun;
    public GameObject victoryObject;
    public StrategyTutorialReproduction str;
    public StrategyTutorialDefense std;
    public StrategyTutorialImmunity sti;

    //Automatically not shown in inspector
    public Dictionary<Vector2, StrategyCellScript> tiles = new Dictionary<Vector2, StrategyCellScript>(new Vector2Comparer());
    [System.NonSerialized]
    public List<StrategyCellScript> cells = new List<StrategyCellScript>();
    [System.NonSerialized]
    public List<StrategyVirusScript> viruses = new List<StrategyVirusScript>();
    [System.NonSerialized]
    public List<StrategyMigratingWhiteBloodCell> whiteCells = new List<StrategyMigratingWhiteBloodCell>();
    [System.NonSerialized]
    public List<StrategyItem> inventory;
    [System.NonSerialized]
    public int turnNumber = 0, cellNum = 1, virNum = 0, virusKills = 0, cellsSpawned = 0;
    [System.NonSerialized]
    public float immunitySpread;
    [System.NonSerialized]
    public bool duplicate, viewingStats;
    [System.NonSerialized]
    public Vector2 selected = new Vector2(0.0f, 0.0f);
    [System.NonSerialized]
    public int eventTurns = 50;
    [System.NonSerialized]
    public string lastEvent = "None";

    public StrategyEvents nextEvent;

    private Vector2 mysteryBoxIndex = new Vector2(500, 500), victoryIndex = new Vector2(-500, -500), virusIndex = new Vector2(-500, 500);
    private GameObject virus1, virus2, virus3;
    private int easy = 0, medium = 1, hard = 2;
    private int victoryNum = 50;
    private float randomRange = .5f;
    private float p2Modifier = 1, p3Modifier = .5f;
    private bool victory = false, defeat = false;
    private int virusTurns = 15, virusDelay = 30;

    private float xOffset = 2.0f;
    private float yOffset = 2.0f;
    #endregion

    #region VariablesWithGets&Sets
    private GameObject virusPrefab
    {
        get
        {
            float p3 = cells.Count * p3Modifier;
            float p2 = cells.Count * p2Modifier;
            float p1 = 100.0f - p3 - p2;
            float r = Random.Range(0.0f, 100.0f);
            if (r <= p1)
            {
                return virus1;
            }
            else if (r <= p1 + p2)
            {
                return virus2;
            }
            else
            {
                return virus3;
            }
        }
    }
    public delegate void StrategyEvents();
    StrategyEvents sEvent
    {
        get
        {
            float avg = (easy + medium + hard) * .333f;
            float eFactor = .333f + (avg - easy) * .333f;
            float mFactor = .333f + (avg - medium) * .333f;

            float r = Random.Range(0.0f, 1.0f);
            if (r <= eFactor)
            {
                easy++;
                float e = Random.Range(0.0f, 1.0f);
                if (e <= .25f)
                    return Migrating_White_Cells;
                else if (e <= .5f)
                    return Spread_Immunity_Faster;
                else if (e <= .75f)
                    return Free_Powerzups;
                return Defend_Cells;
            }
            else if (r <= eFactor + mFactor)
            {
                medium++;
                float m = Random.Range(0.0f, 1.0f);
                if (m <= .333f)
                    return Strengthen_Viruses;
                else if (m <= .666f)
                    return Accelerate_Viruses;
                return Mutate_Viruses;
            }
            else
            {
                hard++;
                float h = Random.Range(0.0f, 1.0f);
                if (h <= .5f)
                    return Migrate_Viruses;
                else
                    return Asymptomatic_Carriers;
            }
        }
    }
    #endregion

    #region Start
    // Use this for initialization
    void Start()
    {
        virus1 = Instantiate(virusPrefab1, new Vector3(-10000, -10000, -10000), Quaternion.identity) as GameObject;
        virus2 = Instantiate(virusPrefab2, new Vector3(-10000, -10000, -10000), Quaternion.identity) as GameObject;
        virus3 = Instantiate(virusPrefab3, new Vector3(-10000, -10000, -10000), Quaternion.identity) as GameObject;

        switch (GlobalVariables.difficulty)
        {
            default:
                {
                    p2Modifier = .5f;
                    p3Modifier = .25f;

                    easy = 0;
                    medium = 2;
                    hard = 4;

                    virusDelay = 40;
                    virusTurns = 20;

                    GameObject t = Instantiate(cellPrefab, new Vector3(xOffset * .5f, 0, 0), cellPrefab.transform.rotation, transform) as GameObject;
                    t.GetComponent<StrategyCellScript>().key = new Vector2(0, 0);
                    AddToDictionary(t.GetComponent<StrategyCellScript>());
                    t.name = "Cell0_0";
                    t.GetComponent<StrategyCellScript>().parent = this;
                    t.GetComponent<StrategyCellScript>().reproduction = 5;
                    t.GetComponent<StrategyCellScript>().defense = 5;
                    t.GetComponent<StrategyCellScript>().immunity = 5;
                    t.GetComponent<StrategyCellScript>().enabled = true;
                    if (t.GetComponent<Collider>())
                        t.GetComponent<Collider>().enabled = true;
                    t.transform.GetChild(1).transform.GetComponent<Collider>().enabled = true;
                }
                break;

            case 1:
                {
                    p2Modifier = 1;
                    p3Modifier = .5f;

                    easy = 0;
                    medium = 1;
                    hard = 2;

                    virusDelay = 30;
                    virusTurns = 15;

                    GameObject t = Instantiate(cellPrefab, new Vector3(xOffset * .5f, 0, 0), cellPrefab.transform.rotation, transform) as GameObject;
                    t.GetComponent<StrategyCellScript>().key = new Vector2(0, 0);
                    AddToDictionary(t.GetComponent<StrategyCellScript>());
                    t.name = "Cell0_0";
                    t.GetComponent<StrategyCellScript>().parent = this;
                    t.GetComponent<StrategyCellScript>().reproduction = 1;
                    t.GetComponent<StrategyCellScript>().defense = 1;
                    t.GetComponent<StrategyCellScript>().immunity = 1;
                    t.GetComponent<StrategyCellScript>().enabled = true;
                    t.GetComponent<Collider>().enabled = true;
                    t.transform.GetChild(1).transform.GetComponent<Collider>().enabled = true;
                }
                break;

            case 2:
                {
                    p2Modifier = 2;
                    p3Modifier = 1;

                    easy = medium = hard = 0;

                    virusDelay = 1;
                    virusTurns = 15;

                    GameObject t = Instantiate(cellPrefab, new Vector3(xOffset * .5f, 0, 0), cellPrefab.transform.rotation, transform) as GameObject;
                    t.GetComponent<StrategyCellScript>().key = new Vector2(0, 0);
                    AddToDictionary(t.GetComponent<StrategyCellScript>());
                    t.name = "Cell0_0";
                    t.GetComponent<StrategyCellScript>().parent = this;
                    t.GetComponent<StrategyCellScript>().reproduction = 0;
                    t.GetComponent<StrategyCellScript>().defense = 0;
                    t.GetComponent<StrategyCellScript>().immunity = 0;
                    t.GetComponent<StrategyCellScript>().enabled = true;
                    t.GetComponent<Collider>().enabled = true;
                    t.transform.GetChild(1).transform.GetComponent<Collider>().enabled = true;
                }
                break;
        }

        nextEvent = sEvent;
        inventory = mysteryBox.items;
    }
    #endregion

    #region Selection
    public void SetSelected(Vector2 k)
    {
        if (tiles.ContainsKey(selected))
        {
            tiles[selected].ToggleUI(false);
        }
        else if (selected == mysteryBoxIndex)
        {
            mysteryBox.ToggleUI();
        }
        else if (selected == virusIndex)
        {
            foreach (StrategyVirusScript virus in viruses)
            {
                if (virus.selected)
                {
                    virus.ToggleUI(false);
                }
            }
        }
        else if (selected == victoryIndex && victoryObject)
        {
            if (victory)
                victoryObject.GetComponent<Destroy>().Kill();
            else
                victoryObject.SetActive(false);
        }
        selected = k;
    }

    public void Unselect()
    {
        if (tiles.ContainsKey(selected))
        {
            tiles[selected].ToggleUI(false);
        }
        selected = new Vector2(-100, -100);
        viewingStats = false;
    }
    #endregion

    #region Turns
    public void ActionPreformed()
    {
        StartCoroutine(TurnUpdate());
    }

    IEnumerator TurnUpdate()
    {
        turnNumber++;
        sun.TurnUpdate();

        foreach (StrategyCellScript child in cells.ToList())
        {
            child.TurnUpdate();
        }
        yield return new WaitForEndOfFrame();

        foreach (StrategyMigratingWhiteBloodCell child in whiteCells.ToList())
        {
            child.TurnUpdate();
        }
        yield return new WaitForEndOfFrame();

        foreach (StrategyVirusScript child in viruses.ToList())
        {
            child.TurnUpdate();
        }
        yield return new WaitForEndOfFrame();

        foreach (StrategyCellScript child in cells.ToList())
        {
            child.DelayedTurnUpdate();
        }
        yield return new WaitForEndOfFrame();

        if (turnNumber >= virusDelay && turnNumber % virusTurns == 0)
        {
            SpawnVirus();
        }

        if (turnNumber % eventTurns == 0)
        {
            lastEvent = nextEvent.Method.ToString();
            lastEvent = lastEvent.Remove(0, 5);
            lastEvent = lastEvent.Remove(lastEvent.Length - 2, 2);
            lastEvent = lastEvent.Replace('_', ' ');
            lastEvent = lastEvent.Replace('z', '-');
            nextEvent();
            nextEvent = sEvent;
        }

        cellNum = cells.Count;
        virNum = viruses.Count;

        if (cellNum >= victoryNum && victoryObject)
        {
            victory = true;
            victoryObject.SetActive(true);
            foreach (StrategyCellScript child in cells.ToList())
                immunitySpread += child.immunitySpread;
            victoryObject.GetComponent<TMPro.TextMeshPro>().text = "Congratulations! You've won!" +
                "\nIt took you " + turnNumber + " turns." +
                "\nYou spawned " + cellsSpawned + " cells." +
                "\nYou spread " + (int)immunitySpread + " immunity." +
                "\nYou killed " + virusKills + " viruses." +
                "\nAt this point you can continue in sandbox mode, retry, or return to the main menu.";
            Camera.main.transform.parent.GetComponent<MoveCamera>().SetDestination(new Vector3(victoryObject.transform.position.x, victoryObject.transform.position.y, victoryObject.transform.position.z - 1.5f));
            SetSelected(victoryIndex);
        }
        else if (virNum > cellNum && !defeat && victoryObject)
        {
            defeat = true;
            victoryObject.SetActive(true);
            foreach (StrategyCellScript child in cells.ToList())
                immunitySpread += child.immunitySpread;
            victoryObject.GetComponent<TMPro.TextMeshPro>().text = "At this point there are currently more viruses than cells." +
                "\nYou can continue if you want, but it is very unlikely that you will win." +
                "\nIt took you " + turnNumber + " turns." +
                "\nYou spawned " + cellsSpawned + " cells." +
                "\nYou spread " + (int)immunitySpread + " immunity." +
                "\nYou killed " + virusKills + " viruses.";
            Camera.main.transform.parent.GetComponent<MoveCamera>().SetDestination(new Vector3(victoryObject.transform.position.x, victoryObject.transform.position.y, victoryObject.transform.position.z - 1.5f));
            SetSelected(victoryIndex);
        }
    }
    #endregion

    #region Cells
    public void AddToDictionary(StrategyCellScript cell)
    {
        tiles.Add(cell.key, cell);
    }

    void SpawnCell(Vector2 k, Vector2 p)
    {
        Vector3 spawnLocation = tiles[p].transform.position;
        Vector3 desination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, CalculateY(k), k.y * yOffset);
        GameObject t = Instantiate(transporter, spawnLocation, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = desination;
        GameObject c;
        if (duplicate)
        {
            c = Instantiate(tiles[p].gameObject, spawnLocation, cellPrefab.transform.rotation, t.transform) as GameObject;
            c.GetComponent<StrategyCellScript>().immunitySpread = 0.0f;
            c.GetComponent<StrategyCellScript>().childrenSpawned = 0;
            c.GetComponent<StrategyCellScript>().ToggleUI(false);
            duplicate = false;
        }
        else
        {
            c = Instantiate(cellPrefab, spawnLocation, cellPrefab.transform.rotation, t.transform) as GameObject;
            c.GetComponent<StrategyCellScript>().defense = tiles[p].GetComponent<StrategyCellScript>().defense;
        }
        c.GetComponent<StrategyCellScript>().key = k;
        AddToDictionary(c.GetComponent<StrategyCellScript>());
        c.name = "Cell" + k.x + "_" + k.y;
        c.GetComponent<StrategyCellScript>().parent = this;
        t.GetComponent<StrategyTransporter>().enabled = true;
        c.GetComponent<StrategyCellScript>().enabled = true;
    }

    public void SelectCellSpawn(Vector2 starting)
    {
        Queue<Vector2> que = new Queue<Vector2>();
        HashSet<Vector2> visited = new HashSet<Vector2>(new Vector2Comparer());
        que.Enqueue(starting);
        visited.Add(starting);
        while (true)
        {
            Vector2 check = que.Peek();
            if (check.y % 2 == 0)
            {
                //Top Right (+1, +1)
                check.x += 1;
                check.y += 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Right (+1, 0)
                check = que.Peek();
                check.x += 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Bottom Right (+1, -1)
                check = que.Peek();
                check.x += 1;
                check.y -= 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Bottom Left (0, -1)
                check = que.Peek();
                check.y -= 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Left (-1, 0)
                check = que.Peek();
                check.x -= 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Top Left (0, +1)
                check = que.Peek();
                check.y += 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);
            }
            else
            {
                //Top Right (0, +1)
                check.y += 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Right (+1, 0)
                check = que.Peek();
                check.x += 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Bottom Right (0, -1)
                check = que.Peek();
                check.y -= 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Bottom Left (-1, -1)
                check = que.Peek();
                check.x -= 1;
                check.y -= 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Left (-1, 0)
                check = que.Peek();
                check.x -= 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);

                //Top Left (-1, +1)
                check = que.Peek();
                check.x -= 1;
                check.y += 1;
                if (!tiles.ContainsKey(check))
                {
                    SpawnCell(check, starting);
                    return;
                }
                if (!visited.Contains(check))
                    que.Enqueue(check);
            }

            que.Dequeue();
        }
    }

    float CalculateY(Vector2 k)
    {
        float avg = 0.0f;
        int total = 0;

        Vector2 check = k;
        if (check.y % 2 == 0)
        {
            //Top Right (+1, +1)
            check.x += 1;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Bottom Right (+1, -1)
            check = k;
            check.x += 1;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Bottom Left (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Top Left (0, +1)
            check = k;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }
        }
        else
        {
            //Top Right (0, +1)
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Bottom Right (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Bottom Left (-1, -1)
            check = k;
            check.x -= 1;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }

            //Top Left (-1, +1)
            check = k;
            check.x -= 1;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                avg += tiles[check].transform.position.y;
                total++;
            }
        }

        avg /= total;
        if (avg == float.NaN)
        {
            avg = 0.0f;
        }

        return Mathf.Clamp(Random.Range(-randomRange, randomRange) + avg, -4.0f, 4.0f);
    }

    public void KillCell(Vector2 k)
    {
        StrategyCellScript instance = tiles[k];
        immunitySpread += instance.immunitySpread;
        cells.Remove(instance);
        tiles.Remove(k);
        StartCoroutine(instance.Die());
    }

    public float SpreadImmunity(Vector2 k, float imm)
    {
        immunitySpread = 0;
        float i;
        Vector2 check = k;
        if (check.y % 2 == 0)
        {
            //Top Right (+1, +1)
            check.x += 1;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Bottom Right (+1, -1)
            check = k;
            check.x += 1;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Bottom Left (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Top Left (0, +1)
            check = k;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }
        }
        else
        {
            //Top Right (0, +1)
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Bottom Right (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Bottom Left (-1, -1)
            check = k;
            check.x -= 1;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }

            //Top Left (-1, +1)
            check = k;
            check.x -= 1;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                i = imm;
                if (tiles[check].hosted)
                    i *= 2.0f;
                tiles[check].AddImmunity(i);
                immunitySpread += i;
                GameObject p = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform) as GameObject;
                p.GetComponent<ImmunityParticles>().target = tiles[check].transform;
                p.GetComponent<ImmunityParticles>().immunity = i;
                p.GetComponent<ImmunityParticles>().startSpeed = tiles[k].startSpeed;
                p.GetComponent<ImmunityParticles>().enabled = true;
            }
        }

        return immunitySpread;
    }
    #endregion

    #region Viruses
    public void SpawnVirus()
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0.65f, 1f);
        float distance = 100.0f;
        Vector3 position = direction * distance;
        GameObject v = Instantiate(virusPrefab, position, Quaternion.identity, transform) as GameObject;
        v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
        v.GetComponent<StrategyVirusScript>().parent = this;
        v.GetComponent<Collider>().enabled = true;
        v.GetComponent<StrategyVirusScript>().enabled = true;
    }

    //Attempts to spawn a virus on an adjacent cell
    //If one of them isn't open it targets a random cell or goes on standby
    public void SpawnVirusSingleAdjacent(Vector2 k, Vector3 p)
    {
        Vector2 check = k;
        if (check.y % 2 == 0)
        {
            //Top Right (+1, +1)
            check.x += 1;
            check.y += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Bottom Right (+1, -1)
            check = k;
            check.x += 1;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Bottom Left (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Top Left (0, +1)
            check = k;
            check.y += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }
        }
        else
        {
            //Top Right (0, +1)
            check.y += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Bottom Right (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Bottom Left (-1, -1)
            check = k;
            check.x -= 1;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }

            //Top Left (-1, +1)
            check = k;
            check.x -= 1;
            check.y += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
                Vector3 move = (tiles[check].transform.position - p) * .5f;
                t.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
                tiles[check].targeted = true;
                GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                v.GetComponent<StrategyVirusScript>().parent = this;
                t.GetComponent<StrategyTransporter>().enabled = true;

                return;
            }
        }

        GameObject tra = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        tra.GetComponent<StrategyTransporter>().destination = new Vector3(p.x + Random.Range(-xOffset * .5f, xOffset * .5f), p.y + 2.5f, p.y + Random.Range(-yOffset * .5f, yOffset * .5f));
        GameObject vir = Instantiate(virusPrefab, p, Quaternion.identity, tra.transform) as GameObject;
        vir.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(vir);
        vir.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
        vir.GetComponent<StrategyVirusScript>().parent = this;
        tra.GetComponent<StrategyTransporter>().enabled = true;
    }

    //Attempts to spawn viruses on all adjacent cells
    //If one of them isn't open it targets a random cell or goes on standby
    public void SpawnVirusAllAdjacent(Vector2 k, Vector3 p)
    {
        Vector2 check = k;
        if (check.y % 2 == 0)
        {
            //Top Right (+1, +1)
            check.x += 1;
            check.y += 1;
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Bottom Right (+1, -1)
            check = k;
            check.x += 1;
            check.y -= 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Bottom Left (0, -1)
            check = k;
            check.y -= 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Top Left (0, +1)
            check = k;
            check.y += 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
        }
        else
        {
            //Top Right (0, +1)
            check.y += 1;
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Bottom Right (0, -1)
            check = k;
            check.y -= 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Bottom Left (-1, -1)
            check = k;
            check.x -= 1;
            check.y -= 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }

            //Top Left (-1, +1)
            check = k;
            check.x -= 1;
            check.y += 1;
            t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f);
            v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().parent = this;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                tiles[check].targeted = true;
                v.GetComponent<StrategyVirusScript>().target = tiles[check];
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
            else
            {
                v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
                v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
                t.GetComponent<StrategyTransporter>().enabled = true;

            }
        }
    }

    public StrategyCellScript FindVirusNewTarget(GameObject vir)
    {
        for (int i = 0; i < 10; i++)
        {
            if (cells.Count > 0)
            {
                StrategyCellScript temp = cells[Random.Range(0, cells.Count)];

                if (!temp.targeted)
                {
                    vir.GetComponent<StrategyVirusScript>().standby = false;
                    temp.targeted = true;
                    return temp;
                }
            }
            else
                break;
        }

        vir.GetComponent<StrategyVirusScript>().standby = true;
        return null;
    }

    public Vector3 RandomPositionAboveHex()
    {
        return new Vector3(Random.Range(tiles.Count * -1.0f, tiles.Count), 10, Random.Range(tiles.Count * -1.0f, tiles.Count));
    }
    #endregion

    #region WhiteCells
    public StrategyVirusScript FindWhiteCellNewTarget()
    {
        for (int i = 0; i < viruses.Count; i++)
        {
            if (!viruses[i].targeted)
            {
                viruses[i].targeted = true;
                return viruses[i];
            }
        }
        return null;
    }
    #endregion

    #region Events
    void Migrating_White_Cells()
    {
        Vector3 ogDirection = Random.onUnitSphere;
        ogDirection.y = Mathf.Clamp(ogDirection.y, 0.65f, 1f);
        int migTotal = turnNumber / 10 + 1;
        for (int i = 0; i < migTotal; i++)
        {
            float distance = Random.Range(98.0f, 102.0f);
            Vector3 direction = ogDirection + new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f));
            Vector3 position = direction * distance;
            GameObject w = Instantiate(whiteCellPrefab, position, Quaternion.identity, transform) as GameObject;
            w.GetComponent<StrategyMigratingWhiteBloodCell>().target = FindWhiteCellNewTarget();
            w.GetComponent<StrategyMigratingWhiteBloodCell>().parent = this;
            w.GetComponent<Collider>().enabled = true;
            w.GetComponent<StrategyMigratingWhiteBloodCell>().enabled = true;
        }
    }

    void Spread_Immunity_Faster()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].UseI2();
        }
    }

    void Free_Powerzups()
    {
        mysteryBox.GiveAll();
    }

    void Defend_Cells()
    {
        float highestDefense = float.MinValue;
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].defense > highestDefense)
                highestDefense = cells[i].defense;
        }

        highestDefense += 5.0f;

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].defense = highestDefense;
        }
    }

    void Strengthen_Viruses()
    {
        virus1.GetComponent<StrategyVirusScript>().health *= 1.5f;
        virus1.GetComponent<StrategyVirusScript>().attackValue *= 1.5f;
        virus2.GetComponent<StrategyVirusScript>().health *= 1.5f;
        virus2.GetComponent<StrategyVirusScript>().attackValue *= 1.5f;
        virus3.GetComponent<StrategyVirusScript>().health *= 1.5f;
        virus3.GetComponent<StrategyVirusScript>().attackValue *= 1.5f;
    }

    void Accelerate_Viruses()
    {
        virus1.GetComponent<StrategyVirusScript>().turnSpeed *= 1.5f;
        virus2.GetComponent<StrategyVirusScript>().turnSpeed *= 1.5f;
        virus3.GetComponent<StrategyVirusScript>().turnSpeed *= 1.5f;
    }

    void Mutate_Viruses()
    {
        p2Modifier *= 1.5f;
        p3Modifier *= 1.5f;
    }

    void Migrate_Viruses()
    {
        Vector3 ogDirection = Random.onUnitSphere;
        ogDirection.y = Mathf.Clamp(ogDirection.y, 0.65f, 1f);
        int migTotal = turnNumber / 50 + 1;
        for (int i = 0; i < migTotal; i++)
        {
            float distance = Random.Range(98.0f, 102.0f);
            Vector3 direction = ogDirection + new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f));
            Vector3 position = direction * distance;
            GameObject v = Instantiate(virusPrefab, position, Quaternion.identity, transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().parent = this;
            v.GetComponent<Collider>().enabled = true;

        }
    }

    void Asymptomatic_Carriers()
    {
        int asyTotal = (int)(cellNum * .05f + 1);
        int index = 0;
        for (int i = 0; i < asyTotal; i++)
        {
            while (index < cells.Count)
            {
                if (!cells[index].targeted)
                    break;
                index++;
            }
            if (index >= cells.Count)
                break;

            GameObject v = Instantiate(virusPrefab, cells[index].transform.position, Quaternion.identity, transform) as GameObject;
            cells[index].targeted = true;
            cells[index].virus = v;
            v.GetComponent<StrategyVirusScript>().target = cells[index];
            v.GetComponent<StrategyVirusScript>().percentTraveled = 100.0f;
            v.GetComponent<StrategyVirusScript>().parent = this;

            index++;
        }
    }
    #endregion
}
