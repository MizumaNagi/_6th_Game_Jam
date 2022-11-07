using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    private float radius = 0.2f;
    private float speed = 2f;
    private Vector3[] posArr = new Vector3[4];
    private float deltaTime;
    private int childCnt;

    public void Start()
    {
        childCnt = transform.childCount;
        for (int i = 0; i < childCnt; i++)
        {
            transform.GetChild(i).localScale *= 0.6f;
        }
    }

    void Update()
    {
        deltaTime += Time.deltaTime;

        for(int i = 1; i <= childCnt; i++)
        {
            Transform target = transform.GetChild(i - 1);
            Vector3 diff = target.position - posArr[i - 1];
            posArr[i - 1] = target.position;
            transform.GetChild(i - 1).position = transform.position + new Vector3(Mathf.Sin(deltaTime * speed + (((float)i / childCnt) * 6.2f)), 0f, Mathf.Cos(deltaTime * speed + (((float)i / childCnt) * 6.2f))) * radius;
            if(diff.magnitude > 0.00001f)
            {
                target.rotation = Quaternion.LookRotation(diff);
            }
        }
    }
}
