using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    //private
    [SerializeField] int dotsNumber = 0;
    [SerializeField] GameObject dotsParent = null;
    [SerializeField] GameObject dotPrefab = null;
    [SerializeField] float dotSpacing = 0.0f;
    [SerializeField] [Range(0.01f, 0.3f)] float dotMinScale = 0.0f;
    [SerializeField] [Range(0.3f, 1f)] float dotMaxScale = 0.0f;

    [SerializeField] Transform[] dotsList;

    Vector2 pos; //dot pos
    float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        // hide trajectory in the start
        Hide();
        // prepare dots
        PrepareDots();
    }

    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        //Debug.Log(dotsNumber);
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            //you can simlify this 2 lines at the top by:
            //pos = (ballPos+force*time)-((-Physics2D.gravity*time*time)/2f);
            //
            //but make sure to turn "pos" in Ball.cs to Vector2 instead of Vector3	

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }

    public bool GetDotsActive()
    {
        return dotsParent.activeSelf;
    }

    public void SetDotsNumber(int n)
    {
        dotsNumber = n;

        if(dotsList.Length > dotsNumber)
        {
            Destroy(dotsList[dotsNumber].gameObject);
        }
    }
}
