using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRot : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float radius;
    public float speed;

    private float deltaTime = 0f;

    private void Update()
    {
        deltaTime += Time.deltaTime;
        transform.position = player.position + new Vector3(Mathf.Sin(deltaTime * speed) * radius, 3.5f, Mathf.Cos(deltaTime * speed) * radius);
        transform.LookAt(player);
    }
}
