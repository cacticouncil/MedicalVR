using UnityEngine;
using System.Collections;
using System;

public class Query : MonoBehaviour
{

    public FBscript hi;
    public void HandleTimeInput()
    {
        hi.QueryScore();
    }
}
