using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃x�W�F�Ȑ��ړ�, ���E�ړ��̏���������N���X
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] private float vertSpeed;
    [SerializeField, Range(0f, 5f)] private float horiSpeed;
    [SerializeField] private RailinfoBetweenArea[] railArr;
    [SerializeField] private Transform player;
    [SerializeField] private Transform childParent;

    private const float CanMoveHorizontalVal = 1.1f;
    private const bool useBezierMove = false;

    private float currentHorizontalMoveVal;
    private Transform myTrans;
    private Vector3 beforeFramePos;
    private float totalDeltaTime = 0f;
    private Coroutine loopPlayRunEffectCoroutine = null;
    private List<Coroutine> childMoveCorutineList = new List<Coroutine>();

    private Vector3 initPos;

    public bool isStop = false;

    public void ManagedStart()
    {
        myTrans = this.transform;
        beforeFramePos = myTrans.position;
        loopPlayRunEffectCoroutine = StartCoroutine(LoopPlayRunEffect());
        initPos = myTrans.position;
    }

    public void ManagedUpdate()
    {
        if (isStop == true)
        {
            if (loopPlayRunEffectCoroutine != null)
            {
                StopCoroutine(loopPlayRunEffectCoroutine);
                loopPlayRunEffectCoroutine = null;
            }

            foreach(Coroutine coroutine in childMoveCorutineList.ToArray())
            {
                StopCoroutine(coroutine);
            }
            childMoveCorutineList.Clear();

            return;
        }

        if (loopPlayRunEffectCoroutine == null) loopPlayRunEffectCoroutine = StartCoroutine(LoopPlayRunEffect());

        float deltaTime = Time.deltaTime;

        // ���ړ����͌��m
        if (Input.GetKey(KeyCode.A)) currentHorizontalMoveVal -= deltaTime * horiSpeed;
        if (Input.GetKey(KeyCode.D)) currentHorizontalMoveVal += deltaTime * horiSpeed;
        currentHorizontalMoveVal = Mathf.Min(currentHorizontalMoveVal, CanMoveHorizontalVal);
        currentHorizontalMoveVal = Mathf.Max(currentHorizontalMoveVal, -CanMoveHorizontalVal);
        totalDeltaTime += deltaTime * vertSpeed;

        if (useBezierMove == true)
        {
            // ���H���擾
            RailinfoBetweenArea targetRail = railArr[Mathf.FloorToInt(totalDeltaTime) % railArr.Length];

            // ����Ă������H�ɉ����Ĉړ�
            targetRail.SetProperty();
            myTrans.position = DrawBezierCurve.GetBezierPos(targetRail.GetProperty(), totalDeltaTime % 1f);

            // �J�����p�x����
            RotForward();

            // ���ړ����f
            myTrans.position += transform.rotation * new Vector3(currentHorizontalMoveVal, 0f, 0f);
        }
        else
        {
            myTrans.position = initPos + transform.rotation * new Vector3(currentHorizontalMoveVal, 0f, totalDeltaTime);
        }

        // �q��������x�����݂Œǔ�������
        Vector3 curFramePos = myTrans.position;
        for (int i = 0; i < childParent.childCount; i++)
        {
            childMoveCorutineList.Add(StartCoroutine(DelayMove(childParent.GetChild(i), curFramePos, myTrans.rotation, 0.1f * i)));
        }
    }

    private IEnumerator LoopPlayRunEffect()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f / 208.45f);
            EffectManager.Instance.PlayEffect(EffectManager.EffectType.Run_Smoke, new Vector3(player.position.x, 0f, player.position.z));
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
