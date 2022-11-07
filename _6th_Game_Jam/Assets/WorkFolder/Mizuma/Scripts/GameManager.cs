using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    [SerializeField] private PlayerManager playerManager;

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
        playerManager.GameEnd();
    }
}
