using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのベジェ曲線移動, 左右移動の処理をするクラス
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float vertSpeed;
    [SerializeField, Range(0f, 5f)] private float horiSpeed;
    [SerializeField] private RailinfoBetweenArea[] railArr;
    [SerializeField] private Transform player;

    private const float CanMoveHorizontalVal = 2f;
    private float currentHorizontalMoveVal;
    private Transform myTrans;
    private Vector3 beforeFramePos;
    private float deltaTime = 0f;

    public bool isStop = false;

    private void Start()
    {
        myTrans = this.transform;
        beforeFramePos = myTrans.position;
    }

    private void Update()
    {
        if (isStop == true) return;

        // 線路情報取得
        deltaTime += Time.deltaTime * vertSpeed;
        RailinfoBetweenArea targetRail = railArr[Mathf.FloorToInt(deltaTime) % railArr.Length];

        // 横移動
        if (Input.GetKey(KeyCode.A)) currentHorizontalMoveVal -= Time.deltaTime * horiSpeed;
        if (Input.GetKey(KeyCode.D)) currentHorizontalMoveVal += Time.deltaTime * horiSpeed;
        currentHorizontalMoveVal = Mathf.Min(currentHorizontalMoveVal, CanMoveHorizontalVal);
        currentHorizontalMoveVal = Mathf.Max(currentHorizontalMoveVal, -CanMoveHorizontalVal);
        
        // 取ってきた線路に沿って移動
        myTrans.position = DrawBezierCurve.GetBezierPos(targetRail.GetProperty(), deltaTime % 1f);

        // カメラ角度調整
        RotForward();

        // 横移動反映
        myTrans.position += transform.rotation * new Vector3(currentHorizontalMoveVal, 0f, 0f);
    }

    private void RotForward()
    {
        Vector3 delta = myTrans.position - beforeFramePos;
        beforeFramePos = myTrans.position;
        if (delta == Vector3.zero) return;
        myTrans.rotation = Quaternion.LookRotation(delta, Vector3.up);
    }
}
