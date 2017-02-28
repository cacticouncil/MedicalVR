using UnityEngine;
using System.Collections;

enum CellMovement { MoveRight = 0, MoveLeft = 1, MoveUp = 2, MoveDown = 3 }
public class Cell : MonoBehaviour
{
    public bool isDead;
    CellMovement CM;
    Vector3 StartPos;
    void Start()
    {
        isDead = false;
        StartPos = transform.position;
        CM = (CellMovement)Random.Range(0, 4);
    }

    void Update()
    {
        switch (CM)
        {
            case CellMovement.MoveRight:
                transform.position = new Vector3(StartPos.x + Mathf.Sin(Time.time), StartPos.y, StartPos.z);
                break;
            case CellMovement.MoveLeft:
                transform.position = new Vector3(StartPos.x - Mathf.Sin(Time.time), StartPos.y, StartPos.z);
                break;
            case CellMovement.MoveUp:
                transform.position = new Vector3(StartPos.x, StartPos.y + Mathf.Sin(Time.time), StartPos.z);
                break;
            case CellMovement.MoveDown:
                transform.position = new Vector3(StartPos.x, StartPos.y - Mathf.Sin(Time.time), StartPos.z);
                break;

            default:
                break;
        }
    }
}
