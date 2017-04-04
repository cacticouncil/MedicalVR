using UnityEngine;
using System.Collections;

public class StrategyTutorial : MonoBehaviour
{
    public GameObject redCell;
    public Vector3 cellPosition;

    private int rNum = 0;

    public void Click()
    {

    }

    IEnumerator SpawnRedCell()
    {
        GameObject c = Instantiate(redCell, cellPosition, redCell.transform.rotation) as GameObject;
        Vector3 desination;
        switch (rNum)
        {
            case 0:
                desination = cellPosition;
                break;
            default:
                break;
        }

        yield return 0;
    }
}
