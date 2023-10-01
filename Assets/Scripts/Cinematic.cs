using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Cinematic : MonoBehaviour
{


    [SerializeField] private Material skyMaterial;
    [SerializeField] private Material deadMaterial;
    [SerializeField] private Material dutyMaterial;
    [SerializeField] private Material hopeMaterial;

    void Start()
    {
        switch (ScoreManager.hopeLevel)
        {
            case 1:
                skyMaterial = deadMaterial;
                break;
            case 2:
                skyMaterial = dutyMaterial;
                break;
            case 3:
                skyMaterial = hopeMaterial;
                break;
        }
    }
}
