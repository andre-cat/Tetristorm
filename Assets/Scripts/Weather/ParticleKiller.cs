using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlesKiller : MonoBehaviour
{

    [Header("SETTINGS")]
    [SerializeField] private float remainingLifeTimeOnExit = 0;

    [Header("REFERENCES")]
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnParticleTrigger()
    {
        List<Particle> particles = new();

        if (_particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, particles) > 0)
        {
            Particle[] particlesArray = particles.ToArray();

            for (int i = 0; i < particlesArray.Length; i++)
            {
                if (particlesArray[i].remainingLifetime > remainingLifeTimeOnExit)
                {
                    particlesArray[i].remainingLifetime = remainingLifeTimeOnExit;
                }
            }

            particles = new List<Particle>(particlesArray);

            ParticlePhysicsExtensions.SetTriggerParticles(_particleSystem, ParticleSystemTriggerEventType.Exit, particles);
        }
    }
}
