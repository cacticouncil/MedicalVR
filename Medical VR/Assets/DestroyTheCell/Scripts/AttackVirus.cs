using UnityEngine;
using System.Collections;

public class AttackVirus : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject WaveManager;
    public GameObject target;
    float Speed = .06f;
    float TempTimerForAttackVirusToLeaveCell = 0.0f;
    public bool CanLeaveCell = false;

    void Start()
    {
        WaveManager = MainCamera.GetComponent<VirusPlayer>().WaveManager;
    }

    void FixedUpdate()
    {
        if (TempTimerForAttackVirusToLeaveCell >= 7.5f)
            Destroy(this.gameObject);

        else if (CanLeaveCell == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed);
            if (transform.position == target.transform.position)
            {
                target.GetComponent<CellReceptors>().Health--;
                SoundManager.PlaySFX("Fight Virus Tutorial/sfx_deathscream_android7");

                if (target.GetComponent<CellReceptors>().Health <= 0)
                    Destroy(target);
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
