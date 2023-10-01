using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private static AudioSource sfxAudioSource;
    [SerializeField] private static AudioSource sfxTetrisAudioSource;
    [Header("SFX Thunder")]
    [SerializeField] private AudioClip[] thunderSFX;
    [Space(5)]
    [SerializeField] private AudioClip[] tetrisSFX;
    public int i = 0;
    public int index;

    [SerializeField] private Weather weather;

    void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();//canvasinnit
        sfxTetrisAudioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();//for board
        InvokeRepeating("SFXRepeating", 5f, 12f);
    }

    private void Update()
    {

    }

    void SFXRepeating()
    {
        i = Random.Range(0, thunderSFX.Length);
        sfxAudioSource.clip = thunderSFX[i];
        sfxAudioSource.volume = Volume;

        if (SceneManager.GetActiveScene().name != "Game" || (SceneManager.GetActiveScene().name != "Game" && (int)weather.Momentum > (int)Momentum.Cloudy))
        {
            sfxAudioSource.Play();
        }
    }

    public void SFXTetris()
    {
        index = Random.Range(0, tetrisSFX.Length);
        sfxAudioSource.volume = Volume;
        sfxAudioSource.PlayOneShot(tetrisSFX[index]);
    }

    private static readonly string VOLUME = "volume";
    public static float Volume
    {
        get { return PlayerPrefs.GetFloat(VOLUME, 0.1f); }
        set { PlayerPrefs.SetFloat(VOLUME, value); sfxTetrisAudioSource.volume = PlayerPrefs.GetFloat(VOLUME, 0.1f); }
    }
}
