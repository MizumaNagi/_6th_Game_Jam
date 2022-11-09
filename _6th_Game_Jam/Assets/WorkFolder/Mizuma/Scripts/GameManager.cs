using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
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
        uiManager.UpdateScoreUI(score);
    }

    public void GameOver()
    {
        uiManager.OnGameEnd();
        uiManager.UpdateGameOverUI(PlayerManager.Instance.GetDistance(), PlayerManager.Instance.GetMaxPlayerLength(), score);
    }

    public void GameEnd()
    {
        PlayerManager.Instance.GameEnd();
        SoundManager.Instance.StopSE(SEName.Running);
        GameOver();
    }
}
