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
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), 0.1f);

            if (transform.position == new Vector3(transform.position.x, transform.position.y + 20, transform.position.z))
            {

                Destroy(gameObject);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
