using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    private int score = 0;
    private bool isTutorial = true;
    [SerializeField] private UIManager uiManager;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        uiManager.OnGameStart();

        SoundManager.Instance.StopBGM(BGMName.Title);
        SoundManager.Instance.PlayBGM(BGMName.Main);

        SoundOption runSeOP = new SoundOption();
        runSeOP.isLoop = 1;
        SoundManager.Instance.PlaySE(SEName.Running, runSeOP);
    }

    private void Update()
    {
        if (isTutorial == false)
        {
            score += PlayerManager.Instance.GetScoreEachFrame();
            uiManager.UpdateScoreUI(score);
        }
    }

    public void EndTutorial()
    {
        isTutorial = false;
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
