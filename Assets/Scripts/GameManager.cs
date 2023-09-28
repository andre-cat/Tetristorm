using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator crossFadeAnim;
    public AnimationClip crossfadeAnimClip;
    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        crossFadeAnim = GameObject.Find("Cover").GetComponent<Animator>();
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
        yield return new WaitForSeconds(1); // used 1 seconds instead of crossfadeAnimClip.length bc it wasn't working properly idk why. (I know the length is 1 seconds bc the inspector)
        SceneManager.LoadScene(sceneNumber+1);

    }
}
