using UnityEngine;
using System.Collections;

public class BannerScript : MonoBehaviour
{
    private static BannerScript localInstance;
    public static BannerScript banner { get { return localInstance; } }

    public GameObject target, hidden;
    private float speed = .6f;

    private void Awake()
    {
        if (localInstance != null && localInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            localInstance = this;
        }
    }

    public static void UnlockTrophy(string name)
    {
        if (PlayerPrefs.GetInt(name) != 1)
        {
            localInstance.StartCoroutine(localInstance.Show());
            PlayerPrefs.SetInt(name, 1);
            SoundManager.PlaySFX("MENU A_Select");
        }
    }

    private IEnumerator Show()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
            if (transform.position == target.transform.position)
            {
                StartCoroutine(Hide());
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, hidden.transform.position, speed);
            if (transform.position == hidden.transform.position)
            {
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public static void LockTrophy(string name)
    {
        PlayerPrefs.SetInt(name, -1);
    }
}
