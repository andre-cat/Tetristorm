using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSquare : MonoBehaviour
{
    public ParticleSystem[] particles;
    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void Play()
    {
        foreach(ParticleSystem ps in particles)
        {
            ps.Stop();
            ps.Play();
        }
    }

}

    
