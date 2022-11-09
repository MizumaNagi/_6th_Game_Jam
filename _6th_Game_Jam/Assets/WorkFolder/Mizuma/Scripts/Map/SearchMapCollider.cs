using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchMapCollider : MonoBehaviour
{
    [SerializeField] private MapController mapCon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Rail") == true)
        {
            StartCoroutine(mapCon.DelayMoveMap());
        }
    }
}
