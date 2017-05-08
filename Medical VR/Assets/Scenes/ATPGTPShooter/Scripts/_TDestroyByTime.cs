using UnityEngine;
using System.Collections;

public class _TDestroyByTime : MonoBehaviour
{
    public float deathTime;
    bool isInit = false;

    //    void Start()
    //    {
    //        if (deathTime > 1.0f)
    //            Destroy(gameObject, deathTime);
    //    }

    void Start()
    {
        Init();
    }
    void Init()
    {
        if (isInit)
            return;
        isInit = true;
        Invoke("DestroyMe", deathTime);
    }

    public void CancelDestroy()
    {
        if (!isInit)
            Init();
        CancelInvoke("DestroyMe");
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
