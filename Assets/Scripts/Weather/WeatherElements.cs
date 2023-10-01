using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherElements : MonoBehaviour
{

    [Header("REFERENCES")]
    [SerializeField] private Weather weather;

    [Space(1)]
    [SerializeField] private GameObject clouds;
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject storms;

    private void FixedUpdate()
    {
        if ((int)weather.Momentum < (int)Momentum.Cloudy)
        {
            clouds.GetComponent<GhostMaterial>().Disappear();
        }
        else
        {
            clouds.SetActive(true);
        }

        if ((int)weather.Momentum < (int)Momentum.Rainy)
        {
            rain.GetComponent<GhostMaterial>().Disappear();
        }
        else
        {
            rain.SetActive(true);
        }

        if ((int)weather.Momentum < (int)Momentum.Stormy)
        {
            storms.GetComponent<GhostMaterial>().Disappear();
        }
        else
        {
            storms.SetActive(true);
        }
    }
}
