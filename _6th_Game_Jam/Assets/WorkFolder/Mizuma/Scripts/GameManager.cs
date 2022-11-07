using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void GameEnd()
    {
        PlayerManager.Instance.GameEnd();
        SoundManager.Instance.StopSE(SEName.Running);
    }
}
