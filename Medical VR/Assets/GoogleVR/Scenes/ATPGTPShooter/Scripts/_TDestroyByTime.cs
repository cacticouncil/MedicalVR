using UnityEngine;
using System.Collections;

public class _TDestroyByTime : MonoBehaviour
{
    public float deathTime;

    //    void Start()
    //    {
    //        if (deathTime > 1.0f)
    //            Destroy(gameObject, deathTime);
    //    }

    void Start()
    {
        Invoke("DestroyMe", deathTime);
    }

    public void CancelDestroy()
    {
        CancelInvoke("DestroyMe");
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
