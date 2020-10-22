using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public float screenWidth;
    public float halfScreenWidth;
    public float screenHeight;
    public float halfScreenHeight;
    [SerializeField] [Range(0.0f, 1.5f)] float heightBlank = 0.0f;
    public int heightIndex;

    public Trajectory trajectory;
    public GameObject plane;
    public GameObject bgPrefab;
    public Sprite[] bgSprites;
    public GameObject[] bgObjs;
    public SpriteRenderer[] bgSpriteRenderers;
    public Decoration[] decorationObjs;
    public GameObject[] blockPrefab;
    private List<GameObject>[] blockObjs;
    public GameObject[] enemyPrefab;
    private List<GameObject>[] enemyObjs;
    public GameObject linePrefab;
    public List<GameObject>[] lineObjs;
    public int enemyStepMin;
    public int enemyStepChange;
    public int enemyMaxNum;
    public int enemyEncount;
    public GameObject[] decorationPrefab;
    public Score score;
    public int scoreIndex;
    public int scoreDigit;

    public Rigidbody2D playerRb;
    public ObjectWithFlick playerObjWithFlick;

    private int oldStep;
    public int step;
    public int dotsNumberMin;
    public int dotsNumberMax;
    public int dotsNumber;
    public int stepDotsIndex;

    public AudioManager audioManager;

    private int oldLineIndex;

    bool bgInNextStep;

    // Start is called before the first frame update
    void Awake()
    {
        screenWidth = (float)Screen.width / 100;
        halfScreenWidth = screenWidth / 2;
        screenHeight = (float)Screen.height / 100;
        halfScreenHeight = screenHeight / 2;

        plane.transform.position = new Vector3(0.0f, -halfScreenHeight + 0.3f, 0.0f);
        plane.transform.localScale = new Vector3(screenWidth, 1.0f, 1.0f);

        // Bgの初期化
        bgObjs = new GameObject[2];

        bgObjs[0] = Instantiate(bgPrefab, new Vector3(0.0f, screenHeight * 0, 1.0f), Quaternion.identity);
        bgObjs[1] = Instantiate(bgPrefab, new Vector3(0.0f, screenHeight * 1, 1.0f), Quaternion.identity);

        bgSpriteRenderers = new SpriteRenderer[2];
        bgSpriteRenderers[0] = bgObjs[0].GetComponent<SpriteRenderer>();
        bgSpriteRenderers[1] = bgObjs[1].GetComponent<SpriteRenderer>();

        // Decorationの初期化
        decorationObjs = new Decoration[bgObjs.Length];
        decorationObjs[0] = bgObjs[0].GetComponentInChildren<Decoration>();
        decorationObjs[1] = bgObjs[1].GetComponentInChildren<Decoration>();

        // Blockの初期化
        blockObjs = new List<GameObject>[2];

        for (int i = 0; i < blockObjs.Length; i++)
        {
            blockObjs[i] = new List<GameObject>();
        }

        // Enemyの初期化
        enemyObjs = new List<GameObject>[2];

        for (int i = 0; i < blockObjs.Length; i++)
        {
            enemyObjs[i] = new List<GameObject>();
        }

        // Lineの初期化
        lineObjs = new List<GameObject>[2];

        for (int i = 0; i < blockObjs.Length; i++)
        {
            lineObjs[i] = new List<GameObject>();
        }

        step = 0;

        // Player初期位置の設定
        RectTransform playerRectTransform = playerRb.gameObject.GetComponent<RectTransform>();

        playerRectTransform.position = new Vector3(
            0.0f,
            -halfScreenHeight + playerRectTransform.sizeDelta.y / 2 * playerRectTransform.localScale.y + 1.1f,
            //plane.transform.position.y + plane.transform.localScale.y / 2 + playerRectTransform.sizeDelta.y / 2 * playerRectTransform.localScale.y,
            -5.0f);

        CreateBG(0, 0);

        // 二個目のブロックの位置調整
        float blockX = Random.Range(-halfScreenWidth, halfScreenWidth);

        // 1.5fは1.5個グリッド線からなるブロックの長さ
        while (Mathf.Abs(blockX) < 3.0f || Mathf.Abs(blockObjs[0][2].transform.position.x - blockX) < 3.0f)
        {
            blockX = Random.Range(-halfScreenWidth, halfScreenWidth);
        }

        blockObjs[0][1].transform.position = new Vector3(
            blockX,
            blockObjs[0][1].transform.position.y,
            blockObjs[0][1].transform.position.z
            );

        blockObjs[0][0].SetActive(false);

        lineObjs[0].Add(Instantiate(linePrefab, new Vector3(0.0f, -halfScreenHeight + 10.0f, -0.2f), Quaternion.identity));
        lineObjs[0][0].GetComponent<Line>().SetText(10 * scoreIndex);
        oldLineIndex = 0;

        dotsNumber = dotsNumberMax;
        trajectory.SetDotsNumber(dotsNumberMax);

        bgInNextStep = false;

        score.scoreIndex = scoreIndex;
        score.scoreDigit = scoreDigit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // oldStepの記録
        oldStep = step;

        // 次のstepに入るかをチェック
        if (halfScreenHeight + screenHeight * step < Camera.main.transform.position.y)
        {
            step++;
            bgInNextStep = false;
        }

        // 次のBGを作る（テクスチャのサイズ）
        if (screenHeight * step - Camera.main.transform.position.y <= 0.1f && !bgInNextStep)
        {
            int index = (step + 1) % 2;

            //DestroyBlock(index);
            DestroyGameObjectsInList(blockObjs[index]);
            DestroyGameObjectsInList(enemyObjs[index]);
            DestroyGameObjectsInList(lineObjs[index]);

            CreateBG(index, step + 1);

            bgInNextStep = true;
        }

        // 予測線の長さの更新
        if (oldStep != step)
        {
            UpdateDotsNumber();
        }
    }

    void CreateBG(int obj_idx, int step_idx)
    {
        bgObjs[obj_idx].transform.position = new Vector3(0.0f, screenHeight * step_idx, 1.0f);

        int sprite_index = step_idx;

        if(sprite_index >= bgSprites.Length)
        {
            sprite_index = bgSprites.Length - 1;
        }
        bgSpriteRenderers[obj_idx].sprite = bgSprites[sprite_index];

        // Spriteの設定
        bgSpriteRenderers[obj_idx].size = new Vector2(screenWidth, screenHeight);

        decorationObjs[obj_idx].ResetDocoration(step_idx);

        CreateBlock(obj_idx, step_idx);
        CreateEnemy(obj_idx, step_idx);
        CreateLine(obj_idx,step_idx);
    }

    void CreateBlock(int obj_idx, int step_idx)
    {
        for (int i = 0; i < heightIndex; i++)
        {
            Vector3 blockPosition = CreateBlockRandomPosition(obj_idx, step_idx, i);

            int blockType = Random.Range(0, 99) % blockPrefab.Length;

            blockObjs[obj_idx].Add(Instantiate(blockPrefab[blockType], blockPosition, Quaternion.identity));

            BlockNormal blockScript = blockObjs[obj_idx][i].GetComponent<BlockNormal>();

            blockScript.playerRb = playerRb;
            blockScript.playerObjWithFlick = playerObjWithFlick;
            blockScript.score = score;
            blockScript.trajectory = trajectory;
        }
    }

    void CreateEnemy(int obj_idx, int step_idx)
    {
        if (step_idx >= enemyStepMin)
        {
            int encount = enemyEncount;

            for (int i = 0; i < enemyMaxNum; i++)
            {
                if(Random.Range(0,99) < encount)
                {
                    Vector3 enemyPosition = CreateEnemyRandomPosition(obj_idx, step_idx, i);

                    int enemyType = 0;

                    if (step_idx >= enemyStepChange)
                    {
                        enemyType = 1;
                    }

                    enemyObjs[obj_idx].Add(Instantiate(enemyPrefab[enemyType], enemyPosition, Quaternion.identity));

                    encount = enemyEncount;
                }
                else
                {
                    encount += enemyEncount;
                }
            }
        }
    }

    void CreateLine(int obj_idx, int step_idx)
    {
        int line_idx = (int)(step_idx * screenHeight + screenHeight) / 50;

        if (lineObjs[obj_idx].Count == 0 && line_idx != oldLineIndex)
        {
            Vector3 linePosition = new Vector3(0.0f, -halfScreenHeight + line_idx * 50, -0.2f);

            lineObjs[obj_idx].Add(Instantiate(linePrefab, linePosition, Quaternion.identity));
            lineObjs[obj_idx][0].GetComponent<Line>().SetText(50 * line_idx * scoreIndex);
            oldLineIndex++;
        }
    }

    void DestroyGameObjectsInList(List<GameObject> listObj)
    {
        foreach (GameObject gameObject in listObj)
        {
            Destroy(gameObject);
        }

        listObj.Clear();
    }

    void UpdateDotsNumber()
    {
        if (step % stepDotsIndex == 0 && dotsNumber > dotsNumberMin)
        {
            dotsNumber = dotsNumberMax - step / stepDotsIndex;
            trajectory.SetDotsNumber(dotsNumber);
        }
    }

    Vector3 CreateBlockRandomPosition(int obj_idx, int step_idx, int height_idx)
    {
        float screenHalfWidth = screenWidth / 2;
        float blockSpaceWidth = screenWidth * 0.75f;
        float fixedScreenHeight = screenHeight - heightBlank;
        float fixedHalfScreenHeight = fixedScreenHeight / 2;
        float fixedScreenDivided = fixedScreenHeight / heightIndex;
        float heightRangeMin = -fixedHalfScreenHeight + height_idx * fixedScreenDivided + screenHeight * step_idx;
        float heightRangeMax = heightRangeMin + fixedScreenDivided / 2;

        bool width = true;
        bool height = true;

        Vector3 position = new Vector3(Random.Range(-halfScreenWidth, halfScreenWidth), Random.Range(heightRangeMin, heightRangeMax), -1.0f);

        if (blockObjs[obj_idx].Count > 0)
        {
            width = false;
            height = false;

            // 一個前のブロックと縦一直線になる対処
            if (Mathf.Abs(blockObjs[obj_idx][blockObjs[obj_idx].Count - 1].transform.position.x - position.x) < 3.0f ||
                Mathf.Abs(blockObjs[obj_idx][blockObjs[obj_idx].Count - 1].transform.position.x - position.x) > blockSpaceWidth)
            {
                width = false;
            }
            else
            {
                width = true;
            }

            // 一個前のブロックと縦距離が短すぎる対処
            if (Mathf.Abs(blockObjs[obj_idx][blockObjs[obj_idx].Count - 1].transform.position.y - position.y) < 1.8f)
            {
                height = false;
            }
            else
            {
                height = true;
            }
        }
        else
        {
            if (step_idx > 0)
            {
                width = false;
                height = false;

                // 一個前のブロックと縦一直線になる対処
                if (Mathf.Abs(blockObjs[(obj_idx + 1) % 2][blockObjs[(obj_idx + 1) % 2].Count - 1].transform.position.x - position.x) < 3.0f ||
                    Mathf.Abs(blockObjs[(obj_idx + 1) % 2][blockObjs[(obj_idx + 1) % 2].Count - 1].transform.position.x - position.x) > blockSpaceWidth)
                {
                    width = false;
                }
                else
                {
                    width = true;
                }

                // 一個前のブロックと縦距離が短すぎる対処
                if (Mathf.Abs(blockObjs[(obj_idx + 1) % 2][blockObjs[(obj_idx + 1) % 2].Count - 1].transform.position.y - position.y) <= 1.8f)
                {
                    height = false;
                }
                else
                {
                    height = true;
                }
            }
        }

        if (!width || !height)
        {
            position = CreateBlockRandomPosition(obj_idx, step_idx, height_idx);
        }
        return position;
    }

    Vector3 CreateEnemyRandomPosition(int obj_idx, int step_idx, int height_idx)
    {
        float fixedScreenHeight = screenHeight - heightBlank;
        float fixedHalfScreenHeight = fixedScreenHeight / 2;
        float fixedScreenDivided = fixedScreenHeight / enemyMaxNum;
        float heightRangeMin = -fixedHalfScreenHeight + height_idx * fixedScreenDivided + screenHeight * step_idx;
        float heightRangeMax = heightRangeMin + fixedScreenDivided * 0.75f;

        Vector3 position = new Vector3(Random.Range(-halfScreenWidth, halfScreenWidth), Random.Range(heightRangeMin, heightRangeMax), 0.0f);

        if (enemyObjs[obj_idx].Count > 0)
        {
            // 一個前のブロックと縦一直線になる対処
            if (Mathf.Abs(enemyObjs[obj_idx][enemyObjs[obj_idx].Count - 1].transform.position.y - position.y) <= 3.0f)
            {
                position = CreateEnemyRandomPosition(obj_idx, step_idx, height_idx);
            }
        }
        else
        {
            if (step_idx > enemyStepMin && enemyObjs[(obj_idx + 1) % 2].Count > 0)
            {
                // 一個前のブロックと縦一直線になる対処
                if (Mathf.Abs(enemyObjs[(obj_idx + 1) % 2][enemyObjs[(obj_idx + 1) % 2].Count - 1].transform.position.y - position.y) <= 3.0f)
                {
                    position = CreateEnemyRandomPosition(obj_idx, step_idx, height_idx);
                }
            }
        }

        return position;
    }
}
