using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weather : MonoBehaviour
{
    [Header("LIGHT")]
    [SerializeField] private Light sunLight;

    [Header("SKYBOX")]
    [SerializeField] private Material skyMaterial;

    [Header("WEATHER")]
    [SerializeField] private Momentum firstMomentum;
    [SerializeField] private WeatherState[] weathers = new WeatherState[Enum.GetValues(typeof(Momentum)).Length];
    [SerializeField][Min(0)] float weatherTransitionSeconds;

    private Dictionary<Momentum, WeatherState> weathersDictionary;

    private Momentum lastMomentum;

    private UnityEvent onMomentumChange;

    private bool isUpdating;

    private void Start()
    {
        weathersDictionary = new Dictionary<Momentum, WeatherState>();

        foreach (WeatherState weather in weathers)
        {
            weathersDictionary[weather.Momentum] = weather;
        }

        onMomentumChange = new UnityEvent();
        onMomentumChange.AddListener(OnMomentumChanged);

        lastMomentum = momentum = firstMomentum;
        StartCoroutine(ChangeWeather(weathersDictionary, lastMomentum, momentum, weatherTransitionSeconds));
    }

    private void OnMomentumChanged()
    {
        if (isUpdating)
        {
            StopCoroutine(ChangeWeather(weathersDictionary, lastMomentum, momentum, weatherTransitionSeconds));
        }
        StartCoroutine(ChangeWeather(weathersDictionary, lastMomentum, momentum, weatherTransitionSeconds));
    }

    private IEnumerator ChangeWeather(Dictionary<Momentum, WeatherState> weathersDictionary, Momentum lastMomentum, Momentum nextMomentum, float seconds)
    {
        isUpdating = true;

        WeatherState lastWeather = weathersDictionary[lastMomentum];
        WeatherState nextWeather = weathersDictionary[nextMomentum];

        float secondsElapsed = 0;

        Color sunDiscColor;
        float sunDiscMultiplier;
        float sunDiscExponent;
        Color sunHaloColor;
        float sunHaloExponent;
        float sunHaloContribution;
        Color horizonLineColor;
        float horizonLineExponent;
        float horizonLineContribution;
        Color skyGradientTop;
        Color skyGradientBottom;
        float skyGradientExponent;

        while (secondsElapsed < seconds)
        {
            float t = secondsElapsed / seconds;

            sunDiscColor = Color.Lerp(lastWeather.Material.GetColor("_SunDiscColor"), nextWeather.Material.GetColor("_SunDiscColor"), t);
            skyMaterial.SetColor("_SunDiscColor", sunDiscColor);

            sunDiscMultiplier = Mathf.Lerp(lastWeather.Material.GetFloat("_SunDiscMultiplier"), nextWeather.Material.GetFloat("_SunDiscMultiplier"), t);
            skyMaterial.SetFloat("_SunDiscMultiplier", sunDiscMultiplier);

            sunDiscExponent = Mathf.Lerp(lastWeather.Material.GetFloat("_SunDiscExponent"), nextWeather.Material.GetFloat("_SunDiscExponent"), t);
            skyMaterial.SetFloat("_SunDiscExponent", sunDiscExponent);

            sunHaloColor = Color.Lerp(lastWeather.Material.GetColor("_SunHaloColor"), nextWeather.Material.GetColor("_SunHaloColor"), t);
            skyMaterial.SetColor("_SunHaloColor", sunHaloColor);

            sunHaloExponent = Mathf.Lerp(lastWeather.Material.GetFloat("_SunHaloExponent"), nextWeather.Material.GetFloat("_SunHaloExponent"), t);
            skyMaterial.SetFloat("_SunHaloExponent", sunHaloExponent);

            sunHaloContribution = Mathf.Lerp(lastWeather.Material.GetFloat("_SunHaloContribution"), nextWeather.Material.GetFloat("_SunHaloContribution"), t);
            skyMaterial.SetFloat("_SunHaloContribution", sunHaloContribution);

            horizonLineColor = Color.Lerp(lastWeather.Material.GetColor("_HorizonLineColor"), nextWeather.Material.GetColor("_HorizonLineColor"), t);
            skyMaterial.SetColor("_HorizonLineColor", horizonLineColor);

            horizonLineExponent = Mathf.Lerp(lastWeather.Material.GetFloat("_HorizonLineExponent"), nextWeather.Material.GetFloat("_HorizonLineExponent"), t);
            skyMaterial.SetFloat("_HorizonLineExponent", horizonLineExponent);

            horizonLineContribution = Mathf.Lerp(lastWeather.Material.GetFloat("_HorizonLineContribution"), nextWeather.Material.GetFloat("_HorizonLineContribution"), t);
            skyMaterial.SetFloat("_HorizonLineContribution", horizonLineContribution);

            skyGradientTop = Color.Lerp(lastWeather.Material.GetColor("_SkyGradientTop"), nextWeather.Material.GetColor("_SkyGradientTop"), t);
            skyMaterial.SetColor("_SkyGradientTop", skyGradientTop);

            skyGradientBottom = Color.Lerp(lastWeather.Material.GetColor("_SkyGradientBottom"), nextWeather.Material.GetColor("_SkyGradientBottom"), t);
            skyMaterial.SetColor("_SkyGradientBottom", skyGradientBottom);

            skyGradientExponent = Mathf.Lerp(lastWeather.Material.GetFloat("_SkyGradientExponent"), nextWeather.Material.GetFloat("_SkyGradientExponent"), t);
            skyMaterial.SetFloat("_SkyGradientExponent", skyGradientExponent);

            float lightIntensity = Mathf.Lerp(lastWeather.LightIntensity, nextWeather.LightIntensity, secondsElapsed / seconds);
            sunLight.intensity = lightIntensity;

            Color lightColor = Color.Lerp(lastWeather.LightColor, nextWeather.LightColor, secondsElapsed / seconds);
            sunLight.color = lightColor;

            Color ambientColor = Color.Lerp(lastWeather.AmbientLightColor, nextWeather.AmbientLightColor, secondsElapsed / seconds);
            RenderSettings.ambientLight = ambientColor;

            float ambientLightIntensity = Mathf.Lerp(lastWeather.AmbientLightIntensity, nextWeather.AmbientLightIntensity, secondsElapsed / seconds);
            RenderSettings.ambientIntensity = ambientLightIntensity;

            secondsElapsed += Time.deltaTime;

            yield return null;
        }

        skyMaterial.SetColor("_SunDiscColor", nextWeather.Material.GetColor("_SunDiscColor"));
        skyMaterial.SetFloat("_SunDiscMultiplier", nextWeather.Material.GetFloat("_SunDiscMultiplier"));
        skyMaterial.SetFloat("_SunDiscExponent", nextWeather.Material.GetFloat("_SunDiscExponent"));
        skyMaterial.SetColor("_SunHaloColor", nextWeather.Material.GetColor("_SunHaloColor"));
        skyMaterial.SetFloat("_SunHaloExponent", nextWeather.Material.GetFloat("_SunHaloExponent"));
        skyMaterial.SetFloat("_SunHaloContribution", nextWeather.Material.GetFloat("_SunHaloContribution"));
        skyMaterial.SetColor("_HorizonLineColor", nextWeather.Material.GetColor("_HorizonLineColor"));
        skyMaterial.SetFloat("_HorizonLineExponent", nextWeather.Material.GetFloat("_HorizonLineExponent"));
        skyMaterial.SetFloat("_HorizonLineContribution", nextWeather.Material.GetFloat("_HorizonLineContribution"));
        skyMaterial.SetColor("_SkyGradientTop", nextWeather.Material.GetColor("_SkyGradientTop"));
        skyMaterial.SetColor("_SkyGradientBottom", nextWeather.Material.GetColor("_SkyGradientBottom"));
        skyMaterial.SetFloat("_SkyGradientExponent", nextWeather.Material.GetFloat("_SkyGradientExponent"));

        isUpdating = false;
    }

    private Momentum momentum;

    public Momentum Momentum
    {
        get { return momentum; }
        set
        {
            lastMomentum = momentum;
            momentum = value;
            onMomentumChange.Invoke();
        }
    }

}
