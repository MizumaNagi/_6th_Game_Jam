using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnItem()
    {
        animator.SetTrigger("isItem");
        animator.speed = Random.Range(0.9f, 1.1f);
    }

    public void OnPlayer()
    {
        animator.SetTrigger("isPlayer");
        animator.speed = Random.Range(0.9f, 1.1f);
    }
}
