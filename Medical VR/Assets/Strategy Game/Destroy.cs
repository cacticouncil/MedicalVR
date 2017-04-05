using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour
{
    public void Kill()
    {
        Destroy(gameObject);
    }
}
