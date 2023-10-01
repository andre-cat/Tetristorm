using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int rows;
    int level = 1;

    public static int hopeLevel = 1;

    public int linesPerLevel = 5;
    public float fadeTimer = 0.2f;

    const int MIN_LINES = 1;
    const int MAX_LINES = 5;

    public TextMeshProUGUI hopePoints;
    public TextMeshProUGUI textMultiplier;
    public TextMeshProUGUI UiHopeLevel;
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreMultiplier(int n)
    {

        n = Mathf.Clamp(n, MIN_LINES, MAX_LINES);

        switch (n)
        {
            case 1:
                score += 10 * level;
                    break;
            case 2:
                score += 80 * level;
                StartCoroutine("SetMultiplierMessage", "Yeah x2");
                break;
            case 3:
                score += 250 * level;
                StartCoroutine("SetMultiplierMessage", "gross x3");
                break;
            case 4:
                StartCoroutine("SetMultiplierMessage", "Holly molly x4");
                score += 600 * level;
                break;
        }

        if(score >= 50 && score < 80)
        {
            hopeLevel = 2;
        }else if(score >= 80)
        {
            hopeLevel = 3;
        }

        UpdateUIText();
    }

    public void Reset()
    {
        level = 1;
        rows = linesPerLevel + level;
    }

    void UpdateUIText()
    {
        if (hopePoints)
        {
            hopePoints.text ="Hope Points: "+ score.ToString();
        }

        if (UiHopeLevel)
        {
            UiHopeLevel.text = hopeLevel.ToString();
        }
    }


    IEnumerator SetMultiplierMessage(string text)
    {
        textMultiplier.text = text;
        textMultiplier.gameObject.SetActive(true);
        //textMultiplier.color = new Color(1, 1, 1, textMultiplier.color.a - (Time.deltaTime * fadeTimer));
        yield return new WaitForSeconds(0.8f);
        textMultiplier.gameObject.SetActive(false);        

    }

    
}
