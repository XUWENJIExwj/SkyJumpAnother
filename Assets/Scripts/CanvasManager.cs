using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject score;
    private RectTransform scoreRectTransform;
    public GameObject scoreBest;
    private RectTransform scoreBestRectTransform;

    // Start is called before the first frame update
    void Awake()
    {
        if(score)
        {
            scoreRectTransform = score.GetComponent<RectTransform>();
        }

        if (scoreBest)
        {
            scoreBestRectTransform = scoreBest.GetComponent<RectTransform>();
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetScorePosition(float pos_x, float pos_y)
    {
        if(score)
        {
            scoreRectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
        else
        {
            score = GameObject.FindGameObjectWithTag("Score");
            scoreRectTransform = score.GetComponent<RectTransform>();
            scoreRectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
    }

    public void SetScoreSize(float width, float height)
    {
        score.transform.localScale = new Vector3(width, height, 1.0f);
    }

    public void SetScoreBestPosition(float pos_x, float pos_y)
    {
        if (scoreBest)
        {
            scoreBestRectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
        else
        {
            scoreBest = GameObject.FindGameObjectWithTag("ScoreBest");
            scoreBestRectTransform = score.GetComponent<RectTransform>();
            scoreBestRectTransform.anchoredPosition = new Vector2(pos_x, pos_y);
        }
    }

    public void SetScoreBestSize(float width, float height)
    {
        scoreBest.transform.localScale = new Vector3(width, height, 1.0f);
    }
}
