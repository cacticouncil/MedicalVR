using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class StrategyCellManagerScript : MonoBehaviour
{
    public Dictionary<Vector2, GameObject> tiles = new Dictionary<Vector2, GameObject>(new Vector2Comparer());
    public GameObject cellPrefab;
    public GameObject virusPrefab1;
    public GameObject virusPrefab2;
    public GameObject virusPrefab3;
    public Text screenUI;
    public int actionsLeft = 4;
    public int turnNumber = 0;

    private GameObject virusPrefab;
    private float xOffset = 1.0f;
    private float yOffset = 1.0f;

    // Use this for initialization
    void Start()
    {
        virusPrefab = virusPrefab1;
        GameObject t = Instantiate(cellPrefab, new Vector3(.5f, 0, 0), Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyCellScript>().key = new Vector2(0, 0);
        AddToDictionary(t);
        t.name = "Cell0_0";
        t.GetComponent<StrategyCellScript>().enabled = true;
        t.GetComponent<StrategyCellScript>().ToggleUI(true);
    }

    public void ActionPreformed()
    {
        actionsLeft--;
        if (actionsLeft == 0)
        {
            actionsLeft = 4;
            TurnUpdate();
        }

        screenUI.text = "Actions Left: " + actionsLeft + "\nTurn Number: " + turnNumber;
    }

    public void TurnUpdate()
    {
        Debug.Log("Turn Updated");
        turnNumber++;
        int children = gameObject.GetComponentsInChildren<StrategyCellScript>().Length;

        if (children > 10 && virusPrefab == virusPrefab1)
        {
            Debug.Log("Viruses Have Mutated");
            virusPrefab = virusPrefab2;
        }

        if (children > 30 && virusPrefab == virusPrefab2)
        {
            Debug.Log("Viruses Have Mutated");
            virusPrefab = virusPrefab3;
        }

        foreach (StrategyCellScript child in gameObject.GetComponentsInChildren<StrategyCellScript>())
        {
            child.TurnUpdate();
        }

        foreach (StrategyVirusScript child in gameObject.GetComponentsInChildren<StrategyVirusScript>())
        {
            child.TurnUpdate();
        }

        foreach (StrategyCellScript child in gameObject.GetComponentsInChildren<StrategyCellScript>())
        {
            child.DelayedTurnUpdate();
        }

        if (turnNumber % 4 == 0)
        {
            SpawnVirus();
        }
    }

    public void AddToDictionary(GameObject cell)
    {
        tiles.Add(cell.GetComponent<StrategyCellScript>().key, cell);
    }

    public void SelectCellSpawn(Vector2 starting)
    {
        Queue<Vector2> que = new Queue<Vector2>();
        que.Enqueue(starting);
        while (true)
        {
            //Top Right (0, +1)
            Vector2 check = que.Peek();
            check.y += 1;
            if (!tiles.ContainsKey(check))
            {
                SpawnCell(check);
                return;
            }
            que.Enqueue(check);

            //Right (+1, 0)
            check = que.Peek();
            check.x += 1;
            if (!tiles.ContainsKey(check))
            {
                SpawnCell(check);
                return;
            }
            que.Enqueue(check);

            //Bottom Right (0, -1)
            check = que.Peek();
            check.y -= 1;
            if (!tiles.ContainsKey(check))
            {
                SpawnCell(check);
                return;
            }
            que.Enqueue(check);

            //Bottom Left (-1, -1)
            check = que.Peek();
            check.x -= 1;
            check.y -= 1;
            if (!tiles.ContainsKey(check))
            {
                SpawnCell(check);
                return;
            }
            que.Enqueue(check);

            //Left (-1, 0)
            check = que.Peek();
            check.x -= 1;
            if (!tiles.ContainsKey(check))
            {
                SpawnCell(check);
                return;
            }
            que.Enqueue(check);

            //Top Left (-1, +1)
            check = que.Peek();
            check.x -= 1;
            check.y += 1;
            if (!tiles.ContainsKey(check))
            {
                SpawnCell(check);
                return;
            }
            que.Enqueue(check);

            que.Dequeue();
        }
    }

    void SpawnCell(Vector2 k)
    {
        GameObject t = Instantiate(cellPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 0, k.y * yOffset), Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyCellScript>().key = k;
        AddToDictionary(t);
        t.name = "Cell" + k.x + "_" + k.y;
        t.GetComponent<StrategyCellScript>().enabled = true;
    }

    public void KillCell(Vector2 k)
    {
        GameObject instance = tiles[k];
        tiles.Remove(k);
        Destroy(instance);
    }

    public void SpreadImmunity(Vector2 starting)
    {
        //Top Right (0, +1)
        Vector2 check = starting;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<StrategyCellScript>().AddImmunity();
        }

        //Right (+1, 0)
        check = starting;
        check.x += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<StrategyCellScript>().AddImmunity();
        }

        //Bottom Right (0, -1)
        check = starting;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<StrategyCellScript>().AddImmunity();
        }

        //Bottom Left (-1, -1)
        check = starting;
        check.x -= 1;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<StrategyCellScript>().AddImmunity();
        }

        //Left (-1, 0)
        check = starting;
        check.x -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<StrategyCellScript>().AddImmunity();
        }

        //Top Left (-1, +1)
        check = starting;
        check.x -= 1;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<StrategyCellScript>().AddImmunity();
        }
    }

    public void SpawnVirus()
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0.65f, 1f);
        float distance = 100.0f;
        Vector3 position = direction * distance;
        GameObject v = Instantiate(virusPrefab, position, Quaternion.identity, transform) as GameObject;
        v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
        v.GetComponent<StrategyVirusScript>().enabled = true;
    }

    //Attempts to spawn a virus on an adjacent cell
    //If one of them isn't open it targets a random cell or goes on standby
    public void SpawnVirusSingleAdjacent(Vector2 k)
    {
        //Top Right
        Vector2 check = k;
        check.y += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Right (+1, 0)
        check = k;
        check.x += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset : k.x * xOffset + xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Bottom Right (0, -1)
        check = k;
        check.y -= 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Bottom Left (-1, -1)
        check = k;
        check.x -= 1;
        check.y -= 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Left (-1, 0)
        check = k;
        check.x -= 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Top Left (-1, +1)
        check = k;
        check.x -= 1;
        check.y += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        GameObject vir = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 2, k.y * yOffset), Quaternion.identity, transform) as GameObject;
        vir.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(vir);
        vir.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
        vir.GetComponent<StrategyVirusScript>().enabled = true;
    }

    //Attempts to spawn viruses on all adjacent cells
    //If one of them isn't open it targets a random cell or goes on standby
    public void SpawnVirusAllAdjacent(Vector2 k)
    {
        //Top Right
        Vector2 check = k;
        check.y += 1;
        GameObject v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Right (+1, 0)
        check = k;
        check.x += 1;
        v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset : k.x * xOffset + xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Bottom Right (0, -1)
        check = k;
        check.y -= 1;
        v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Bottom Left (-1, -1)
        check = k;
        check.x -= 1;
        check.y -= 1;
        v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Left (-1, 0)
        check = k;
        check.x -= 1;
        v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Top Left (-1, +1)
        check = k;
        check.x -= 1;
        check.y += 1;
        v = Instantiate(virusPrefab, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted && tiles[check].GetComponent<StrategyCellScript>().immunity < 10)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
    }

    public GameObject FindVirusNewTarget(GameObject vir)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = tiles.Values.ElementAt(Random.Range(0, tiles.Values.Count));

            if (!temp.GetComponent<StrategyCellScript>().targeted && temp.GetComponent<StrategyCellScript>().immunity < 10)
            {
                vir.GetComponent<StrategyVirusScript>().standby = false;
                temp.GetComponent<StrategyCellScript>().targeted = true;
                return temp;
            }
        }

        vir.GetComponent<StrategyVirusScript>().standby = true;
        return null;
    }

    public Vector3 RandomPositionAboveHex()
    {
        return new Vector3(Random.Range(tiles.Count * .17f * -1.0f, tiles.Count * .17f), 10, Random.Range(tiles.Count * .17f * -1.0f, tiles.Count * .17f));
    }
}

class Vector2Comparer : IEqualityComparer<Vector2>
{
    public bool Equals(Vector2 x, Vector2 y)
    {
        if (x.x == y.x && x.y == y.y)
            return true;
        return false;
    }

    public int GetHashCode(Vector2 x)
    {
        return (int)x.x + (int)x.y * 1000000;
    }
}
