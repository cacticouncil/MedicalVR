using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class StrategyCellManagerScript : MonoBehaviour
{
    #region Variables
    //Static
    private static StrategyCellManagerScript localInstance;
    public static int difficulty = 0;

    //In Inspector
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
    public GameObject eventSystem;
    public StrategySoundHolder soundSource;
    [Space(2)]

    [Header("Instances")]
    public StrategyBox mysteryBox;
    public SimulateSun sun;
    public GameObject victoryObject;
    public StrategyTutorialReproduction str;
    public StrategyTutorialDefense std;
    public StrategyTutorialImmunity sti;
    [Space(2)]

    [Header("Scoreboard")]
    public GameObject scoreBoard;
    public TMPro.TextMeshPro username;
    public UnityEngine.UI.Image profilePicture;
    public static int finalScore;

    //Not in Inspector
    public StrategyEvents nextEvent;
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

    //Private
    private Vector2 mysteryBoxIndex = new Vector2(500, 500), victoryIndex = new Vector2(-500, -500), virusIndex = new Vector2(-500, 500);
    private GameObject virus1, virus2, virus3;
    private int easy = 0, medium = 1, hard = 2;
    private int victoryNum = 50, maxParticleSystems = 200;
    private float randomRange = .5f;
    private float p2Modifier = 1, p3Modifier = .5f;
    private bool victory = false, defeat = false;
    private float virusReset = 50, virusTurn = 50, virusTurnMod = 2.5f;
    private float xOffset = 2.0f;
    private float yOffset = 2.0f;
    #endregion

    #region VariablesWithGets&Sets
    public static StrategyCellManagerScript instance { get { return localInstance; } }
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
                    return Protein_Chaos;
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

    #region Initialization
    private void Awake()
    {
        if (localInstance != null && localInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            localInstance = this;
        }
    }

    void Start()
    {
        virus1 = Instantiate(virusPrefab1, new Vector3(-10000, -10000, -10000), Quaternion.identity) as GameObject;
        virus2 = Instantiate(virusPrefab2, new Vector3(-10000, -10000, -10000), Quaternion.identity) as GameObject;
        virus3 = Instantiate(virusPrefab3, new Vector3(-10000, -10000, -10000), Quaternion.identity) as GameObject;

        switch (difficulty)
        {
            //Easy
            default:
                {
                    //Base virus stats
                    StrategyVirusScript v = virus1.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .09f;
                    v.attackValue = .5f;
                    v.immunityToKill = 15.0f;

                    //Host virus stats
                    v = virus2.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .09f;
                    v.attackValue = 2.0f;
                    v.immunityToKill = 30.0f;

                    //Explosion virus stats
                    v = virus3.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .09f;
                    v.attackValue = 1.0f;
                    v.immunityToKill = 20.0f;

                    //Chances of spawning stronger viruses
                    p2Modifier = .5f;
                    p3Modifier = .25f;

                    //Chances of events happening, inverted
                    easy = 0;
                    medium = 2;
                    hard = 4;

                    //Virus spawn rate
                    virusTurnMod = 2.5f;

                    StrategyCellScript t = Instantiate(cellPrefab, new Vector3(xOffset * .5f, 0, 0), cellPrefab.transform.rotation, transform).GetComponent<StrategyCellScript>();
                    t.key = new Vector2(0, 0);
                    AddToDictionary(t);
                    t.name = "Cell0_0";
                    t.reproduction = 5;
                    t.defense = 5;
                    t.immunity = 5;
                    t.enabled = true;
                    t.transform.GetChild(1).transform.GetComponent<Collider>().enabled = true;
                }
                break;

            //Normal
            case 1:
                {
                    //Base virus stats
                    StrategyVirusScript v = virus1.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .1f;
                    v.attackValue = 1.0f;
                    v.immunityToKill = 20.0f;

                    //Host virus stats
                    v = virus2.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .1f;
                    v.attackValue = 4.0f;
                    v.immunityToKill = 30.0f;

                    //Explosion virus stats
                    v = virus3.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .1f;
                    v.attackValue = 1.5f;
                    v.immunityToKill = 25.0f;

                    //Chances of spawning stronger viruses
                    p2Modifier = 1;
                    p3Modifier = .5f;

                    //Chances of events happening, inverted
                    easy = 0;
                    medium = 1;
                    hard = 2;

                    //Virus spawn rate
                    virusTurnMod = 3;

                    StrategyCellScript t = Instantiate(cellPrefab, new Vector3(xOffset * .5f, 0, 0), cellPrefab.transform.rotation, transform).GetComponent<StrategyCellScript>();
                    t.key = new Vector2(0, 0);
                    AddToDictionary(t);
                    t.name = "Cell0_0";
                    t.reproduction = 5;
                    t.defense = 5;
                    t.immunity = 5;
                    t.enabled = true;
                    t.transform.GetChild(1).transform.GetComponent<Collider>().enabled = true;
                }
                break;

            //Hard
            case 2:
                {
                    //Base virus stats
                    StrategyVirusScript v = virus1.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .11f;
                    v.attackValue = 2.5f;
                    v.immunityToKill = 20.0f;

                    //Host virus stats
                    v = virus2.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .11f;
                    v.attackValue = 5.0f;
                    v.immunityToKill = 30.0f;

                    //Explosion virus stats
                    v = virus3.GetComponent<StrategyVirusScript>();
                    v.turnSpeed = .11f;
                    v.attackValue = 2.0f;
                    v.immunityToKill = 25.0f;

                    //Chances of spawning stronger viruses
                    p2Modifier = 2;
                    p3Modifier = 1;

                    //Chances of events happening, inverted
                    easy = medium = hard = 0;

                    //Virus spawn rate
                    virusTurnMod = 3.5f;

                    StrategyCellScript t = Instantiate(cellPrefab, new Vector3(xOffset * .5f, 0, 0), cellPrefab.transform.rotation, transform).GetComponent<StrategyCellScript>();
                    t.key = new Vector2(0, 0);
                    AddToDictionary(t);
                    t.name = "Cell0_0";
                    t.reproduction = 5;
                    t.defense = 5;
                    t.immunity = 5;
                    t.enabled = true;
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
                Destroy(victoryObject.gameObject);
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
        soundSource.PlayRandomSound();
        StartCoroutine(TurnUpdate());
    }

    private void EnableCollider(Collider c)
    {
        c.enabled = true;
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

        virusTurn -= virusTurnMod;
        while (virusTurn <= 0)
        {
            virusTurn += virusReset;
            SpawnVirusSky();
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
            switch (difficulty)
            {
                default:
                    break;
                case 1:
                    float m = Random.Range(0.0f, 1.0f);
                    if (m <= .333f)
                        Strengthen_Viruses();
                    else if (m <= .666f)
                        Accelerate_Viruses();
                    Mutate_Viruses();
                    break;
                case 2:
                    Strengthen_Viruses();
                    Accelerate_Viruses();
                    Mutate_Viruses();
                    break;
            }
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

            if (difficulty == 2)
                BannerScript.UnlockTrophy("Cell");

            ShowScore();
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
            ShowScore();
        }
    }

    void ShowScore()
    {
        int score = (int)((cellsSpawned * .01f + immunitySpread * .00001f + virusKills * .1f) / turnNumber * ((difficulty + 1.0f) * 2.0f));
        if (defeat && !victory)
            score = Mathf.Max(100, score);
        if (score > finalScore)
            finalScore = score;
        if (finalScore > PlayerPrefs.GetInt("StrategyScore"))
            PlayerPrefs.SetInt("StrategyScore", score);
        else
            finalScore = PlayerPrefs.GetInt("StrategyScore");
        scoreBoard.SetActive(true);
        username.text = FacebookManager.Instance.ProfileName + ": " + score.ToString();
        if (FacebookManager.Instance.ProfilePic != null)
            profilePicture.sprite = FacebookManager.Instance.ProfilePic;
    }
    #endregion

    #region Cells
    public void AddToDictionary(StrategyCellScript cell)
    {
        tiles.Add(cell.key, cell);
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

    public Vector3 CalculatePosition(Vector2 k)
    {
        return new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, CalculateY(k), k.y * yOffset);
    }

    void SpawnCell(Vector2 k, Vector2 p)
    {
        Vector3 spawnLocation = tiles[p].transform.position;
        Vector3 desination = CalculatePosition(k);
        StrategyTransporter t = Instantiate(transporter, spawnLocation, Quaternion.identity, transform).GetComponent<StrategyTransporter>();
        t.destination = desination;
        StrategyCellScript c;
        if (duplicate)
        {
            c = Instantiate(tiles[p].gameObject, spawnLocation, cellPrefab.transform.rotation, t.transform).GetComponent<StrategyCellScript>();
            c.immunitySpread = 0.0f;
            c.childrenSpawned = 0;
            c.ToggleUI(false);
            duplicate = false;
        }
        else
        {
            c = Instantiate(cellPrefab, spawnLocation, cellPrefab.transform.rotation, t.transform).GetComponent<StrategyCellScript>();
            c.defense = tiles[p].defense;
        }
        c.key = k;
        AddToDictionary(c);
        c.name = "Cell" + k.x + "_" + k.y;
        t.enabled = true;
        c.enabled = true;
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

    public void KillCell(Vector2 k)
    {
        StrategyCellScript c = tiles[k];
        immunitySpread += c.immunitySpread;
        cells.Remove(c);
        tiles.Remove(k);
        StartCoroutine(c.Die());
    }

    private void SpawnParticles(float i, Vector2 k, Vector2 check)
    {
        if (tiles[check].hosted)
            i *= 2.0f;
        tiles[check].AddImmunity(i);
        immunitySpread += i;
        ImmunityParticles t = Instantiate(particleToTarget, tiles[k].transform.position, Quaternion.LookRotation(tiles[check].transform.position - tiles[k].transform.position), transform).GetComponent<ImmunityParticles>();
        if (ImmunityParticles.list.Count > maxParticleSystems)
        {
            ParticleSystem p = null;
            while (p == null && ImmunityParticles.list.Count > maxParticleSystems)
            {
                p = ImmunityParticles.list[0].p;
                ImmunityParticles.list.RemoveAt(0);
            }
            if (p)
                p.Clear();
        }
        ImmunityParticles.list.Add(t);
        t.target = tiles[check].transform;
        t.immunity = i;
        t.startSpeed = tiles[k].startSpeed;
        t.enabled = true;
    }

    public float SpreadImmunity(Vector2 k, float imm)
    {
        immunitySpread = 0;
        Vector2 check = k;
        if (check.y % 2 == 0)
        {
            //Top Right (+1, +1)
            check.x += 1;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Bottom Right (+1, -1)
            check = k;
            check.x += 1;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Bottom Left (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Top Left (0, +1)
            check = k;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }
        }
        else
        {
            //Top Right (0, +1)
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Bottom Right (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Bottom Left (-1, -1)
            check = k;
            check.x -= 1;
            check.y -= 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }

            //Top Left (-1, +1)
            check = k;
            check.x -= 1;
            check.y += 1;
            if (tiles.ContainsKey(check))
            {
                SpawnParticles(imm, k, check);
            }
        }

        return immunitySpread;
    }
    #endregion

    #region Viruses
    public void SpawnVirusSky()
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0.65f, 1f);
        float distance = 100.0f;
        Vector3 position = direction * distance;
        StrategyVirusScript v = Instantiate(virusPrefab, position, Quaternion.identity, transform).GetComponent<StrategyVirusScript>();
        viruses.Add(v);
        v.GetComponent<Collider>().isTrigger = false;
        v.target = FindVirusNewTarget(v);
        v.enabled = true;
    }

    private void SpawnVirusWithTarget(Vector2 check, Vector3 p)
    {
        StrategyTransporter t = Instantiate(transporter, p, Quaternion.identity, transform).GetComponent<StrategyTransporter>();
        Vector3 move = (tiles[check].transform.position - p) * .5f;
        t.destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
        tiles[check].targeted = true;
        StrategyVirusScript v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform).GetComponent<StrategyVirusScript>();
        viruses.Add(v);
        v.target = tiles[check];
        v.percentTraveled = 1.0f - v.turnSpeed;
        t.enabled = true;
        v.enabled = true;
    }

    private void SpawnVirusAroundCell(Vector2 check, Vector3 p)
    {
        StrategyTransporter t = Instantiate(transporter, p, Quaternion.identity, transform).GetComponent<StrategyTransporter>();
        Vector3 move = (CalculatePosition(check) - p) * .5f;
        t.destination = new Vector3(p.x + move.x, p.y + move.y, p.z + move.z);
        StrategyVirusScript v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform).GetComponent<StrategyVirusScript>();
        viruses.Add(v);

        if (tiles.ContainsKey(check) && !tiles[check].targeted)
        {
            tiles[check].targeted = true;
            v.target = tiles[check];
            v.percentTraveled = 1.0f - v.turnSpeed;
            t.enabled = true;
        }
        else
        {
            v.target = FindVirusNewTarget(v);
            v.percentTraveled = 1.0f - v.turnSpeed;
            t.enabled = true;
        }
        v.enabled = true;
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
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Bottom Right (+1, -1)
            check = k;
            check.x += 1;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Bottom Left (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Top Left (0, +1)
            check = k;
            check.y += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }
        }
        else
        {
            //Top Right (0, +1)
            check.y += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Right (+1, 0)
            check = k;
            check.x += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Bottom Right (0, -1)
            check = k;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Bottom Left (-1, -1)
            check = k;
            check.x -= 1;
            check.y -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }

            //Top Left (-1, +1)
            check = k;
            check.x -= 1;
            check.y += 1;
            if (tiles.ContainsKey(check) && !tiles[check].targeted)
            {
                SpawnVirusWithTarget(check, p);
                return;
            }
        }

        StrategyTransporter t = Instantiate(transporter, p, Quaternion.identity, transform).GetComponent<StrategyTransporter>();
        t.destination = new Vector3(p.x + Random.Range(-xOffset * .5f, xOffset * .5f), p.y + 2.5f, p.z + Random.Range(-yOffset * .5f, yOffset * .5f));
        StrategyVirusScript v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform).GetComponent<StrategyVirusScript>();
        viruses.Add(v);
        v.target = FindVirusNewTarget(v);
        v.percentTraveled = 1.0f - v.turnSpeed;
        t.enabled = true;
        v.enabled = true;
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
            SpawnVirusAroundCell(check, p);

            //Right (+1, 0)
            check = k;
            check.x += 1;
            SpawnVirusAroundCell(check, p);

            //Bottom Right (+1, -1)
            check = k;
            check.x += 1;
            check.y -= 1;
            SpawnVirusAroundCell(check, p);

            //Bottom Left (0, -1)
            check = k;
            check.y -= 1;
            SpawnVirusAroundCell(check, p);

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            SpawnVirusAroundCell(check, p);

            //Top Left (0, +1)
            check = k;
            check.y += 1;
            SpawnVirusAroundCell(check, p);
        }
        else
        {
            //Top Right (0, +1)
            check.y += 1;
            SpawnVirusAroundCell(check, p);

            //Right (+1, 0)
            check = k;
            check.x += 1;
            SpawnVirusAroundCell(check, p);

            //Bottom Right (0, -1)
            check = k;
            check.y -= 1;
            SpawnVirusAroundCell(check, p);

            //Bottom Left (-1, -1)
            check = k;
            check.x -= 1;
            check.y -= 1;
            SpawnVirusAroundCell(check, p);

            //Left (-1, 0)
            check = k;
            check.x -= 1;
            SpawnVirusAroundCell(check, p);

            //Top Left (-1, +1)
            check = k;
            check.x -= 1;
            check.y += 1;
            SpawnVirusAroundCell(check, p);
        }
    }

    public StrategyCellScript FindVirusNewTarget(StrategyVirusScript vir)
    {
        for (int i = 0; i < 10; i++)
        {
            if (cells.Count > 0)
            {
                StrategyCellScript temp = cells[Random.Range(0, cells.Count)];
                if (!temp.targeted)
                {
                    vir.standby = false;
                    temp.targeted = true;
                    return temp;
                }
            }
            else
                break;
        }
        vir.standby = true;
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
        int migTotal = turnNumber / 100 + 1;
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

    void Protein_Chaos()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            Proteins prev = cells[i].protein;
            while (cells[i].protein == prev)
            {
                cells[i].protein = (Proteins)Random.Range(1, 7);
            }
            cells[i].p.text = "Protein: " + cells[i].protein.ToString();
            if (cells[i].key == selected)
            {
                cells[i].RefreshUI();
            }
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
        virus1.GetComponent<StrategyVirusScript>().immunityToKill *= 1.5f;
        virus1.GetComponent<StrategyVirusScript>().attackValue *= 1.5f;
        virus2.GetComponent<StrategyVirusScript>().immunityToKill *= 1.5f;
        virus2.GetComponent<StrategyVirusScript>().attackValue *= 1.5f;
        virus3.GetComponent<StrategyVirusScript>().immunityToKill *= 1.5f;
        virus3.GetComponent<StrategyVirusScript>().attackValue *= 1.5f;
    }

    void Accelerate_Viruses()
    {
        virus1.GetComponent<StrategyVirusScript>().turnSpeed *= 1.5f;
        virus2.GetComponent<StrategyVirusScript>().turnSpeed *= 1.5f;
        virus3.GetComponent<StrategyVirusScript>().turnSpeed *= 1.5f;
        virusTurnMod *= 1.5f;
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
            StrategyVirusScript v = Instantiate(virusPrefab, position, Quaternion.identity, transform).GetComponent<StrategyVirusScript>();
            viruses.Add(v);
            v.target = FindVirusNewTarget(v);
            v.GetComponent<Collider>().isTrigger = false;
            v.enabled = true;
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

            StrategyTransporter t = Instantiate(transporter, cells[index].transform.position, Quaternion.identity, transform).GetComponent<StrategyTransporter>();
            t.destination = cells[index].transform.position;
            StrategyVirusScript v = Instantiate(virusPrefab, t.transform.position, Quaternion.identity, t.transform).GetComponent<StrategyVirusScript>();
            viruses.Add(v);
            cells[index].targeted = true;
            cells[index].virus = v;
            v.target = cells[index];
            v.percentTraveled = 101.0f;
            t.enabled = true;
            v.enabled = true;
            index++;
        }
    }
    #endregion
}
