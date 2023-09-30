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
    [SerializeField] private WeatherState[] weathers = new WeatherState[WeatherState.Length];
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

        while (secondsElapsed < seconds)
        {

            float skyboxExposure = Mathf.Lerp(lastWeather.SkyboxExposure, nextWeather.SkyboxExposure, secondsElapsed / seconds);
            skyMaterial.SetFloat("_Exposure", skyboxExposure);

            Color skyboxTint = Color.Lerp(lastWeather.SkyboxTint, nextWeather.SkyboxTint, secondsElapsed / seconds);
            skyMaterial.SetColor("_Tint", skyboxTint);

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

        skyMaterial.SetFloat("_Exposure", nextWeather.SkyboxExposure);
        skyMaterial.SetColor("_Tint", nextWeather.SkyboxTint);
        sunLight.intensity = nextWeather.LightIntensity;
        sunLight.color = nextWeather.LightColor;

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
