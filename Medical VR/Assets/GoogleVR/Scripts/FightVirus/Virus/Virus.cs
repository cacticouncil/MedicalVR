using UnityEngine;
using System.Collections;
enum VirusBehavior {LeaveCell = 0, AttackProtein = 1, AdvancedAI = 2}

public class Virus : MonoBehaviour
{
    VirusBehavior VB;
    Vector3 RandomPos;

    GameObject CellManager;
    public float speed = .7f;
    int RandomCell;
    void Start()
    {
        RandomPos = Random.onUnitSphere;
        RandomPos *= 50.0f;

        int RandomBehavior = Random.Range(0, 2);
        VB = (VirusBehavior)RandomBehavior;

        CellManager = GameObject.Find("CellSpawn");
        RandomCell = Random.Range(0, CellManager.GetComponent<CellManager>().CellCount);
    }

    void Update()
    {
        switch (VB)
        {
            case VirusBehavior.LeaveCell:
                transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed);
                break;

            case VirusBehavior.AttackProtein:
                //Closest Cell
                //GameObject GoToCell = null;
                //float ShortestDistance = float.MaxValue;
                //foreach (GameObject t in CellManager.GetComponent<CellManager>().Cells)
                //{
                //    float Distance = Vector3.Distance(t.transform.position, transform.position);
                //    if (Distance < ShortestDistance)
                //    {
                //        GoToCell = t;
                //        ShortestDistance = Distance;
                //    }
                //}

                //Random Cell
                transform.position = Vector3.MoveTowards(transform.position, CellManager.GetComponent<CellManager>().Cells[RandomCell].transform.position, speed);
                
                break;

            case VirusBehavior.AdvancedAI:
                break;

            default:
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
