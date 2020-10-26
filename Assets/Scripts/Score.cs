using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int ScoreIndex = 0;
    [SerializeField] private int ScoreDigit = 5;
    [SerializeField] private int ScoreMax = 99999;
    [SerializeField] private Sprite[] Sprites = null;
    [SerializeField] private Image[] Images = null;

    static public int score { get; private set; }
    static public int scoreIndex { get; private set; }
    static public int scoreDigit { get; private set; }
    static public int scoreMax { get; private set; }
    static private Sprite[] sprites { get; set; }
    static private Image[] images { get; set; }

    private void Awake()
    {
        score = 0;
        scoreIndex = ScoreIndex;
        scoreDigit = ScoreDigit;
        scoreMax = ScoreMax;

        sprites = Sprites;
        images = Images;

        images[0].sprite = sprites[0];

        for (int i = 1; i < images.Length; i++)
        {
            images[i].sprite = sprites[sprites.Length - 1];
            //images[i].sprite = sprites[0];
        }
    }

    static public void SetScore(float s)
    {
        s *= scoreIndex;

        if (score < (int)s)
        {
            if ((int)s >= scoreMax)
            {
                score = scoreMax;
            }
            else
            {
                score = (int)s;
            }
            
            ScoreDigitCheck();
        }
    }

    static private void ScoreDigitCheck()
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
