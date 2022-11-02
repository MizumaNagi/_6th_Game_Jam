using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロック状のマップがプレイヤーを検知するクラス
/// </summary>
public class AreaTriggerController : MonoBehaviour
{
    [SerializeField] private BoxCollider frontTrigger;
    [SerializeField] private BoxCollider backTrigger;
    [SerializeField] private RailinfoBetweenArea railInfo;

    private const float AutoEnableTriggerTime = 5f;

    public RailinfoBetweenArea OnTrigger()
    {
        DisableTrigger();
        return railInfo;
    }

    private void EnableTrigger()
    {
        frontTrigger.enabled = true;
        backTrigger.enabled = true;
    }

    private void DisableTrigger()
    {
        frontTrigger.enabled = false;
        backTrigger.enabled = false;
        StartCoroutine(AutoEnableTrigger());
    }

    private IEnumerator AutoEnableTrigger()
    {
        yield return new WaitForSeconds(AutoEnableTriggerTime);
        EnableTrigger();
    }
}
