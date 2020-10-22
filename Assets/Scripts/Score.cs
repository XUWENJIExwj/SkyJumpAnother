using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public int scoreIndex;
    public int scoreDigit;
    public int ScoreMax;

    public Sprite[] sprites;
    private Image[] images;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        images = GetComponentsInChildren<Image>();
       
        images[0].sprite = sprites[0];

        for (int i = 1; i < images.Length; i++)
        {
            images[i].sprite = sprites[sprites.Length - 1];
        }
    }

    public void SetScore(float s)
    {
        s *= scoreIndex;

        if (score < (int)s)
        {
            if ((int)s >= ScoreMax)
            {
                score = ScoreMax;
            }
            else
            {
                score = (int)s;
            }
            
            ScoreDigitCheck();
        }
    }

    private void ScoreDigitCheck()
    {
        bool isDisplay = false;
        int workscore = score;

        for (int i = 0; i < scoreDigit; i++)
        {
            // 最大の位の値を取り出す
            int work = workscore / (int)Mathf.Pow(10, scoreDigit - 1 - i);

            if (work == 0 && !isDisplay)
            {
                images[scoreDigit - 1 - i].sprite = sprites[sprites.Length - 1];
            }
            else
            {
                images[scoreDigit - 1 - i].sprite = sprites[work];
                isDisplay = true;
            }

            // 最大の位の値を取り除く
            workscore -= work * (int)Mathf.Pow(10, scoreDigit - 1 - i);
        }
    }
}
