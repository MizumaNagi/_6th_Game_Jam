using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ベジェ曲線の描画、値を求めるクラス
/// </summary>
public class DrawBezierCurve : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform pointParent;

    [SerializeField] private Transform start;
    [SerializeField] private Transform startHandle;
    [SerializeField] private bool isUseStartHandle;
    [SerializeField] private Transform end;
    [SerializeField] private Transform endHandle;
    [SerializeField] private bool isUseEndHandle;
    [SerializeField] private GameObject markerObj;
    [SerializeField, Range(0f, 1f)] private float markerT;

    private const int drawLineCnt = 10;
    private GameObject[] lineObjArr = new GameObject[drawLineCnt];
    private Transform[] pointArr;
    private Transform lineObjParent;
    private Vector3[] pointPosArr;

    private void Start()
    {
        lineObjParent = new GameObject("LineObjParent").transform;
        lineObjParent.SetParent(this.transform);
        for(int i = 0; i < drawLineCnt; i++)
        {
            GameObject newLineObj = Instantiate(linePrefab);
            lineObjArr[i] = newLineObj;
            newLineObj.transform.SetParent(lineObjParent);
        }

        pointArr = new Transform[pointParent.childCount];
        for(int i = 0; i < pointParent.childCount; i++)
        {
            pointArr[i] = pointParent.GetChild(i).transform;
        }
    }

    private void Update()
    {
        markerT += 0.01f;
        markerT %= 1f;
        int pListLen = pointArr.Length;
        if (pListLen < 3) { Debug.Log("ベジェ曲線の描画には少なくとも3点以上の座標(pointArr)が必要です。"); return; }

        if (isUseStartHandle == false) startHandle.position = start.transform.position;
        if (isUseEndHandle == false) endHandle.position = end.transform.position;

        pointPosArr = new Vector3[pListLen];
        for (int i = 0; i < pListLen; i++)
        {
            pointPosArr[i] = pointArr[i].transform.position;
        }

        DrawBezierLine();
        markerObj.transform.position = GetBezierPos(pointPosArr, markerT);
    }

    private void DrawBezierLine()
    {
        for(int i = 0; i < drawLineCnt; i++)
        {
            float t = (float)i / (float)drawLineCnt;
            lineObjArr[i].transform.position = GetBezierPos(pointPosArr, t);
        }
    }

    public static Vector3 GetBezierPos(Vector3[] vertexPosArr, float t)
    {
        List<Vector3> betweenEachVertexList = new List<Vector3>();
        betweenEachVertexList.AddRange(vertexPosArr);

        for(int i = 0; i < vertexPosArr.Length; i++)
        {
            Vector3[] afterBetweenEachVertexArr = GetBetweenEachVertex(betweenEachVertexList.ToArray(), t);
            betweenEachVertexList.Clear();
            betweenEachVertexList.AddRange(afterBetweenEachVertexArr);
        }

        return betweenEachVertexList[0];
    }

    private static Vector3[] GetBetweenEachVertex(Vector3[] vertexPosArr, float t)
    {
        int argVertexLen = vertexPosArr.Length;
        if (argVertexLen == 0) return new Vector3[] { Vector3.zero };
        if (argVertexLen == 1) return vertexPosArr;

        Vector3[] result = new Vector3[argVertexLen - 1];

        for (int i = 0; i < vertexPosArr.Length - 1; i++)
        {
            result[i] = new Vector3(Mathf.Lerp(vertexPosArr[i].x, vertexPosArr[i + 1].x, t),
                Mathf.Lerp(vertexPosArr[i].y, vertexPosArr[i + 1].y, t),
                Mathf.Lerp(vertexPosArr[i].z, vertexPosArr[i + 1].z, t));
        }

        return result;
    }
}
