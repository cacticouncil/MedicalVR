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
    public GameObject transporter;
    public SimulateSun sun;
    public TextMesh screenUI;
    public int actionsLeft = 4;
    public int turnNumber = 0;
    public int cellNum = 1;
    public int virNum = 0;
    public Vector2 selected = new Vector2(0.0f, 0.0f);
    public bool viewingStats = false;
    public float randomRange = .5f;

    private GameObject virusPrefab;
    //private float xOffset = 1.0f;
    //private float yOffset = 1.0f;
    public float xOffset = 2.0f;
    public float yOffset = 2.0f;

    // Use this for initialization
    void Start()
    {
        virusPrefab = virusPrefab1;
        GameObject t = Instantiate(cellPrefab, new Vector3(xOffset * .5f, 0, 0), Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyCellScript>().key = new Vector2(0, 0);
        AddToDictionary(t);
        t.name = "Cell0_0";
        t.GetComponent<StrategyCellScript>().enabled = true;
        t.GetComponent<BoxCollider>().enabled = true;
    }

    public void SetSelected(Vector2 k)
    {
        tiles[selected].GetComponent<StrategyCellScript>().ToggleUI(false);
        selected = k;
    }

    public void ActionPreformed()
    {
        actionsLeft--;
        if (actionsLeft == 0)
        {
            actionsLeft = 4;
            TurnUpdate();
        }

        screenUI.text = "Actions Left: " + actionsLeft + "\nTurn Number: " + turnNumber + "\nCells Alive: " + cellNum + "\nViruses Alive: " + virNum;
    }

    public void TurnUpdate()
    {
        Debug.Log("Turn Updating");
        turnNumber++;

        foreach (StrategyCellScript child in gameObject.GetComponentsInChildren<StrategyCellScript>())
        {
            child.TurnUpdate();
        }

        Debug.Log("Cells Updated");

        foreach (StrategyVirusScript child in gameObject.GetComponentsInChildren<StrategyVirusScript>())
        {
            child.TurnUpdate();
        }

        Debug.Log("Viruses Updated");

        foreach (StrategyCellScript child in gameObject.GetComponentsInChildren<StrategyCellScript>())
        {
            child.DelayedTurnUpdate();
        }

        Debug.Log("Cells Late Updated");

        if (turnNumber % 4 == 0)
        {
            SpawnVirus();
        }

        cellNum = gameObject.GetComponentsInChildren<StrategyCellScript>().Length;
        virNum = gameObject.GetComponentsInChildren<StrategyVirusScript>().Length;

        if (cellNum > 10 && virusPrefab == virusPrefab1)
        {
            Debug.Log("Viruses Have Mutated");
            virusPrefab = virusPrefab2;
        }

        if (cellNum > 30 && virusPrefab == virusPrefab2)
        {
            Debug.Log("Viruses Have Mutated");
            virusPrefab = virusPrefab3;
        }

        sun.TurnUpdate();

        screenUI.text = "Actions Left: " + actionsLeft + "\nTurn Number: " + turnNumber + "\nCells Alive: " + cellNum + "\nViruses Alive: " + virNum;
        Debug.Log("Turn Updated");
    }

    public void AddToDictionary(GameObject cell)
    {
        tiles.Add(cell.GetComponent<StrategyCellScript>().key, cell);
    }

    public void SelectCellSpawn(Vector2 starting)
    {
        Queue<Vector2> que = new Queue<Vector2>();
        HashSet<Vector2> visited = new HashSet<Vector2>(new Vector2Comparer());
        que.Enqueue(starting);
        visited.Add(starting);
        while (true)
        {
            //Top Right (0, +1)
            Vector2 check = que.Peek();
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

            que.Dequeue();
        }
    }

    float CalculateY(Vector2 k)
    {
        float avg = 0.0f;
        int total = 0;

        //Top Right (0, +1)
        Vector2 check = k;
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

        avg /= total;
        if (avg == float.NaN)
        {
            avg = 0.0f;
        }

        return Mathf.Clamp(Random.Range(-randomRange, randomRange) + avg, -4.0f, 4.0f);
    }

    void SpawnCell(Vector2 k, Vector2 p)
    {
        Vector3 spawnLocation = tiles[p].transform.position;
        Vector3 desination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, CalculateY(k), k.y * yOffset);
        GameObject t = Instantiate(transporter, spawnLocation, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = desination;
        GameObject c = Instantiate(cellPrefab, spawnLocation, Quaternion.identity, t.transform) as GameObject;
        c.GetComponent<StrategyCellScript>().key = k;
        AddToDictionary(c);
        c.name = "Cell" + k.x + "_" + k.y;
        t.GetComponent<StrategyTransporter>().enabled = true;
        c.GetComponent<StrategyCellScript>().enabled = true;
    }

    public void KillCell(Vector2 k)
    {
        GameObject instance = tiles[k];
        tiles.Remove(k);
        Destroy(instance);
    }

    public int SpreadImmunity(Vector2 starting)
    {
        int immunitySpread = 0;

        //Top Right (0, +1)
        Vector2 check = starting;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            if (tiles[check].GetComponent<StrategyCellScript>().AddImmunity())
                immunitySpread++;
        }

        //Right (+1, 0)
        check = starting;
        check.x += 1;
        if (tiles.ContainsKey(check))
        {
            if (tiles[check].GetComponent<StrategyCellScript>().AddImmunity())
                immunitySpread++;
        }

        //Bottom Right (0, -1)
        check = starting;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            if (tiles[check].GetComponent<StrategyCellScript>().AddImmunity())
                immunitySpread++;
        }

        //Bottom Left (-1, -1)
        check = starting;
        check.x -= 1;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            if (tiles[check].GetComponent<StrategyCellScript>().AddImmunity())
                immunitySpread++;
        }

        //Left (-1, 0)
        check = starting;
        check.x -= 1;
        if (tiles.ContainsKey(check))
        {
            if (tiles[check].GetComponent<StrategyCellScript>().AddImmunity())
                immunitySpread++;
        }

        //Top Left (-1, +1)
        check = starting;
        check.x -= 1;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            if (tiles[check].GetComponent<StrategyCellScript>().AddImmunity())
                immunitySpread++;
        }

        return immunitySpread;
    }

    public void SpawnVirus()
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0.65f, 1f);
        float distance = 100.0f;
        Vector3 position = direction * distance;
        GameObject v = Instantiate(virusPrefab, position, Quaternion.identity, transform) as GameObject;
        v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
        v.GetComponent<Collider>().enabled = true;
        v.GetComponent<StrategyVirusScript>().enabled = true;
    }

    //Attempts to spawn a virus on an adjacent cell
    //If one of them isn't open it targets a random cell or goes on standby
    public void SpawnVirusSingleAdjacent(Vector2 k, Vector3 p)
    {
        //Top Right
        Vector2 check = k;
        check.y += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y* yOffset +yOffset * .5f);
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Right (+1, 0)
        check = k;
        check.x += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset : k.x * xOffset + xOffset * .5f, 1, k.y * yOffset);
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Bottom Right (0, -1)
        check = k;
        check.y -= 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset - yOffset * .5f);
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Bottom Left (-1, -1)
        check = k;
        check.x -= 1;
        check.y -= 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset - yOffset * .5f);
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Left (-1, 0)
        check = k;
        check.x -= 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset);
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }

        //Top Left (-1, +1)
        check = k;
        check.x -= 1;
        check.y += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
            t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset + yOffset * .5f);
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
            return;
        }
        
        GameObject tra = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        tra.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 2, k.y * yOffset);
        GameObject vir = Instantiate(virusPrefab, p, Quaternion.identity, tra.transform) as GameObject;
        vir.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(vir);
        vir.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
        tra.GetComponent<StrategyTransporter>().enabled = true;
        vir.GetComponent<StrategyVirusScript>().enabled = true;
    }

    //Attempts to spawn viruses on all adjacent cells
    //If one of them isn't open it targets a random cell or goes on standby
    public void SpawnVirusAllAdjacent(Vector2 k, Vector3 p)
    {
        //Top Right
        Vector2 check = k;
        check.y += 1;
        GameObject t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y* yOffset +yOffset * .5f);
        GameObject v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Right (+1, 0)
        check = k;
        check.x += 1;
        t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset : k.x * xOffset + xOffset * .5f, 1, k.y * yOffset);
        v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Bottom Right (0, -1)
        check = k;
        check.y -= 1;
        t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset - yOffset * .5f);
        v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Bottom Left (-1, -1)
        check = k;
        check.x -= 1;
        check.y -= 1;
        t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset - yOffset * .5f);
        v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Left (-1, 0)
        check = k;
        check.x -= 1;
        t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset);
        v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }

        //Top Left (-1, +1)
        check = k;
        check.x -= 1;
        check.y += 1;
        t = Instantiate(transporter, p, Quaternion.identity, transform) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset + yOffset * .5f);
        v = Instantiate(virusPrefab, p, Quaternion.identity, t.transform) as GameObject;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<StrategyCellScript>().targeted)
        {
            tiles[check].GetComponent<StrategyCellScript>().targeted = true;
            v.GetComponent<StrategyVirusScript>().target = tiles[check];
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
        else
        {
            v.GetComponent<StrategyVirusScript>().target = FindVirusNewTarget(v);
            v.GetComponent<StrategyVirusScript>().percentTraveled = .75f;
            t.GetComponent<StrategyTransporter>().enabled = true;
            v.GetComponent<StrategyVirusScript>().enabled = true;
        }
    }

    public GameObject FindVirusNewTarget(GameObject vir)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = tiles.Values.ElementAt(Random.Range(0, tiles.Values.Count));

            if (!temp.GetComponent<StrategyCellScript>().targeted)
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
        return new Vector3(Random.Range(tiles.Count * .2f * -1.0f, tiles.Count * .17f), 10, Random.Range(tiles.Count * .2f * -1.0f, tiles.Count * .17f));
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
