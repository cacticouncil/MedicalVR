using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CellManagerScript : MonoBehaviour
{
    public Dictionary<Vector2, GameObject> tiles = new Dictionary<Vector2, GameObject>(new Vector2Comparer());
    public GameObject cellPrefab;
    public GameObject virus;
    private float xOffset = 1.0f;
    private float yOffset = 1.0f;

    public int actionsLeft = 4;
    public int turnNumber = 0;

    // Use this for initialization
    void Start()
    {
        GameObject t = Instantiate(cellPrefab, new Vector3(.5f, 0, 0), Quaternion.identity, transform) as GameObject;
        t.GetComponent<CellScript>().key = new Vector2(0, 0);
        AddToDictionary(t);
        t.name = "Cell0_0";
        t.GetComponent<CellScript>().enabled = true;
        t.GetComponent<CellScript>().ToggleUI(true);
    }

    public void ActionPreformed()
    {
        actionsLeft--;
        if (actionsLeft == 0)
        {
            actionsLeft = 4;
            TurnUpdate();
        }
    }

    public void TurnUpdate()
    {
        Debug.Log("Turn Updated");
        turnNumber++;
        foreach (CellScript child in gameObject.GetComponentsInChildren<CellScript>())
        {
            child.TurnUpdate();
        }

        foreach (VirusScript child in gameObject.GetComponentsInChildren<VirusScript>())
        {
            child.TurnUpdate();
        }
        
        foreach (CellScript child in gameObject.GetComponentsInChildren<CellScript>())
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
        tiles.Add(cell.GetComponent<CellScript>().key, cell);
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
        t.GetComponent<CellScript>().key = k;
        AddToDictionary(t);
        t.name = "Cell" + k.x + "_" + k.y;
        t.GetComponent<CellScript>().enabled = true;
    }

    public void KillCell(Vector2 k)
    {
        Destroy(tiles[k]);
        tiles.Remove(k);
    }

    public void SpreadImmunity(Vector2 starting)
    {
        //Top Right (0, +1)
        Vector2 check = starting;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().AddImmunity();
        }

        //Right (+1, 0)
        check = starting;
        check.x += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().AddImmunity();
        }

        //Bottom Right (0, -1)
        check = starting;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().AddImmunity();
        }

        //Bottom Left (-1, -1)
        check = starting;
        check.x -= 1;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().AddImmunity();
        }

        //Left (-1, 0)
        check = starting;
        check.x -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().AddImmunity();
        }

        //Top Left (-1, +1)
        check = starting;
        check.x -= 1;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().AddImmunity();
        }
    }

    public void SpawnVirus()
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0.65f, 1f);
        float distance = 100.0f;
        Vector3 position = direction * distance;
        GameObject v = Instantiate(virus, position, Quaternion.identity, transform) as GameObject;
        GameObject temp = tiles.Values.ElementAt(Random.Range(0, tiles.Values.Count));

        for (int i = 0; i < 10 || temp.GetComponent<CellScript>().targeted; i++)
        {
            temp = tiles.Values.ElementAt(Random.Range(0, tiles.Values.Count));
        }

        temp.GetComponent<CellScript>().targeted = true;
        v.GetComponent<VirusScript>().target = temp;
        v.GetComponent<VirusScript>().enabled = true;
    }

    public void SpawnVirusSingleAdjacent(Vector2 k)
    {
        //Top Right
        Vector2 check = k;
        check.y += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<CellScript>().targeted)
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
            return;
        }

        //Right (+1, 0)
        check = k;
        check.x += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset : k.x * xOffset + xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
            return;
        }

        //Bottom Right (0, -1)
        check = k;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
            return;
        }

        //Bottom Left (-1, -1)
        check = k;
        check.x -= 1;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
            return;
        }

        //Left (-1, 0)
        check = k;
        check.x -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
            return;
        }

        //Top Left (-1, +1)
        check = k;
        check.x -= 1;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
            return;
        }
        
        GameObject vir = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 2, k.y * yOffset), Quaternion.identity, transform) as GameObject;
        GameObject temp = tiles.Values.ElementAt(Random.Range(0, tiles.Values.Count));

        for (int i = 0; i < 10 || temp.GetComponent<CellScript>().targeted; i++)
        {
            temp = tiles.Values.ElementAt(Random.Range(0, tiles.Values.Count));
        }

        temp.GetComponent<CellScript>().targeted = true;
        vir.GetComponent<VirusScript>().target = temp;
        vir.GetComponent<VirusScript>().percentTraveled = .75f;
        vir.GetComponent<VirusScript>().enabled = true;
    }

    //Attempts to spawn virus on all adjacent cells
    //If one of them isn't open it just doesn't spawn
    public void SpawnVirusAllAdjacent(Vector2 k)
    {
        //Top Right
        Vector2 check = k;
        check.y += 1;
        if (tiles.ContainsKey(check) && !tiles[check].GetComponent<CellScript>().targeted)
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
        }

        //Right (+1, 0)
        check = k;
        check.x += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset : k.x * xOffset + xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
        }

        //Bottom Right (0, -1)
        check = k;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset + xOffset * .5f : k.x * xOffset, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
        }

        //Bottom Left (-1, -1)
        check = k;
        check.x -= 1;
        check.y -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset - yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
        }

        //Left (-1, 0)
        check = k;
        check.x -= 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
        }

        //Top Left (-1, +1)
        check = k;
        check.x -= 1;
        check.y += 1;
        if (tiles.ContainsKey(check))
        {
            tiles[check].GetComponent<CellScript>().targeted = true;
            GameObject v = Instantiate(virus, new Vector3(k.y % 2 == 0 ? k.x * xOffset - xOffset : k.x * xOffset - xOffset * .5f, 1, k.y * yOffset + yOffset * .5f), Quaternion.identity, transform) as GameObject;
            v.GetComponent<VirusScript>().target = tiles[check];
            v.GetComponent<VirusScript>().percentTraveled = .75f;
            v.GetComponent<VirusScript>().enabled = true;
        }
    }

    public GameObject FindVirusNewTarget()
    {
       return tiles.Values.ElementAt(Random.Range(0, tiles.Values.Count));
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
