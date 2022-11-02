using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float speed;
    [SerializeField] private RailinfoBetweenArea[] railArr;

    private const float CanMoveHorizontalVal = 2f;
    private float currentHorizontalMoveVal;
    private Transform myTrans;
    private Vector3 beforeFramePos;

    private void Start()
    {
        myTrans = this.transform;
    }

    float deltaTime = 0f;
    private void Update()
    {
        // é©ìÆà⁄ìÆ
        deltaTime += Time.deltaTime * speed;
        RailinfoBetweenArea targetRail = railArr[Mathf.FloorToInt(deltaTime) % railArr.Length];
        myTrans.position = DrawBezierCurve.GetBezierPos(targetRail.GetProperty(), deltaTime % 1f);

        // â°à⁄ìÆ


        // éãì_êÿÇËë÷Ç¶
        RotForward();
    }

    private void RotForward()
    {
        Vector3 delta = myTrans.position - beforeFramePos;
        beforeFramePos = myTrans.position;
        if (delta == Vector3.zero) return;
        myTrans.rotation = Quaternion.LookRotation(delta, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rail") == true)
        {
            RailinfoBetweenArea railArea = other.GetComponent<AreaTriggerController>().OnTrigger();
            //Debug.Log(other.gameObject.name);
        }
    }
}
