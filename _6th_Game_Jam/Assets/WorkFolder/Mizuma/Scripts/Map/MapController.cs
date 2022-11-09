using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Transform[] initMaps;

    private int initMapsLen = 0;
    private int nextMapIndex = 0;
    private int remNonMoveCnt = 0;

    private void Start()
    {
        initMapsLen = initMaps.Length;
        nextMapIndex = initMapsLen;
        remNonMoveCnt = initMapsLen / 2;
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
}
