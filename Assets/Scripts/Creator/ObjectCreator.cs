using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectCreator : MonoBehaviour
{
    [SerializeField] protected ObjectWithFlick player = null;
    [SerializeField] protected Trajectory trajectory = null;
    [SerializeField] protected GameObject[] objPrefabs = null;
    [SerializeField] protected GameObject[] objs = null;
    [SerializeField] protected int objNum = 0;
    [SerializeField] protected float heightRangeMin = 0.0f;
    [SerializeField] protected float heightRangeMax = 0.0f;
    [SerializeField] protected float bgDivided = 0.0f;

    protected bool widthOK = true;
    protected bool heightOK = true;

    //[SerializeField] [Range(0.0f, 1.5f)] float heightBlank = 0.0f;
    //public int heightIndex;

    //public Decoration[] decorationObjs;
    //public GameObject[] blockPrefab;
    //private List<GameObject>[] blockObjs;
    //public GameObject[] enemyPrefab;
    //private List<GameObject>[] enemyObjs;
    //public GameObject linePrefab;
    //public List<GameObject>[] lineObjs;
    //public int enemyStepMin;
    //public int enemyStepChange;
    //public int enemyMaxNum;
    //public int enemyEncount;
    //public GameObject[] decorationPrefab;
    //public Score score;
    //public int scoreIndex;
    //public int scoreDigit;

    //public Rigidbody2D playerRb;
    //public ObjectWithFlick playerObjWithFlick;

    //private int oldLineIndex;

    protected virtual void Awake()
    {
        objs = new GameObject[objNum];

        bgDivided = ScreenInfo.bgFixedSizeMatchX.y / objs.Length;
    }

    public abstract GameObject[] CreateObjs(GameObject obj_previous_bg = null);

    protected abstract Vector3 CreateRandomPosition(int height_idx, GameObject obj_previous_bg = null);

    // オブジェクトとの横距離が近すぎるか、もしくは遠すぎるかをチェック
    protected bool CheckIfWidthSpaceOK(float pos_x_a, float pos_x_b, float range_min, float range_max)
    {
        if (Mathf.Abs(pos_x_a - pos_x_b) < range_min || Mathf.Abs(pos_x_a - pos_x_b) > range_max)
        {
            return false;
        }

        return true;
    }

    // オブジェクトとの縦距離が近すぎるかをチェック
    protected bool CheckIfHeightSpaceOK(float pos_y_a, float pos_y_b, float range_min)
    {
        if (Mathf.Abs(pos_y_a - pos_y_b) < range_min)
        {
            return false;
        }

        return true;
    }

    // Start is called before the first frame update
    //void Awake()
    //{


    // Decorationの初期化
    //decorationObjs = new Decoration[bgObjs.Length];
    //decorationObjs[0] = bgObjs[0].GetComponentInChildren<Decoration>();
    //decorationObjs[1] = bgObjs[1].GetComponentInChildren<Decoration>();

    // Blockの初期化
    //blockObjs = new List<GameObject>[2];

    //for (int i = 0; i < blockObjs.Length; i++)
    //{
    //    blockObjs[i] = new List<GameObject>();
    //}

    //// Enemyの初期化
    //enemyObjs = new List<GameObject>[2];

    //for (int i = 0; i < blockObjs.Length; i++)
    //{
    //    enemyObjs[i] = new List<GameObject>();
    //}

    //// Lineの初期化
    //lineObjs = new List<GameObject>[2];

    //for (int i = 0; i < blockObjs.Length; i++)
    //{
    //    lineObjs[i] = new List<GameObject>();
    //}

    //// 二個目のブロックの位置調整
    //float blockX = Random.Range(-halfScreenWidth, halfScreenWidth);

    //// 1.5fは1.5個グリッド線からなるブロックの長さ
    //while (Mathf.Abs(blockX) < 3.0f || Mathf.Abs(blockObjs[0][2].transform.position.x - blockX) < 3.0f)
    //{
    //    blockX = Random.Range(-halfScreenWidth, halfScreenWidth);
    //}

    //blockObjs[0][1].transform.position = new Vector3(
    //    blockX,
    //    blockObjs[0][1].transform.position.y,
    //    blockObjs[0][1].transform.position.z
    //    );

    //blockObjs[0][0].SetActive(false);

    // Line
    //lineObjs[0].Add(Instantiate(linePrefab, new Vector3(0.0f, -halfScreenHeight + 10.0f, -0.2f), Quaternion.identity));
    //lineObjs[0][0].GetComponent<Line>().SetText(10 * scoreIndex);
    //oldLineIndex = 0;


    //score.scoreIndex = scoreIndex;
    //score.scoreDigit = scoreDigit;
    //}

    // Update is called once per frame
    void FixedUpdate()
    {

        //// 次のBGを作る（テクスチャのサイズ）
        //if (screenHeight * step - Camera.main.transform.position.y <= 0.1f && !bgInNextStep)
        //{
        //    int index = (step + 1) % 2;

        //    //DestroyBlock(index);
        //    DestroyGameObjectsInList(blockObjs[index]);
        //    DestroyGameObjectsInList(enemyObjs[index]);
        //    DestroyGameObjectsInList(lineObjs[index]);

        //    CreateBG(index, step + 1);

        //    bgInNextStep = true;
        //}
    }

    void CreateBG(int obj_idx, int step_idx)
    {
        

        //decorationObjs[obj_idx].ResetDocoration(step_idx);

        //CreateBlock(obj_idx, step_idx);
        //CreateEnemy(obj_idx, step_idx);
        //CreateLine(obj_idx,step_idx);
    }

    void CreateEnemy(int obj_idx, int step_idx)
    {
        //if (step_idx >= enemyStepMin)
        //{
        //    int encount = enemyEncount;

        //    for (int i = 0; i < enemyMaxNum; i++)
        //    {
        //        if(Random.Range(0,99) < encount)
        //        {
        //            Vector3 enemyPosition = CreateEnemyRandomPosition(obj_idx, step_idx, i);

        //            int enemyType = 0;

        //            if (step_idx >= enemyStepChange)
        //            {
        //                enemyType = 1;
        //            }

        //            enemyObjs[obj_idx].Add(Instantiate(enemyPrefab[enemyType], enemyPosition, Quaternion.identity));

        //            encount = enemyEncount;
        //        }
        //        else
        //        {
        //            encount += enemyEncount;
        //        }
        //    }
        //}
    }

    void CreateLine(int obj_idx, int step_idx)
    {
        //int line_idx = (int)(step_idx * screenHeight + screenHeight) / 50;

        //if (lineObjs[obj_idx].Count == 0 && line_idx != oldLineIndex)
        //{
        //    Vector3 linePosition = new Vector3(0.0f, -halfScreenHeight + line_idx * 50, -0.2f);

        //    lineObjs[obj_idx].Add(Instantiate(linePrefab, linePosition, Quaternion.identity));
        //    lineObjs[obj_idx][0].GetComponent<Line>().SetText(50 * line_idx * scoreIndex);
        //    oldLineIndex++;
        //}
    }

    void DestroyGameObjectsInList(List<GameObject> listObj)
    {
        foreach (GameObject gameObject in listObj)
        {
            Destroy(gameObject);
        }

        listObj.Clear();
    }

    

    

    //Vector3 CreateEnemyRandomPosition(int obj_idx, int step_idx, int height_idx)
    //{
    //    float fixedScreenHeight = screenHeight - heightBlank;
    //    float fixedHalfScreenHeight = fixedScreenHeight / 2;
    //    float fixedScreenDivided = fixedScreenHeight / enemyMaxNum;
    //    float heightRangeMin = -fixedHalfScreenHeight + height_idx * fixedScreenDivided + screenHeight * step_idx;
    //    float heightRangeMax = heightRangeMin + fixedScreenDivided * 0.75f;

    //    Vector3 position = new Vector3(Random.Range(-halfScreenWidth, halfScreenWidth), Random.Range(heightRangeMin, heightRangeMax), 0.0f);

    //    if (enemyObjs[obj_idx].Count > 0)
    //    {
    //        // 一個前のブロックと縦一直線になる対処
    //        if (Mathf.Abs(enemyObjs[obj_idx][enemyObjs[obj_idx].Count - 1].transform.position.y - position.y) <= 3.0f)
    //        {
    //            position = CreateEnemyRandomPosition(obj_idx, step_idx, height_idx);
    //        }
    //    }
    //    else
    //    {
    //        if (step_idx > enemyStepMin && enemyObjs[(obj_idx + 1) % 2].Count > 0)
    //        {
    //            // 一個前のブロックと縦一直線になる対処
    //            if (Mathf.Abs(enemyObjs[(obj_idx + 1) % 2][enemyObjs[(obj_idx + 1) % 2].Count - 1].transform.position.y - position.y) <= 3.0f)
    //            {
    //                position = CreateEnemyRandomPosition(obj_idx, step_idx, height_idx);
    //            }
    //        }
    //    }

    //    return position;
    //}
}
