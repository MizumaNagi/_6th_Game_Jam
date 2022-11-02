using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRailInfoInPlay : MonoBehaviour
{
    [SerializeField] private Transform mapParent;
    [SerializeField] private Transform debugBezierParent;

    private void Start()
    {
        Invoke("DataUpdate", 1f);
    }

    private void DataUpdate()
    {
        string s = "";
        for(int i = 0; i < mapParent.childCount; i++)
        {
            Transform bezierPosParent = debugBezierParent.GetChild(i).GetChild(1);
            s += bezierPosParent.GetChild(0).position;
            s += bezierPosParent.GetChild(1).position;
            s += bezierPosParent.GetChild(3).position;
            s += bezierPosParent.GetChild(2).position;
            s += '\n';
        }
    }
}