using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Cinematic : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Material skyMaterial;
    [SerializeField] private GameObject returnPanel;

    [Header("HOPE 1")]
    [SerializeField] private GameObject deadScene;
    [SerializeField] private Material deadMaterial;

    [Header("HOPE 2")]
    [SerializeField] private GameObject dutyScene;
    [SerializeField] private Material dutyMaterial;

    [Header("HOPE 3")]
    [SerializeField] private GameObject hopeScene;
    [SerializeField] private Material fakeSky;
    [SerializeField] private Material hopeMaterial;
    [SerializeField] private Material hopelessMaterial;

    void Start()
    {
        StartLevel(ScoreManager.hopeLevel);
        //StartLevel(3);
    }

    private void StartLevel(int hopeLevel)
    {
        switch (hopeLevel)
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
                StartCoroutine(ChangeStyledMaterial(hopelessMaterial, hopeMaterial, 10));
                hopeScene.SetActive(true);
                break;
        }
    }

    private IEnumerator ChangeStyledMaterial(Material iniMaterial, Material endMaterial, float seconds)
    {
        float secondsElapsed = 0;

        Color sunDiscColor;
        float sunDiscMultiplier;
        float sunDiscExponent;
        Color sunHaloColor;
        float sunHaloExponent;
        float sunHaloContribution;
        Color horizonLineColor;
        float horizonLineExponent;
        float horizonLineContribution;
        Color skyGradientTop;
        Color skyGradientBottom;
        float skyGradientExponent;

        while (secondsElapsed < seconds)
        {
            float t = secondsElapsed / seconds;

            sunDiscColor = Color.Lerp(iniMaterial.GetColor("_SunDiscColor"), endMaterial.GetColor("_SunDiscColor"), t);
            skyMaterial.SetColor("_SunDiscColor", sunDiscColor);

            sunDiscMultiplier = Mathf.Lerp(iniMaterial.GetFloat("_SunDiscMultiplier"), endMaterial.GetFloat("_SunDiscMultiplier"), t);
            skyMaterial.SetFloat("_SunDiscMultiplier", sunDiscMultiplier);

            sunDiscExponent = Mathf.Lerp(iniMaterial.GetFloat("_SunDiscExponent"), endMaterial.GetFloat("_SunDiscExponent"), t);
            skyMaterial.SetFloat("_SunDiscExponent", sunDiscExponent);

            sunHaloColor = Color.Lerp(iniMaterial.GetColor("_SunHaloColor"), endMaterial.GetColor("_SunHaloColor"), t);
            skyMaterial.SetColor("_SunHaloColor", sunHaloColor);

            sunHaloExponent = Mathf.Lerp(iniMaterial.GetFloat("_SunHaloExponent"), endMaterial.GetFloat("_SunHaloExponent"), t);
            skyMaterial.SetFloat("_SunHaloExponent", sunHaloExponent);

            sunHaloContribution = Mathf.Lerp(iniMaterial.GetFloat("_SunHaloContribution"), endMaterial.GetFloat("_SunHaloContribution"), t);
            skyMaterial.SetFloat("_SunHaloContribution", sunHaloContribution);

            horizonLineColor = Color.Lerp(iniMaterial.GetColor("_HorizonLineColor"), endMaterial.GetColor("_HorizonLineColor"), t);
            skyMaterial.SetColor("_HorizonLineColor", horizonLineColor);

            horizonLineExponent = Mathf.Lerp(iniMaterial.GetFloat("_HorizonLineExponent"), endMaterial.GetFloat("_HorizonLineExponent"), t);
            skyMaterial.SetFloat("_HorizonLineExponent", horizonLineExponent);

            horizonLineContribution = Mathf.Lerp(iniMaterial.GetFloat("_HorizonLineContribution"), endMaterial.GetFloat("_HorizonLineContribution"), t);
            skyMaterial.SetFloat("_HorizonLineContribution", horizonLineContribution);

            skyGradientTop = Color.Lerp(iniMaterial.GetColor("_SkyGradientTop"), endMaterial.GetColor("_SkyGradientTop"), t);
            skyMaterial.SetColor("_SkyGradientTop", skyGradientTop);

            skyGradientBottom = Color.Lerp(iniMaterial.GetColor("_SkyGradientBottom"), endMaterial.GetColor("_SkyGradientBottom"), t);
            skyMaterial.SetColor("_SkyGradientBottom", skyGradientBottom);

            skyGradientExponent = Mathf.Lerp(iniMaterial.GetFloat("_SkyGradientExponent"), endMaterial.GetFloat("_SkyGradientExponent"), t);
            skyMaterial.SetFloat("_SkyGradientExponent", skyGradientExponent);

            secondsElapsed += Time.deltaTime;

            yield return null;
        }

        skyMaterial.SetColor("_SunDiscColor", endMaterial.GetColor("_SunDiscColor"));
        skyMaterial.SetFloat("_SunDiscMultiplier", endMaterial.GetFloat("_SunDiscMultiplier"));
        skyMaterial.SetFloat("_SunDiscExponent", endMaterial.GetFloat("_SunDiscExponent"));
        skyMaterial.SetColor("_SunHaloColor", endMaterial.GetColor("_SunHaloColor"));
        skyMaterial.SetFloat("_SunHaloExponent", endMaterial.GetFloat("_SunHaloExponent"));
        skyMaterial.SetFloat("_SunHaloContribution", endMaterial.GetFloat("_SunHaloContribution"));
        skyMaterial.SetColor("_HorizonLineColor", endMaterial.GetColor("_HorizonLineColor"));
        skyMaterial.SetFloat("_HorizonLineExponent", endMaterial.GetFloat("_HorizonLineExponent"));
        skyMaterial.SetFloat("_HorizonLineContribution", endMaterial.GetFloat("_HorizonLineContribution"));
        skyMaterial.SetColor("_SkyGradientTop", endMaterial.GetColor("_SkyGradientTop"));
        skyMaterial.SetColor("_SkyGradientBottom", endMaterial.GetColor("_SkyGradientBottom"));
        skyMaterial.SetFloat("_SkyGradientExponent", endMaterial.GetFloat("_SkyGradientExponent"));

        returnPanel.SetActive(true);
    }

}
