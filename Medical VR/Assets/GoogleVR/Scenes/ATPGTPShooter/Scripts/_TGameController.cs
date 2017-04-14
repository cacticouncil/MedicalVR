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
    public int scorePerEnzyme;
    public int score;

    public GameObject viralEnzyme;
    public GameObject enzyme;
    public GameObject enzymeCollector;
    public int maxNumEnzymes;
    public GameObject nucleus;
    public Boundary spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start ()
    {
        score = 0;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                if (maxNumEnzymes > transform.GetChild(0).childCount)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject haz = Instantiate(enzyme, spawnPosition, spawnRotation, enzymeCollector.transform) as GameObject;
                    haz.GetComponent<_TTravelToNucleus>().nucleus = nucleus;
                    if (haz.GetComponent<_TEnzymeController>())
                        haz.GetComponent<_TEnzymeController>().pointsValue = scorePerEnzyme;
                    else
                        Debug.Log("Unable to access Enzyme Controller");

                   // Debug.Log("Velocity:" haz.transform.G)

                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
    public void AddToScore(int _points)
    {
        score += _points;
    }
}
