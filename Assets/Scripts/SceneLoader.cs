using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private Image cover;
    [SerializeField] private string crossfadeTrigger;
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private float lastDelay;

    private Button button;

    public void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(GoToScene);
    }

    public void GoToScene()
    {
        StartCoroutine(GoToScene(scene));
    }

    private IEnumerator GoToScene(string sceneName)
    {
        cover.gameObject.GetComponent<Animator>().SetTrigger(crossfadeTrigger);
        yield return new WaitForSecondsRealtime(animationClip.length + lastDelay);
        SceneManager.LoadScene(sceneName);
    }

}
