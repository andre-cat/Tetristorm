using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator crossFadeAnim;
    public AnimationClip crossfadeAnimClip;
    // Start is called before the first frame update
    void Start()
    {
        crossFadeAnim = GameObject.Find("PanelForTransition").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ChangeScene());
        }

    }

    public IEnumerator ChangeScene()
    {
        crossFadeAnim.SetTrigger("SceneEnded");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);

    }
}
