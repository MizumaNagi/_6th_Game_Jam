using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveMode : MonoBehaviour
{
    private const float MoveTriggerDistance = 25f;

    public GameObject Player; //Target

    private void Start()
    {
        Player = PlayerManager.Instance.gameObject;
    }

    void Update()
    {
        var direction = Player.transform.position - transform.position;
        if (Mathf.Abs(direction.z) > MoveTriggerDistance) return;

        transform.Translate(direction.normalized * Time.deltaTime * 1f, Space.World);

        var angle = Vector3.Angle(transform.forward, direction);

        var cross = Vector3.Cross(transform.forward, direction);

        var turn = cross.y >= 0 ? 1f : -1f;

        transform.Rotate(transform.up, angle * Time.deltaTime * 5f * turn, Space.World);
    }
}
