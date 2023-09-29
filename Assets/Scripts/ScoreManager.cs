using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int rows;
    int level = 1;

    public int linesPerLevel = 5;

    const int MIN_LINES = 1;
    const int MAX_LINES = 5;

    public TextMeshProUGUI hopePoints;
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
                break;
            case 3:
                score += 250 * level;
                break;
            case 4:
                score += 600 * level;
                break;
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
    }

    
}
