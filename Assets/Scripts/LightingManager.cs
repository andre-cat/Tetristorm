using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Lighting lighting;
    [SerializeField][Range(0, 24)] private float dayTime;
    [SerializeField] Momentum momentum;

    private void Start()
    {
        momentum = Momentum.Morning;
        dayTime = (int)momentum;
    }

    private void Update()
    {
        UpdateToHour((int)momentum);
    }

    private void UpdateToHour(float hour)
    {
        if (lighting != null)
        {
            if (Application.isPlaying)
            {
                if (Math.Abs(hour - dayTime) > 0.1)
                {
                    if (dayTime < hour)
                    {
                        dayTime += Time.deltaTime;
                    }
                    else
                    {
                        dayTime -= Time.deltaTime;
                    }
                    dayTime %= 24;
                    UpdateLighting(dayTime / 24);
                }
            }
        }
    }

    private void Update24Hours()
    {
        if (lighting != null)
        {
            if (Application.isPlaying)
            {
                dayTime += Time.deltaTime;
                dayTime %= 24;
            }
            UpdateLighting(dayTime / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lighting.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lighting.fogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = lighting.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(timePercent * 360f - 90f, -170f, 0));
        }
    }

    private void OnValidate()
    {
        if (directionalLight == null)
        {
            if (RenderSettings.sun != null)
            {
                directionalLight = RenderSettings.sun;
            }
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }

    private enum Momentum
    {
        Morning = 6,
        Afternoon = 12,
        Evening = 18,
        Night = 24
    }

}
