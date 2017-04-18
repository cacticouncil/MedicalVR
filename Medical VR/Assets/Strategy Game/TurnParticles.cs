using UnityEngine;
using System.Collections;

public class TurnParticles : MonoBehaviour
{
    public Transform target;
    public float immunity;
    public float startSpeed = 15.0f;

    ParticleSystem p;
    ParticleSystem.Particle[] particles;
    private float lifetime;
    private float threshHold = .99f;
    private byte fadeRate = 2;
    private float vComparer = .04f;

    // Use this for initialization
    void OnEnable()
    {
        p = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule e = p.emission;
        e.rate = Mathf.CeilToInt(immunity);
        lifetime = p.startLifetime;
        p.startLifetime = 100.0f;
        p.startSpeed = startSpeed;

        StartCoroutine(Emit());
        StartCoroutine(Curve());
    }

    void OnDisable()
    {
        p.startLifetime = lifetime;
    }

    private IEnumerator Emit()
    {
        float delay = 1.0f / Mathf.CeilToInt(immunity);
        while (immunity > 0)
        {

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startSize = Mathf.Lerp(.1f, 1f, immunity);
            p.Emit(emitParams, 1);
            immunity--;

            yield return delay;
        }
    }

    private IEnumerator Curve()
    {
        while (p.IsAlive())
        {
            InitializeIfNeeded();
            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            int numParticlesAlive = p.GetParticles(particles);

            for (int i = 0; i < numParticlesAlive; i++)
            {
                if (V3Equal(particles[i].position, target.position))
                {
                    particles[i].lifetime = 0.0f;
                }
                else
                {
                    float f = 1.0f - Mathf.Clamp01((particles[i].lifetime - (p.startLifetime - lifetime)) / lifetime);
                    if (f > threshHold)
                    {
                        Color32 c = particles[i].startColor;
                        if (c.a < fadeRate)
                            c.a = 0;
                        else
                            c.a -= fadeRate;
                        particles[i].startColor = c;
                    }
                    Vector3 direction = target.position - particles[i].position;
                    particles[i].velocity = Vector3.Lerp(particles[i].velocity, direction, f);
                }
            }

            // Apply the particle changes to the particle system
            p.SetParticles(particles, numParticlesAlive);

            yield return 0;
        }
        Destroy(gameObject);
        //this.enabled = false;
    }

    private void InitializeIfNeeded()
    {
        if (p == null)
            p = GetComponent<ParticleSystem>();

        if (particles == null || particles.Length < p.maxParticles)
            particles = new ParticleSystem.Particle[p.maxParticles];
    }

    private bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < vComparer;
    }
}
