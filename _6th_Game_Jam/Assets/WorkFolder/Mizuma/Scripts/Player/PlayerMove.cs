using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform myTrans;
    private Vector3 beforeFramePos;

    private void Start()
    {
        myTrans = this.transform;
    }

    private void Update()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
        RotForward();
    }

    private void RotForward()
    {
        Vector3 delta = myTrans.position - beforeFramePos;
        beforeFramePos = myTrans.position;
        if (delta == Vector3.zero) return;
        myTrans.rotation = Quaternion.LookRotation(delta, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rail") == true)
        {
            RailinfoBetweenArea railArea = other.GetComponent<AreaTriggerController>().OnTrigger();
            Debug.Log(other.gameObject.name);
        }
    }
}
