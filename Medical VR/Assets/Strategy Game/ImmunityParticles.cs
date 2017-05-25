using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImmunityParticles : MonoBehaviour
{
    public static List<ImmunityParticles> list = new List<ImmunityParticles>();

    public ParticleSystem p;
    public Transform target;
    public float immunity;
    public float startSpeed = 15.0f;

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
        e.rateOverTimeMultiplier = Mathf.CeilToInt(immunity);
        lifetime = p.main.startLifetimeMultiplier;
        var par = p.main;
        par.startLifetimeMultiplier = 100.0f;
        par.startSpeedMultiplier = startSpeed;

        StartCoroutine(Emit());
        StartCoroutine(Curve());
    }

    void OnDisable()
    {
        var par = p.main;
        par.startLifetimeMultiplier = lifetime;
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

            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator Curve()
    {
        yield return 0;
        while (p.IsAlive())
        {
            InitializeIfNeeded();
            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            int numParticlesAlive = p.GetParticles(particles);

            if (target == null)
            {
                for (int i = 0; i < numParticlesAlive; i++)
                {
                    particles[i].remainingLifetime = 0;
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
                        particles[j].remainingLifetime = 0;
                    }

                    // Apply the particle changes to the particle system
                    p.SetParticles(particles, numParticlesAlive);
                    yield return 0;
                    Destroy(gameObject);
                    yield break;
                }
                else if (V3Equal(particles[i].position, target.position))
                {
                    particles[i].remainingLifetime = 0.0f;
                }
                else
                {
                    float f = 1.0f - Mathf.Clamp01((particles[i].remainingLifetime - (p.main.startLifetimeMultiplier - lifetime)) / lifetime);
                    if (f > threshHold)
                    {
                        Color32 c = particles[i].startColor;
                        if (c.a < fadeRate)
                        {
                            c.a = 0;
                            particles[i].remainingLifetime = 0.0f;
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
        list.Remove(this);
        Destroy(gameObject);
        //this.enabled = false;
    }

    private void InitializeIfNeeded()
    {
        if (p == null)
            p = GetComponent<ParticleSystem>();

        if (particles == null || particles.Length < p.main.maxParticles)
            particles = new ParticleSystem.Particle[p.main.maxParticles];
    }

    private bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < vComparer;
    }
}
