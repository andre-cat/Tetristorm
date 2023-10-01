using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Weather weather;

    [Header("WEATHER CHANGE")]
    [SerializeField] private bool changeByMomentum = false;
    [SerializeField] private Momentum momentum = Momentum.Sunny;

    private int currentLevel;

    private void Start()
    {
        currentLevel = 0;
    }

    private void Update()
    {
        if (changeByMomentum)
        {
            SetWeatherByMomentum();
        }
        else
        {
            SetWeatherByScore();
        }
    }

    public void SetWeatherByScore()
    {
        if (currentLevel != Board.levelReached)
        {
            currentLevel = Board.levelReached;
            Debug.Log(Board.levelReached);
            switch (Board.levelReached)
            {
                case 0:
                    weather.Momentum = Momentum.Sunny;
                    break;
                case 1:
                    weather.Momentum = Momentum.Cloudy;
                    break;
                case 2:
                    weather.Momentum = Momentum.Rainy;
                    break;
                case 3:
                    weather.Momentum = Momentum.Stormy;
                    break;
                case 4:
                    GameManager.gameOver = true;
                    Debug.Log("GAME OVER");
                    break;
            }
        }
    }

    public void SetWeatherByMomentum()
    {
        if (momentum != weather.Momentum)
        {
            weather.Momentum = momentum;
            momentum = weather.Momentum;
        }
    }
}
