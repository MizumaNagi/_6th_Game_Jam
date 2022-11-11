using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] private TutorialController tutoCon;
    [SerializeField] private bool isStartTri = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            if (isStartTri == true) tutoCon.NextEnableHint();
            else tutoCon.EndCurrentHint();
        }
    }
}
