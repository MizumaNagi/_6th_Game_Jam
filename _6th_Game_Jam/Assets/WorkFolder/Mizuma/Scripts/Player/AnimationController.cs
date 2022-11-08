using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnItem()
    {
        animator.SetBool("isItem", true);
        animator.SetBool("isPlayer", false);
    }

    public void OnPlayer()
    {
        animator.SetBool("isItem", false);
        animator.SetBool("isPlayer", true);
        animator.speed = Random.Range(0.9f, 1.1f);
    }
}
