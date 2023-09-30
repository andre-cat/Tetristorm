using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    //[SerializeField] private Momentum momentum;
    [SerializeField] private Board board;
    [SerializeField] private Weather weather;

    private int currentLevel;

    private void Start()
    {
        currentLevel = 0;
    }

    private void Update()
    {
        SetWeatherByScore();
        //ChangeWeatherTest();
    }

    public void SetWeatherByScore()
    {
        if (currentLevel != board.levelReached)
        {
            Debug.Log(board.levelReached);
            switch (board.levelReached)
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
                    Debug.Log("GameOver");
                    break;
            }
            currentLevel = board.levelReached;
        }
    }

    /*
    public void ChangeWeatherTest()
    {
        if (momentum != weatherManager.Momentum)
        {
            weatherManager.Momentum = momentum;
            momentum = weatherManager.Momentum;
        }
    }
    */
}
