using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinCollectorScript : MonoBehaviour
{
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GameObject hazardCollector;
    public GameObject[] obsticles;
    public Boundary spawnValues;
    public int hazardCount;
    public int maxNumHazards;
    public GameObject spawnLocation;

    IEnumerator SpawnHazards()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                if (maxNumHazards > hazardCollector.transform.childCount)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;

                    GameObject haz;

                    if (spawnLocation)
                    {
                        haz = Instantiate(obsticles[Random.Range(0, obsticles.Length)], spawnPosition, spawnRotation, spawnLocation.transform) as GameObject;
                        haz.transform.parent = hazardCollector.transform;
                    }
                    else
                        haz = Instantiate(obsticles[Random.Range(0, obsticles.Length)], spawnPosition, spawnRotation, hazardCollector.transform) as GameObject;
                    // Instantiate(obsticles[Random.Range(0, obsticles.Length)], spawnPosition, spawnRotation, hazardCollector.transform);
                }

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);
        }
    }

    public void StartHazards()
    {
        StartCoroutine("SpawnHazards");
    }

    public void StopHazards()
    {
        StopCoroutine("SpawnHazards");
    }
}
