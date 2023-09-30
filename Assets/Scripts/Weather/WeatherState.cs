using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Weather", menuName = "Scriptables/Weather", order = 1)]
public class WeatherState : ScriptableObject
{
    [SerializeField] private Momentum momentum;
    [SerializeField] private Material material;
    [SerializeField][Range(0, 1)] private float lightIntensity;
    [SerializeField] private Color lightColor;
    [SerializeField][Range(0, 1)] private float ambientLightIntensity;
    [SerializeField] private Color ambientLightColor;

    public Momentum Momentum
    {
        get => momentum;
        set => momentum = value;
    }

    public Material Material
    {
        get => material;
        set => material = value;
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

    public float AmbientLightIntensity
    {
        get => ambientLightIntensity;
        set => ambientLightIntensity = value;
    }

    public Color AmbientLightColor
    {
        get => ambientLightColor;
        set => ambientLightColor = value;
    }

}
