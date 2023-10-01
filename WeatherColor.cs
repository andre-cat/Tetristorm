using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherColor : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Weather weather;
    [SerializeField] private Material material;

    [Header("COLORS")]
    [SerializeField] private Color[] colors = new Color[Weather.States];

    private Dictionary<Momentum, Color> weatherColors;

    private Color currentColor;
    private bool isUpdating;

    private void Start()
    {
        weatherColors = new()
        {
            [Momentum.Sunny] = colors[0],
            [Momentum.Cloudy] = colors[1],
            [Momentum.Rainy] = colors[2],
            [Momentum.Stormy] = colors[3]
        };
    }

    private void FixedUpdate()
    {
        if (currentColor != weatherColors[weather.Momentum])
        {
            currentColor = weatherColors[weather.Momentum];

            if (isUpdating)
            {
                StopCoroutine(ChangeColor());
            }
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        isUpdating = true;

        Color iniColor = new(material.color.r, material.color.g, material.color.b, material.color.a);
        Color endColor = weatherColors[weather.Momentum];

        float secondsElapsed = 0;

        while (secondsElapsed < weather.TransitionSeconds)
        {
            material.color = Color.Lerp(iniColor, endColor, secondsElapsed / weather.TransitionSeconds);
            secondsElapsed += Time.deltaTime;
            yield return null;
        }

        material.color = endColor;

        isUpdating = false;
    }
}
