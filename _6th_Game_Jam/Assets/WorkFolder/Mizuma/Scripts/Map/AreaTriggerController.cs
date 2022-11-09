using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �u���b�N��̃}�b�v���v���C���[�����m����N���X
/// </summary>
public class AreaTriggerController : MonoBehaviour
{
    [SerializeField] private BoxCollider frontTrigger;

    private const float AutoEnableTriggerTime = 5f;

    public void OnTrigger()
    {
        DisableTrigger();
    }

    private void EnableTrigger()
    {
        frontTrigger.enabled = true;
    }

    private void DisableTrigger()
    {
        frontTrigger.enabled = false;
        StartCoroutine(AutoEnableTrigger());
    }

    private IEnumerator AutoEnableTrigger()
    {
        yield return new WaitForSeconds(AutoEnableTriggerTime);
        EnableTrigger();
    }
}
