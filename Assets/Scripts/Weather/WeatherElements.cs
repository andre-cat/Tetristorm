using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherElements : MonoBehaviour
{

    [Header("REFERENCES")]
    [SerializeField] private Weather weather;

    [Header("-- Clouds")]
    [SerializeField] private ParticleSystem cloudParticles1;
    [SerializeField] private ParticleSystem cloudParticles2;

    [Header("-- Rain")]
    [SerializeField] private ParticleSystem rain;

    [Header("-- Storm")]
    [SerializeField] private ParticleSystem storms;

    private void FixedUpdate()
    {
        if ((int)weather.Momentum < (int)Momentum.Cloudy)
        {

            if (cloudParticles1.gameObject.activeSelf)
            {
                cloudParticles1.gameObject.SetActive(false);
            }

            if (cloudParticles2.gameObject.activeSelf)
            {
                cloudParticles2.gameObject.SetActive(false);
            }
        }
        else
        {

            if (!cloudParticles1.gameObject.activeSelf)
            {
                cloudParticles1.gameObject.SetActive(true);
            }

            if (!cloudParticles2.gameObject.activeSelf)
            {
                cloudParticles2.gameObject.SetActive(true);
            }
        }

        if ((int)weather.Momentum < (int)Momentum.Rainy)
        {
            rain.gameObject.SetActive(false);
        }
        else
        {
            rain.gameObject.SetActive(true);
        }

        if ((int)weather.Momentum < (int)Momentum.Stormy)
        {
            storms.gameObject.SetActive(false);
        }
        else
        {
            storms.gameObject.SetActive(true);
        }
    }
}
