using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    [SerializeField] Text levelText;
    [SerializeField] Text highScoreText;
    [SerializeField] Text currentScoreText;
    [SerializeField] Button playGameBtn;

    private void Start()
    {
        GameManager.Instance.GameOverAction += OnGameOver;
        playGameBtn.onClick.AddListener(OnPlayButtonPressed);
    }

    private void OnEnable()
    {
        Init();
        GameManager.Instance.GameStartAction += Init;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameStartAction -= Init;
    }

    private void Init()
    {
        scoreText.text = "Score: " + 0;
        livesText.text = "Lives: " + 3;
        levelText.text = "Level: " + 1;
        currentScoreText.text = "";
        highScoreText.text = "";
    }
    
    public void ShowScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ShowLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void ShowLevel(int level)
    {
        levelText.text = "Level: " + level;
    }

    public void OnPlayButtonPressed()
    {
        playGameBtn.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void OnGameOver()
    {
        playGameBtn.gameObject.SetActive(true);
    }

    public void ShowHighScore(int highScore)
    {
        currentScoreText.text = scoreText.text;
        highScoreText.text = "High Score: " + highScore;
    }


}
