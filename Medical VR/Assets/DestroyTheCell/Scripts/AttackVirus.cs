using UnityEngine;
using System.Collections;

public class AttackVirus : MonoBehaviour
{
    public VirusPlayer Player;
    public CellReceptors target;
    float Speed = .06f;
    float TempTimerForAttackVirusToLeaveCell = 0.0f;
    public bool CanLeaveCell = false;

    void FixedUpdate()
    {
        if (TempTimerForAttackVirusToLeaveCell >= 7.5f)
        {
            Destroy(gameObject);
        }

        else if (CanLeaveCell == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed);
            if (transform.position == target.transform.position)
            {
                target.Health--;
                SoundManager.PlaySFX("Fight Virus Tutorial/sfx_deathscream_android7");

                if (target.Health <= 0)
                {
                    Player.CurrentScore += 200;
                    Destroy(target.gameObject);
                }
                CanLeaveCell = true;
            }
        }

        else if (CanLeaveCell == true)
        {
            transform.position += transform.forward * Speed;
            TempTimerForAttackVirusToLeaveCell += Time.deltaTime;
        }
    }
}
