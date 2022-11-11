using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのベジェ曲線移動, 左右移動の処理をするクラス
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float initVertSpeed;
    [SerializeField, Range(0f, 5f)] private float horiSpeed;
    [SerializeField] private RailinfoBetweenArea[] railArr;
    [SerializeField] private Transform player;
    [SerializeField] private Transform childParent;

    private const float CanMoveHorizontalVal = 1.1f;
    private const bool useBezierMove = false;
    private const int SaveParentPosFrameSize = 1000;
    private const int DelayFrameEachChild = 5;
    private const float AcceleEachFrame = 0.002f;

    private float vertSpeed;
    private float currentHorizontalMoveVal;
    private Transform myTrans;
    private Vector3 beforeFramePos;
    private Vector3[] parentPosEachFrame = new Vector3[SaveParentPosFrameSize];
    private float totalDeltaTime = 0f;
    private int deltaFrame = 0;
    private Coroutine loopPlayRunEffectCoroutine = null;
    private List<Coroutine> childMoveCorutineList = new List<Coroutine>();
    private bool isTutorial = false;

    private Vector3 initPos;

    public float Distance => totalDeltaTime;
    public float VertSpeed => vertSpeed;

    public bool isStop = false;

    public void ManagedStart()
    {
        myTrans = this.transform;
        beforeFramePos = myTrans.position;
        loopPlayRunEffectCoroutine = StartCoroutine(LoopPlayRunEffect());
        initPos = myTrans.position;
        vertSpeed = initVertSpeed;
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

        // 横移動入力検知
        if (Input.GetKey(KeyCode.A)) currentHorizontalMoveVal -= deltaTime * horiSpeed;
        if (Input.GetKey(KeyCode.D)) currentHorizontalMoveVal += deltaTime * horiSpeed;
        currentHorizontalMoveVal = Mathf.Min(currentHorizontalMoveVal, CanMoveHorizontalVal);
        currentHorizontalMoveVal = Mathf.Max(currentHorizontalMoveVal, -CanMoveHorizontalVal);
        totalDeltaTime += deltaTime * vertSpeed;

        if (useBezierMove == true)
        {
            // 線路情報取得
            RailinfoBetweenArea targetRail = railArr[Mathf.FloorToInt(totalDeltaTime) % railArr.Length];

            // 取ってきた線路に沿って移動
            targetRail.SetProperty();
            myTrans.position = DrawBezierCurve.GetBezierPos(targetRail.GetProperty(), totalDeltaTime % 1f);

            // カメラ角度調整
            //RotForward();

            // 横移動反映
            myTrans.position += transform.rotation * new Vector3(currentHorizontalMoveVal, 0f, 0f);
        }
        else
        {
            myTrans.position = initPos + transform.rotation * new Vector3(currentHorizontalMoveVal, 0f, totalDeltaTime);
        }

        // 子供たちを遅延込みで追尾させる
        Vector3 curFramePos = myTrans.position;
        for (int i = 0; i < childParent.childCount; i++)
        {
            childMoveCorutineList.Add(StartCoroutine(DelayMove(childParent.GetChild(i), curFramePos, myTrans.rotation, 0.1f * i)));
        }
    }
    
    public void ManagedFixedUpdate()
    {
        float firstChildPosZ = transform.position.z;
        for (int i = 0; i < childParent.childCount; i++)
        {
            Transform target = childParent.GetChild(i);
            target.position = parentPosEachFrame[GetSurplusSupportMinusValue(deltaFrame - Mathf.RoundToInt((float)(i + 1) * DelayFrameEachChild * (initVertSpeed / vertSpeed)), SaveParentPosFrameSize)];
            //Vector3 tmp = target.position;
            //target.position = new Vector3(tmp.x, tmp.y, firstChildPosZ - 0.5f * i);
        }

        if (isStop == true) return;

        float deltaTime = Time.deltaTime;

        // 横移動入力検知
        if (Input.GetKey(KeyCode.A)) currentHorizontalMoveVal -= deltaTime * horiSpeed;
        if (Input.GetKey(KeyCode.D)) currentHorizontalMoveVal += deltaTime * horiSpeed;
        currentHorizontalMoveVal = Mathf.Min(currentHorizontalMoveVal, CanMoveHorizontalVal);
        currentHorizontalMoveVal = Mathf.Max(currentHorizontalMoveVal, -CanMoveHorizontalVal);
        totalDeltaTime += deltaTime * vertSpeed;

        Vector3 currentPos = initPos + transform.rotation * new Vector3(currentHorizontalMoveVal, 0f, totalDeltaTime);
        myTrans.position = currentPos;
        parentPosEachFrame[deltaFrame % SaveParentPosFrameSize] = currentPos;
        deltaFrame++;

        if(isTutorial == false) vertSpeed += AcceleEachFrame;
    }

    public IEnumerator GameEnd()
    {
        yield return null;
        isStop = true;
        StopCoroutine(loopPlayRunEffectCoroutine);
        loopPlayRunEffectCoroutine = null;
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

    /// <summary>
    /// num % divNumの値を返す
    /// num < 0の時、(divNum * x) - num < divNumとなるように返す
    /// -1 % 100 = 99, -2 % 100 = 98, -101 % 100 = 99
    /// </summary>
    /// <returns></returns>
    private int GetSurplusSupportMinusValue(int num, int divNum)
    {
        if (num >= 0) return num % divNum;

        int i = (Mathf.Abs(num) / divNum) + 1;
        return (divNum * i - Mathf.Abs(num)) % divNum;
    }

    public void SkipTutorial()
    {
        totalDeltaTime = 104f;
        isTutorial = false;
        GameManager.Instance.EndTutorial();
    }

    public void EndTutorial()
    {
        isTutorial = false;
        GameManager.Instance.EndTutorial();
    }
}
