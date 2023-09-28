using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuClip_;
    [SerializeField] private AudioClip levelClip_;
    [SerializeField] private AudioClip winClip_;

    private static AudioClip menuClip;
    private static AudioClip levelClip;
    private static AudioClip winClip;

    private static AudioSource audioSource;
    public GameManager gameManager;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            StartComponents();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource.Play();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void StartComponents()
    {
        levelClip = levelClip_;
        menuClip = menuClip_;
        winClip = winClip_;

        audioSource = gameObject.GetComponent<AudioSource>();
        
        audioSource.volume = Volume;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.clip = menuClip;
    }


    public void PlayMenuMusic()
    {
        if (!audioSource.clip.Equals(menuClip))
        {
            audioSource.clip = menuClip;
            audioSource.Play();
        }
    }

    public void PlayLevelMusic()
    {
        if (!audioSource.clip.Equals(levelClip))
        {
            audioSource.clip = levelClip;
            audioSource.Play();
        }
    }

    public void PlayGameOverMusic()
    {
        if (!audioSource.clip.Equals(winClip))
        {
            audioSource.clip = winClip;
            audioSource.Play();
        }
    }

    #region attributes

    private static AudioManager instance; // = null;

    public static float crossfadeSeconds = 0.5f;

    private static readonly string VOLUME = "volume";
    private static readonly string LEVEL = "level";

    public static float Volume
    {
        get { return PlayerPrefs.GetFloat(VOLUME, 0.1f); }
        set { PlayerPrefs.SetFloat(VOLUME, value); audioSource.volume = PlayerPrefs.GetFloat(VOLUME, 0.1f); }
    }

    #endregion attributes



}