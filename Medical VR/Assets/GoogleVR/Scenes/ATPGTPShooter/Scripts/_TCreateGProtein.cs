using UnityEngine;
using System.Collections;

public class _TCreateGProtein : MonoBehaviour
{
    public GameObject GProtein;
    public GameObject shotSpawner;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GameObject[] adenylylCyclase;

    void Start()
    {
        if (!shotSpawner)
        {
            Debug.Log("failed to load shotSpawner");
        }
        if (!GProtein)
        {
            Debug.Log("failed to load GProtein");
        }

    }

    void Update()
    {

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < adenylylCyclase.Length; ++i)
            {
                Vector3 spawnPosition = shotSpawner.GetComponent<Transform>().position;
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(GProtein, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }

    }
}
