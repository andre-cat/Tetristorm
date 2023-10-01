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
    [SerializeField] private ParticleSystem storms1;
    [SerializeField] private ParticleSystem storms2;

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
            if (storms1.gameObject.activeSelf)
            {
                storms1.gameObject.SetActive(false);
            }

            if (storms2.gameObject.activeSelf)
            {
                storms2.gameObject.SetActive(false);
            }

        }
        else
        {
            if (!storms1.gameObject.activeSelf)
            {
                storms1.gameObject.SetActive(true);
            }

            if (!storms2.gameObject.activeSelf)
            {
                storms2.gameObject.SetActive(true);
            }
        }
    }
}
