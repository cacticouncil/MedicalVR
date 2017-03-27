using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax;
    public float yMin, yMax;
    public float zMin, zMax;
}

public class _TGameController : MonoBehaviour
{
    public GameObject hazard;
    public GameObject nucleus;
    public Boundary spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start ()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(spawnValues.xMin, spawnValues.xMax),
                    Random.Range(spawnValues.yMin, spawnValues.yMax),
                    Random.Range(spawnValues.zMin, spawnValues.zMax));
                Quaternion spawnRotation = Quaternion.identity;
                GameObject haz = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;
                haz.GetComponent<_TTravelToNucleus>().nucleus = nucleus;
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
