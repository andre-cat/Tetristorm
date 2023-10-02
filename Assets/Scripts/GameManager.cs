using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator crossFadeAnim;
    public AnimationClip crossfadeAnimClip;
    public int sceneNumber;
    public AudioManager audioManager;

    public bool isPaused = false;
    public GameObject pausePanel;

    public static bool gameOver;

    void Start()
    {
        crossFadeAnim = GameObject.Find("PanelForCrossfade").GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }

        gameOver = false;
    }

    void Update()
    {
        if (gameOver)
        {
            StartCoroutine(nameof(GameOver));
            gameOver = false;
        }


        ChangeMusicForScene();

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePausePanel();
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

    public void TogglePausePanel()
    {
        isPaused = !isPaused;
        if (pausePanel)
        {
            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    public void GoToMainMenu()
    {
        StartCoroutine(nameof(MainMenu));
    }

    public IEnumerator MainMenu()
    {
        crossFadeAnim.SetBool("SceneCompleted", true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(crossfadeAnimClip.length + 0.5f); // used 1 seconds instead of crossfadeAnimClip.length bc it wasn't working properly idk why. (I know the length is 1 seconds bc the inspector)
        SceneManager.LoadScene(0);
    }

    public IEnumerator GameOver()
    {
        crossFadeAnim.SetBool("SceneCompleted", true);
        Time.timeScale = 1;
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSeconds(100); // used 1 seconds instead of crossfadeAnimClip.length bc it wasn't working properly idk why. (I know the length is 1 seconds bc the inspector)
        //audioManager.PlayGameOverMusic();
    }

}
