using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    GameObject gameOver;
    private int score = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        this.gameOver = GameObject.Find("GameOverText");
        this.gameOver.SetActive(false);
    }

    private void Update()
    {
        score += PlayerManager.Instance.GetScoreEachFrame();
    }

    public void GameOver()
    {
        // GameOverテキストの表示
        this.gameOver.SetActive(true);
    }

    public void GameEnd()
    {
        PlayerManager.Instance.GameEnd();
        SoundManager.Instance.StopSE(SEName.Running);
        GameOver();
    }
}
