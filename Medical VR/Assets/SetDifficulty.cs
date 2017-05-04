using UnityEngine;
using System.Collections;

public class SetDifficulty : MonoBehaviour
{
    public void SetDiff(int i)
    {
        GlobalVariables.difficulty = i;
    }
}
