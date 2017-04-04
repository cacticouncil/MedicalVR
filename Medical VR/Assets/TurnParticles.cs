using UnityEngine;
using System.Collections;

public class TurnParticles : MonoBehaviour
{
    public Transform target;
    ParticleSystem p;
    ParticleSystem.Particle[] particles;
    // Use this for initialization
    void OnEnable()
    {
        p = GetComponent<ParticleSystem>();
        p.Play();
        StartCoroutine(Curve());
    }

    private IEnumerator Curve()
    {
        while (p.IsAlive())
        {
            InitializeIfNeeded();

            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            int numParticlesAlive = p.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].rotation3D = Vector3.zero;
                float f = particles[i].lifetime / particles[i].startLifetime;
                Vector3 direction = target.position - particles[i].position;
                particles[i].velocity = particles[i].velocity * f + direction * (1.0f - f);
            }

            // Apply the particle changes to the particle system
            p.SetParticles(particles, numParticlesAlive);

            yield return new WaitForFixedUpdate();
        }
        this.enabled = false;
    }

    void InitializeIfNeeded()
    {
        if (p == null)
            p = GetComponent<ParticleSystem>();

        if (particles == null || particles.Length < p.maxParticles)
            particles = new ParticleSystem.Particle[p.maxParticles];
    }
}
