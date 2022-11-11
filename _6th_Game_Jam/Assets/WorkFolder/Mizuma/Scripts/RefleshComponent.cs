using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RefleshComponent : MonoBehaviour
{
    [SerializeField] private MeshFilter filter;
    [SerializeField] private MeshRenderer render;
    [SerializeField] private VideoPlayer video;

    private void Start()
    {
        SoundManager.Instance.StopBGM(BGMName.Main);
        SoundManager.Instance.PlayBGM(BGMName.Title);

        render.enabled = false;
        video.enabled = false;
        StartCoroutine(RePlayComponent());
    }

    private IEnumerator RePlayComponent()
    {
        yield return null;
        render.enabled = true;
        video.enabled = true;
    }
}
