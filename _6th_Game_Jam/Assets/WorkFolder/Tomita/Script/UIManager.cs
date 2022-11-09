using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]private GameObject scorePanel;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI diatanceText;
    [SerializeField] private TextMeshProUGUI maxPlayerText;
    [SerializeField] private TextMeshProUGUI GameOverscoreText;
    private void Start()
    {
        OnGameStart();
        //OnGameEnd();
        UpdateScoreUI(9);
        UpdateGameOverUI(5.5f,4,8);
    }
    void OnGameStart()
    {
        this.scorePanel.SetActive(true);
    }
    void OnGameEnd()
    {
        this.scorePanel.SetActive(false);
        this.GameOver.SetActive(true);
    }
    void UpdateScoreUI(int score)
    {
        this.scoreText.text = score.ToString();
    }
    void UpdateGameOverUI(float diatance, int maxPlayer, int score)
    {
        this.diatanceText.text = diatance.ToString();
        this.maxPlayerText.text = maxPlayer.ToString();
        this.GameOverscoreText.text = score.ToString();
    }
}


