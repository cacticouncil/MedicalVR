using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellManagerScript : MonoBehaviour {
    
    public Dictionary<Vector2, GameObject> tiles = new Dictionary<Vector2, GameObject>(new Vector2Comparer());
    public GameObject cellPrefab;
    private float xOffset = 1.0f;
    private float yOffset = 1.0f;

    // Use this for initialization
    void Start () {
        GameObject t = Instantiate(cellPrefab, new Vector3(.5f, 0, 0), Quaternion.identity, transform) as GameObject;
        t.GetComponent<CellScript>().key = new Vector2(0, 0);
        AddToDictionary(t);
        t.name = "Cell0_0";
        t.GetComponent<CellScript>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TurnUpdate()
    {
        Debug.Log("Turn Updated");
        foreach (CellScript child in gameObject.GetComponentsInChildren<CellScript>())
        {
            child.TurnUpdate();
        }
    }

    public void AddToDictionary(GameObject cell)
    {
        tiles.Add(cell.GetComponent<CellScript>().key, cell);
    }

    public void SpawnCell(Vector2 starting)
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
                Spawn(check);
                return;
            }
            que.Enqueue(check);

            //Right (+1, 0)
            check = que.Peek();
            check.x += 1;
            if (!tiles.ContainsKey(check))
            {
                Spawn(check);
                return;
            }
            que.Enqueue(check);

            //Bottom Right (0, -1)
            check = que.Peek();
            check.y -= 1;
            if (!tiles.ContainsKey(check))
            {
                Spawn(check);
                return;
            }
            que.Enqueue(check);

            //Bottom Left (-1, -1)
            check = que.Peek();
            check.x -= 1;
            check.y -= 1;
            if (!tiles.ContainsKey(check))
            {
                Spawn(check);
                return;
            }
            que.Enqueue(check);

            //Left (-1, 0)
            check = que.Peek();
            check.x -= 1;
            if (!tiles.ContainsKey(check))
            {
                Spawn(check);
                return;
            }
            que.Enqueue(check);

            //Top Left (-1, +1)
            check = que.Peek();
            check.x -= 1;
            check.y += 1;
            if (!tiles.ContainsKey(check))
            {
                Spawn(check);
                return;
            }
            que.Enqueue(check);

            que.Dequeue();
        }
    }

    void Spawn(Vector2 k)
    {
        GameObject t;
        if (k.y % 2 == 0)
        {
            t = Instantiate(cellPrefab, new Vector3(k.x * xOffset + xOffset * .5f, 0, k.y * yOffset), Quaternion.identity, transform) as GameObject;
        }
        else
        {
            t = Instantiate(cellPrefab, new Vector3(k.x * xOffset, 0, k.y * yOffset), Quaternion.identity, transform) as GameObject;
        }
        t.GetComponent<CellScript>().key = k;
        AddToDictionary(t);
        t.name = "Cell" + k.x + "_" + k.y;
        t.GetComponent<CellScript>().enabled = true;
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
