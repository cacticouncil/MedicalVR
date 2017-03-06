using UnityEngine;
using System.Collections;

public class _TDestroyByTime : MonoBehaviour
{
    public float deathTime;
    
	void Start ()
    {
        if (deathTime > 1.0f)
            Destroy(gameObject, deathTime);
	}
}
