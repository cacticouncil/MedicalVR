using UnityEngine;
using System.Collections;

public class PlateletScript : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SoundManager.PlaySFX("wall");
            StartCoroutine(Reverse(other.gameObject.GetComponent<MovingCamera>()));
        }
    }

    IEnumerator Reverse(MovingCamera player)
    {
        player.score -= 10;
        if (player.score < 0)
            player.score = 0;

        float timer = 0;
        player.speed *= -.5f;

        while (timer < 3)
        {
            timer += Time.deltaTime;
            player.score -= Time.smoothDeltaTime;
            if (player.score < 0)
                player.score = 0;
            yield return 0;
        }
        player.speed = player.orgSpeed;
        timer = 0;
    }
}
