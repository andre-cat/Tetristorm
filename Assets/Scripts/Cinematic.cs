using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Cinematic : MonoBehaviour
{

    [SerializeField] private Material skyMaterial;

    [Header("HOPE 1")]
    [SerializeField] private GameObject deadScene;
    [SerializeField] private Material deadMaterial;

    [Header("HOPE 2")]
    [SerializeField] private GameObject dutyScene;
    [SerializeField] private Material dutyMaterial;

    [Header("HOPE 3")]
    [SerializeField] private GameObject hopeScene;
    [SerializeField] private Material hopeMaterial;

    void Start()
    {
        switch (ScoreManager.hopeLevel)
        {
            case 1:
                skyMaterial = deadMaterial;
                deadScene.SetActive(true);
                break;
            case 2:
                skyMaterial = dutyMaterial;
                dutyScene.SetActive(true);
                break;
            case 3:
                skyMaterial = hopeMaterial;
                hopeScene.SetActive(true);
                break;
        }
    }
}
