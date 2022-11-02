using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃x�W�F�Ȑ��ړ�, ���E�ړ��̏���������N���X
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

        // ���H���擾
        deltaTime += Time.deltaTime * vertSpeed;
        RailinfoBetweenArea targetRail = railArr[Mathf.FloorToInt(deltaTime) % railArr.Length];

        // ���ړ�
        if (Input.GetKey(KeyCode.A)) currentHorizontalMoveVal -= Time.deltaTime * horiSpeed;
        if (Input.GetKey(KeyCode.D)) currentHorizontalMoveVal += Time.deltaTime * horiSpeed;
        currentHorizontalMoveVal = Mathf.Min(currentHorizontalMoveVal, CanMoveHorizontalVal);
        currentHorizontalMoveVal = Mathf.Max(currentHorizontalMoveVal, -CanMoveHorizontalVal);
        
        // ����Ă������H�ɉ����Ĉړ�
        myTrans.position = DrawBezierCurve.GetBezierPos(targetRail.GetProperty(), deltaTime % 1f);

        // �J�����p�x����
        RotForward();

        // ���ړ����f
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
