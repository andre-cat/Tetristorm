using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private float transitionSeconds;
    [SerializeField] private GameObject cover;
    [SerializeField] private string crossfadeTrigger;

    private Button button;

    public void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(GoToScene);
    }

    public void GoToScene()
    {
        StartCoroutine(GoToScene(scene, transitionSeconds));
    }

    private IEnumerator GoToScene(string sceneName, float transitionSeconds)
    {
        cover.GetComponent<Animator>().SetTrigger(crossfadeTrigger);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSecondsRealtime(transitionSeconds);
    }

}
