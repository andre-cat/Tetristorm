using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Weather", menuName = "Scriptables/Weather", order = 1)]
public class WeatherState : ScriptableObject
{
    [SerializeField] private Momentum momentum;
    [SerializeField][Range(0, 8)] private float skyboxExposure;
    [SerializeField] private Color skyboxTint;
    [SerializeField][Range(0, 8)] private float lightIntensity;
    [SerializeField] private Color lightColor;
    [SerializeField] private Color ambientLightColor;
    [SerializeField][Range(0, 1)] private float ambientLightIntensity;

    public Momentum Momentum
    {
        get => momentum;
        set => momentum = value;
    }

    public float SkyboxExposure
    {
        get => skyboxExposure;
        set => skyboxExposure = value;
    }

    public Color SkyboxTint
    {
        get => skyboxTint;
        set => skyboxTint = value;
    }

    public float LightIntensity
    {
        get => lightIntensity;
        set => lightIntensity = value;
    }

    public Color LightColor
    {
        get => lightColor;
        set => lightColor = value;
    }

    public Color AmbientLightColor {
        get => ambientLightColor;
        set => ambientLightColor = value;
    }

    public float AmbientLightIntensity
    {
        get => ambientLightIntensity;
        set => ambientLightIntensity = value;
    }

    public static int Length
    {
        get => Enum.GetValues(typeof(Momentum)).Length;
    }
}
