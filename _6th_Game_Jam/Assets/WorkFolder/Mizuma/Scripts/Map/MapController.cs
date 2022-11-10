using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Transform[] initMaps;

    private const int CastleMapInterval = 10;
    private const int InitGenerateItemGroup = 15;
    private const int InitEmptyMapBlock = 7;
    private const int DestroyItemMapBlocks = 5;

    private int difficulty = 500;
    private int initMapsLen = 0;
    private int nextMapIndex = 0;
    private int itemDropMapIndex = 0;
    private int remNonMoveCnt = 0;
    private int currentPlayerPosIndex = 0;

    private readonly int[,,] mapData = new int[,,]
    {
        // Map_01
        {
            {0,1,0},
            {0,0,0},
            {1,0,1},
            {0,0,1},
            {0,0,0},
        },
        // Map_02
        {
            {0,0,0},
            {0,0,1},
            {0,1,1},
            {0,1,0},
            {0,1,0},
        },
        // Map_03
        {
            {0,1,0},
            {0,1,1},
            {1,1,0},
            {0,0,1},
            {0,0,0},
        },
        // Map_04
        {
            {0,1,1},
            {1,1,0},
            {1,0,1},
            {0,1,0},
            {1,0,0},
        },
        // Map_05
        {
            {0,0,1},
            {0,1,0},
            {1,0,1},
            {0,1,0},
            {0,0,1},
        },
        // Map_06
        {
            {0,0,1},
            {0,1,0},
            {0,1,1},
            {0,0,0},
            {0,1,1},
        },
        // Map_07
        {
            {0,0,1},
            {0,0,0},
            {1,0,1},
            {1,1,0},
            {0,1,0},
        },
        // Map_08
        {
            {0,0,1},
            {0,1,1},
            {0,1,1},
            {1,1,0},
            {1,0,0},
        },
        // Map_09
        {
            {0,1,0},
            {0,1,1},
            {1,0,1},
            {0,1,0},
            {1,0,0},
        },
        // Map_10
        {
            {1,0,1},
            {0,1,0},
            {1,0,1},
            {0,0,1},
            {1,0,0},
        },
        // Map_11
        {
            {0,1,0},
            {0,1,0},
            {1,1,1},
            {0,1,0},
            {0,1,0},
        },
        // Map_12
        {
            {0,1,1},
            {1,0,0},
            {0,1,0},
            {1,0,1},
            {0,1,0},
        },
        // Map_13
        {
            {0,0,0},
            {1,0,1},
            {0,1,0},
            {1,0,1},
            {0,1,0},
        },
        // Map_14
        {
            {0,0,0},
            {0,0,1},
            {0,1,0},
            {0,1,0},
            {1,1,0},
        },
        // Map_15
        {
            {0,1,1},
            {1,0,1},
            {0,1,1},
            {0,1,0},
            {0,1,0},
        },
        // Map_16
        {
            {0,0,1},
            {1,1,0},
            {0,0,1},
            {0,1,0},
            {0,1,0},
        },
        // Map_17
        {
            {0,1,0},
            {0,0,1},
            {1,0,0},
            {0,0,1},
            {1,0,0},
        },
        // Map_18
        {
            {0,0,1},
            {0,1,1},
            {0,1,1},
            {1,0,1},
            {0,1,0},
        },
        // Map_19
        {
            {1,0,1},
            {1,1,0},
            {0,1,0},
            {0,0,1},
            {0,1,1},
        },
        // Map_20
        {
            {0,1,1},
            {0,1,1},
            {0,1,1},
            {1,1,0},
            {1,0,0},
        },
    };

    private List<GameObject> mapIndexOfItemObjectList = new List<GameObject>();
    private List<int> mapIndexOfItemIndexList = new List<int>();

    private void Start()
    {
        initMapsLen = initMaps.Length;
        itemDropMapIndex = InitEmptyMapBlock;
        nextMapIndex = initMapsLen;
        remNonMoveCnt = initMapsLen / 2;
        InitItemGenerate();
        StartCoroutine(CheckUselessItem());
    }

    private void Update()
    {
        difficulty++;
    }

    public IEnumerator DelayMoveMap()
    {
        if(remNonMoveCnt > 0)
        {
            remNonMoveCnt--;
            yield break;
        }

        Transform t = initMaps[nextMapIndex % initMapsLen];
        yield return null;

        t.position = new Vector3(0f, 0f, 6f * nextMapIndex);
        nextMapIndex++;
    }

    public IEnumerator DelayCreateItem()
    {
        currentPlayerPosIndex++;
        if (InitEmptyMapBlock < currentPlayerPosIndex) GenerateItemGroup();
        yield return null;
    }

    public void InitItemGenerate()
    {
        for(int i = 0; i < InitGenerateItemGroup; i++)
        {
            GenerateItemGroup();
        }
    }

    private void GenerateItemGroup()
    {
        itemDropMapIndex++;

        // 砦及び空白エリアの生成
        if (itemDropMapIndex % CastleMapInterval == 0)
        {
            SendItemGenerator(1, 0, itemDropMapIndex, Item.ItemType.Enemy_Large, itemDropMapIndex / CastleMapInterval);
            return;
        }

        if (itemDropMapIndex % CastleMapInterval == 1)
        {
            return;
        }

        // 難易度指定
        float min = (difficulty - 1500) * 0.002f;
        min = Mathf.Max(0f, min);
        min = Mathf.Min(5f, min);
        float max = difficulty * 0.002f;
        max = Mathf.Min(5f, max);
        ItemSpawnDiff targetDiff = (ItemSpawnDiff)Random.Range(min, max);
        targetDiff = ItemSpawnDiff.Easy;

        // アイテム座標チェック
        List<(int, int)> itemIndexList = new List<(int, int)>();
        int rndItemTable = Random.Range(0, 20);
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 5; y++)
            {
                if (mapData[rndItemTable, y, x] == 1)
                {
                    itemIndexList.Add((x, y));
                }
            }
        }

        // 難易度によるアイテム数補正
        float numItemScale = ((int)targetDiff + 1) * 0.2f;
        int enableItemNum = Mathf.RoundToInt(itemIndexList.Count * numItemScale);
        int healItemNum = Mathf.RoundToInt((enableItemNum / 2));
        int enemyItemNum = enableItemNum - healItemNum;

        // プレイヤーHP, 難易度に応じたアイテム効力の補正
        int totalEffectPow = 1 + (itemDropMapIndex / 8);

        int[] enemyPowArr = new int[enemyItemNum];
        int[] enemyPowWeight = new int[enemyItemNum];
        int totalEnemyWeight = 0;
        for (int i = 0; i < enemyItemNum; i++)
        {
            enemyPowWeight[i] = Random.Range(1, 5);
            totalEnemyWeight += enemyPowWeight[i];
        }
        for(int i = 0; i< enemyItemNum; i++)
        {
            enemyPowArr[i] = Mathf.CeilToInt((float)totalEffectPow * enemyPowWeight[i] / totalEnemyWeight);
        }

        int[] healPowArr = new int[healItemNum];
        int[] healPowWeight = new int[healItemNum];
        int totalHealWeight = 0;
        for(int i = 0; i < healItemNum; i++)
        {
            healPowWeight[i] = Random.Range(3, 5);
            totalHealWeight = healPowWeight[i];
        }
        for(int i = 0; i < healItemNum; i++)
        {
            healPowArr[i] = Mathf.CeilToInt((float)totalEffectPow * healPowWeight[i] / totalHealWeight);
            healPowArr[i] = Mathf.Min(healPowArr[i], 4);
        }

        // アイテム生成
        for(int i = 0; i < enemyItemNum; i++)
        {
            int rnd = Random.Range(0, itemIndexList.Count);
            SendItemGenerator(itemIndexList[rnd].Item1, itemIndexList[rnd].Item2, itemDropMapIndex, Item.ItemType.Enemy, enemyPowArr[i]);
            itemIndexList.RemoveAt(rnd);
        }

        for(int i = 0; i < healItemNum; i++)
        {
            int rnd = Random.Range(0, itemIndexList.Count);
            SendItemGenerator(itemIndexList[rnd].Item1, itemIndexList[rnd].Item2, itemDropMapIndex, Item.ItemType.Heal, healPowArr[i]);
            itemIndexList.RemoveAt(rnd);
        }
    }

    private void SendItemGenerator(int x, int y, int map, Item.ItemType type, int effectPower)
    {
        Vector3 createPos = new Vector3(-1f + x, 0f, map * 6f + 0.8f * (3 - y));
        GameObject newItem = ItemFactory.Instance.CreateItem(type, effectPower, createPos);
        mapIndexOfItemObjectList.Add(newItem);
        mapIndexOfItemIndexList.Add(map);
    }

    private IEnumerator CheckUselessItem()
    {
        while (true)
        {
            for(int i = 0; i < mapIndexOfItemIndexList.Count; i++)
            {
                if(currentPlayerPosIndex > mapIndexOfItemIndexList[i] + DestroyItemMapBlocks)
                {
                    GameObject killItem = mapIndexOfItemObjectList[i];
                    mapIndexOfItemObjectList.RemoveAt(i);
                    mapIndexOfItemIndexList.RemoveAt(i);
                    Destroy(killItem);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private enum ItemSpawnDiff
    {
        VeryEasy,
        Easy,
        Normal,
        Hard,
        VeryHard
    }
}
