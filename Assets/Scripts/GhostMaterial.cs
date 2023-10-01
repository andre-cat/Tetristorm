using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMaterial : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float seconds;

    private void OnEnable()
    {
        StartCoroutine(ChangeColor(material, 0, 1, seconds));
    }

    public void Disappear()
    {
        StartCoroutine(ChangeColor(material, 1, 0, seconds));
        gameObject.SetActive(false);
    }

    private IEnumerator ChangeColor(Material material, float initAlpha, float endAlpha, float seconds)
    {

        float secondsElapsed = 0;

        while (secondsElapsed < seconds)
        {
            float a = Mathf.Lerp(initAlpha, endAlpha, secondsElapsed / seconds);
            material.color = new Color(material.color.r, material.color.g, material.color.b, a);
            secondsElapsed += Time.deltaTime;
            yield return null;
        }

        material.color = new Color(material.color.r, material.color.g, material.color.b, endAlpha);
    }
}
