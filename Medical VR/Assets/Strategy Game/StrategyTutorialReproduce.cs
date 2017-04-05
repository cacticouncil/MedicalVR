using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialReproduce : MonoBehaviour
{
    public GameObject cellMesh;
    public GameObject transporter;
    public List<GameObject> children = new List<GameObject>();


    public float reproduction = 0;
    public float Treproduction = 50;
    public float reproductionReset = 50;
    public int childrenSpawned = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Reproduce());
    }

    IEnumerator Reproduce()
    {
        while (true)
        {
            Treproduction -= 2.0f * Mathf.Sqrt(reproduction * 10 + 1);
            while (Treproduction <= 0)
            {
                //reproduce
                SelectCellSpawn();
                Treproduction = reproductionReset + Treproduction;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void SelectCellSpawn()
    {
        switch (childrenSpawned)
        {
            case 0:
                {
                    //Top Right (1, 1)
                    Vector3 desination = new Vector3(2, transform.position.y, 2);
                    GameObject t = Instantiate(transporter, transform.position, Quaternion.identity, transform) as GameObject;
                    t.GetComponent<StrategyTransporter>().destination = desination;
                    GameObject c = Instantiate(cellMesh, transform.position, cellMesh.transform.rotation, t.transform) as GameObject;
                    c.name = "0_1";
                    t.GetComponent<StrategyTransporter>().enabled = true;
                    children.Add(c);
                    childrenSpawned++;
                }
                break;
            case 1:
                {
                    //Right (1, 0)
                    Vector3 desination = new Vector3(3, transform.position.y, 0);
                    GameObject t = Instantiate(transporter, transform.position, Quaternion.identity, transform) as GameObject;
                    t.GetComponent<StrategyTransporter>().destination = desination;
                    GameObject c = Instantiate(cellMesh, transform.position, cellMesh.transform.rotation, t.transform) as GameObject;
                    c.name = "1_0";
                    t.GetComponent<StrategyTransporter>().enabled = true;
                    children.Add(c);
                    childrenSpawned++;
                }
                break;
            case 2:
                {
                    //Bottom Right (1, -1)
                    Vector3 desination = new Vector3(2, transform.position.y, -2);
                    GameObject t = Instantiate(transporter, transform.position, Quaternion.identity, transform) as GameObject;
                    t.GetComponent<StrategyTransporter>().destination = desination;
                    GameObject c = Instantiate(cellMesh, transform.position, cellMesh.transform.rotation, t.transform) as GameObject;
                    c.name = "0_-1";
                    t.GetComponent<StrategyTransporter>().enabled = true;
                    children.Add(c);
                    childrenSpawned++;
                }
                break;
            case 3:
                {
                    //Bottom Left (0, -1)
                    Vector3 desination = new Vector3(0, transform.position.y, -2);
                    GameObject t = Instantiate(transporter, transform.position, Quaternion.identity, transform) as GameObject;
                    t.GetComponent<StrategyTransporter>().destination = desination;
                    GameObject c = Instantiate(cellMesh, transform.position, cellMesh.transform.rotation, t.transform) as GameObject;
                    c.name = "-1_-1";
                    t.GetComponent<StrategyTransporter>().enabled = true;
                    children.Add(c);
                    childrenSpawned++;
                }
                break;
            case 4:
                {
                    //Left (-1, 0)
                    Vector3 desination = new Vector3(-1, transform.position.y, 0);
                    GameObject t = Instantiate(transporter, transform.position, Quaternion.identity, transform) as GameObject;
                    t.GetComponent<StrategyTransporter>().destination = desination;
                    GameObject c = Instantiate(cellMesh, transform.position, cellMesh.transform.rotation, t.transform) as GameObject;
                    c.name = "-1_0";
                    t.GetComponent<StrategyTransporter>().enabled = true;
                    children.Add(c);
                    childrenSpawned++;
                }
                break;
            case 5:
                {
                    //Top Left (0, 1)
                    Vector3 desination = new Vector3(0, transform.position.y, 2);
                    GameObject t = Instantiate(transporter, transform.position, Quaternion.identity, transform) as GameObject;
                    t.GetComponent<StrategyTransporter>().destination = desination;
                    GameObject c = Instantiate(cellMesh, transform.position, cellMesh.transform.rotation, t.transform) as GameObject;
                    c.name = "-1_1";
                    t.GetComponent<StrategyTransporter>().enabled = true;
                    children.Add(c);
                    childrenSpawned = 0;

                    Invoke("Shrink", 1f);
                }
                break;
        }
    }

    void Shrink()
    {
        while (children.Count > 0)
        {
            children[0].GetComponent<Shrink>().enabled = true;
            children.RemoveAt(0);
        }
    }
}
