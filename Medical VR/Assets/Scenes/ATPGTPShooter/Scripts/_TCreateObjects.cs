using UnityEngine;
using System.Collections;

public class _TCreateObjects : MonoBehaviour
{
    public GameObject parent;
    public GameObject objects;
    public int numberOfObjects;

    bool isInit = false;

    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        if (isInit)
            return;
        isInit = true;
        if (objects)
            if (parent)
                for (int i = 0; i < numberOfObjects; ++i)
                    Instantiate(objects, parent.transform);
            else
                for (int i = 0; i < numberOfObjects; ++i)
                    Instantiate(objects);
        else
            Debug.Log("Could not Create Objects");

    }
}
