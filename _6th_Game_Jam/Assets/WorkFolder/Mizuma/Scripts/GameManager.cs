using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    GameObject gameOver;
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
    public void GameOver()
    {
        // GameOver�e�L�X�g�̕\��
        this.gameOver.SetActive(true);
    }
}
