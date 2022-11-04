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
    [SerializeField] private Transform childParent;

    private const float CanMoveHorizontalVal = 2f;

    private float currentHorizontalMoveVal;
    private Transform myTrans;
    private Vector3 beforeFramePos;
    private float totalDeltaTime = 0f;
    private Coroutine loopPlayRunEffectCoroutine = null;

    public bool isStop = false;

    public void ManagedStart()
    {
        myTrans = this.transform;
        beforeFramePos = myTrans.position;
        loopPlayRunEffectCoroutine = StartCoroutine(LoopPlayRunEffect());
    }

    public void ManagedUpdate()
    {
        if (isStop == true) { StopCoroutine(loopPlayRunEffectCoroutine); return; }

        // ���H���擾
        float deltaTime = Time.deltaTime;
        totalDeltaTime += deltaTime * vertSpeed;
        RailinfoBetweenArea targetRail = railArr[Mathf.FloorToInt(totalDeltaTime) % railArr.Length];

        // ���ړ�
        if (Input.GetKey(KeyCode.A)) currentHorizontalMoveVal -= deltaTime * horiSpeed;
        if (Input.GetKey(KeyCode.D)) currentHorizontalMoveVal += deltaTime * horiSpeed;
        currentHorizontalMoveVal = Mathf.Min(currentHorizontalMoveVal, CanMoveHorizontalVal);
        currentHorizontalMoveVal = Mathf.Max(currentHorizontalMoveVal, -CanMoveHorizontalVal);
        
        // ����Ă������H�ɉ����Ĉړ�
        myTrans.position = DrawBezierCurve.GetBezierPos(targetRail.GetProperty(), totalDeltaTime % 1f);

        // �J�����p�x����
        RotForward();

        // ���ړ����f
        myTrans.position += transform.rotation * new Vector3(currentHorizontalMoveVal, 0f, 0f);

        // �q��������x�����݂Œǔ�������
        Vector3 curFramePos = myTrans.position;
        for (int i = 0; i < childParent.childCount; i++)
        {
            StartCoroutine(DelayMove(childParent.GetChild(i), curFramePos, myTrans.rotation, 0.1f * (i + 1)));
        }
    }

    private IEnumerator LoopPlayRunEffect()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f / 208.45f);
            EffectManager.Instance.PlayEffect(EffectManager.EffectType.Run_Smoke, player.position);
        }
    }

    private IEnumerator DelayMove(Transform child, Vector3 pos, Quaternion rot, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if (child == null) yield break;

        child.position = new Vector3(pos.x, child.position.y, pos.z);
        child.rotation = rot;
    }

    private void RotForward()
    {
        Vector3 delta = myTrans.position - beforeFramePos;
        beforeFramePos = myTrans.position;
        if (delta == Vector3.zero) return;
        myTrans.rotation = Quaternion.LookRotation(delta, Vector3.up);
    }
}
