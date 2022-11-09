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

    public void OnGameStart()
    {
        this.scorePanel.SetActive(true);
    }
    public void OnGameEnd()
    {
        this.scorePanel.SetActive(false);
        this.GameOver.SetActive(true);
    }
    public void UpdateScoreUI(int score)
    {
        this.scoreText.text = score.ToString();
    }
    public void UpdateGameOverUI(float diatance, int maxPlayer, int score)
    {
        this.diatanceText.text = diatance.ToString("F1") + " m";
        this.maxPlayerText.text = maxPlayer.ToString();
        this.GameOverscoreText.text = score.ToString() + " pt";
    }
}