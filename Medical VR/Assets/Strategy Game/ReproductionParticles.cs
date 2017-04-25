using UnityEngine;
using System.Collections;

public class ReproductionParticles : MonoBehaviour
{
    public Transform target;

    ParticleSystem p;
    ParticleSystem.Particle[] particles;
    private int count = 5;
    private float lifetime;
    private float threshHold = .99f;
    private byte fadeRate = 2;

    // Use this for initialization
    void OnEnable()
    {
        p = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule e = p.emission;
        e.rate = 5;
        lifetime = p.startLifetime;
        p.startLifetime = 100.0f;

        StartCoroutine(Emit());
        StartCoroutine(Curve());
    }

    void OnDisable()
    {
        p.startLifetime = lifetime;
    }

    private IEnumerator Emit()
    {
        float delay = .2f;
        while (count > 0)
        {
            var emitParams = new ParticleSystem.EmitParams();
            p.Emit(emitParams, 1);
            count--;

            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator Curve()
    {
        while (p.IsAlive())
        {
            InitializeIfNeeded();
            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            int numParticlesAlive = p.GetParticles(particles);

            if (target == null)
            {
                for (int i = 0; i < numParticlesAlive; i++)
                {
                    particles[i].lifetime = 0;
                }

                // Apply the particle changes to the particle system
                p.SetParticles(particles, numParticlesAlive);
                yield return 0;
            }

            for (int i = 0; i < numParticlesAlive; i++)
            {
                if (target == null)
                {
                    for (int j = 0; j < numParticlesAlive; j++)
                    {
                        particles[j].lifetime = 0;
                    }

                    // Apply the particle changes to the particle system
                    p.SetParticles(particles, numParticlesAlive);
                    yield return 0;
                    Destroy(gameObject);
                    yield break;
                }
                else if (Equals(particles[i].position, target.position))
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
                        {
                            c.a = 0;
                            particles[i].lifetime = 0.0f;
                        }
                        else
                        {
                            c.a -= fadeRate;
                        }
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
}
