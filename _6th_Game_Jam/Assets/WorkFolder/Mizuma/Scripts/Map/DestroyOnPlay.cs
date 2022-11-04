using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlay : MonoBehaviour
{
    [SerializeField] private bool isDeatroy = false;

    void Start()
    {
        if (isDeatroy == true) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}
