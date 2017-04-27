using UnityEngine;
using System.Collections;

public class SetBool : MonoBehaviour
{
    public GameObject prefab;

    public void SetBools(bool s)
    {
        prefab.GetComponent<Tutorial>().tutorial = s;
    }
}
