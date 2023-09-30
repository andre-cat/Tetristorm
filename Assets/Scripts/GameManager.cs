using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator crossFadeAnim;
    public AnimationClip crossfadeAnimClip;
    public int sceneNumber;
    AudioManager audioManager;
    // Start is called before the first frame update
    //[SerializeField] private Momentum momentum;
    [SerializeField] private Board board;
    [SerializeField] private WeatherManager weatherManager;

    private int currentLevel;

    void Start()
    {
        crossFadeAnim = GameObject.Find("PanelForCrossfade").GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Wrapper();
        }

        ChangeMusicForScene();
        ChangeWeather();
        //ChangeWeatherTest();
    }

    public IEnumerator ChangeScene()
    {
        crossFadeAnim.SetBool("SceneCompleted", true);
        yield return new WaitForSeconds(crossfadeAnimClip.length + 1f); // used 1 seconds instead of crossfadeAnimClip.length bc it wasn't working properly idk why. (I know the length is 1 seconds bc the inspector)
        SceneManager.LoadScene(sceneNumber + 1);
    }

    public void Wrapper()
    {
        StartCoroutine(ChangeScene());
    }

    public void ChangeMusicForScene()
    {
        if (audioManager != null)
        {
            if (sceneNumber == 0)
            {
                audioManager.PlayMenuMusic();
            }
            if (sceneNumber == 1)
            {
                audioManager.PlayLevelMusic();
            }
            if (sceneNumber == -1)
            {
                audioManager.PlayGameOverMusic();
            }
        }
    }

    public void ChangeWeather()
    {
        if (currentLevel != board.levelReached)
        {
            switch (board.levelReached)
            {
                case 0:
                    Debug.Log("0");
                    weatherManager.Momentum = Momentum.Sunny;
                    break;
                case 1:
                    Debug.Log("1");
                    weatherManager.Momentum = Momentum.Cloudy;
                    break;
                case 2:
                    Debug.Log("2");
                    weatherManager.Momentum = Momentum.Rainy;
                    break;
                case 3:
                    Debug.Log("3");
                    weatherManager.Momentum = Momentum.Stormy;
                    break;
                case 4:
                    Debug.Log("4");
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
