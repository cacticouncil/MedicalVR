using UnityEngine;
using System.Collections;

public class _TCreateObjects : MonoBehaviour {
    GameObject parent;
    GameObject objects;
    int numberOfObjects;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < numberOfObjects; ++i)
            Instantiate(objects, parent.transform);
    }
}
