using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Transform[] initMaps;

    private const int EmptyMapInterval = 10;

    private int deltaFrame = 750;
    private int initMapsLen = 0;
    private int nextMapIndex = 0;
    private int itemDropMapIndex = 5;
    private int remNonMoveCnt = 0;

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


    private void Start()
    {
        initMapsLen = initMaps.Length;
        nextMapIndex = initMapsLen;
        remNonMoveCnt = initMapsLen / 2;
        InitItemGenerate();
        Debug.Log("!");
    }

    private void Update()
    {
        deltaFrame++;
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

    public void InitItemGenerate()
    {
        int tmp = itemDropMapIndex;
        for(int i = 0; i < 20; i++)
        {
            GenerateItemGroup();
        }
    }

    private void GenerateItemGroup()
    {
        itemDropMapIndex++;

        if (itemDropMapIndex % EmptyMapInterval == 0)
        {
            return;
        }

        if (itemDropMapIndex % EmptyMapInterval == 1)
        {
            return;
        }

        float min = (deltaFrame - 1500) * 0.002f;
        min = Mathf.Max(0f, min);
        min = Mathf.Min(3f, min);
        float max = deltaFrame * 0.002f;
        max = Mathf.Min(3f, max);
        ItemSpawnDiff targetDiff = (ItemSpawnDiff)Random.Range(min, max);

        int rndItemTable = Random.Range(0, 20);
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 5; y++)
            {
                if (mapData[rndItemTable, y, x] == 1)
                {
                    SendItemGenerator(x, y, itemDropMapIndex, Item.ItemType.Enemy);
                }
            }
        }
    }

    private void SendItemGenerator(int x, int y, int map, Item.ItemType type)
    {
        Vector3 createPos = new Vector3(-1f + x, 0f, map * 6f + 0.8f * (3 - y));
        ItemFactory.Instance.CreateItem(type, 1, createPos);
    }

    private enum ItemSpawnDiff
    {
        Easy,
        Normal,
        Hard
    }
}
