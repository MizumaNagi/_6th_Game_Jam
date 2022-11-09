using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    GameObject gameOver;
    private int score = 0;
    [SerializeField] private UIManager uiManager;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        uiManager.OnGameStart();
    }

    private void Update()
    {
        score += PlayerManager.Instance.GetScoreEachFrame();
        uiManager.UpdateScoreUI(1);
    }

    public void GameOver()
    {
        uiManager.OnGameEnd();
        uiManager.UpdateGameOverUI(1234.5f, 30, 83991);
    }

    public void GameEnd()
    {
        PlayerManager.Instance.GameEnd();
        SoundManager.Instance.StopSE(SEName.Running);
        GameOver();
    }
}
