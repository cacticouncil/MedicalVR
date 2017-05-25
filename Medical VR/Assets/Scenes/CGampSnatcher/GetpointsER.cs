using UnityEngine;
using System.Collections;

public class GetpointsER : MonoBehaviour
{
    public GameObject storebullets;
    public SpawnSting parent;
    public Transform position;

    private bool hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CBullet" && !hit)
        {
            hit = true;
            storebullets.GetComponent<Storebullets>().AddToScore(25);
            Storebullets.numberofstingsdone += 1;
            parent.takenPoints.Remove(position);
            StartCoroutine(MoveTo());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator MoveTo()
    {
        //if (SoundManager.IsJordanPlaying("180278__rodny5__surreal-bell") == false)
            SoundManager.PlayJordanVoice("180278__rodny5__surreal-bell");
        int x = 0;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), 0.1f);
            ++x;
            if (transform.position == new Vector3(transform.position.x, transform.position.y + 20, transform.position.z))
            {
                Debug.Log("In here this many times: " + x);
                Destroy(gameObject);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
