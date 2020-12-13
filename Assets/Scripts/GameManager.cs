using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { }
    private ObstacleManager obstacleManager;
    private UIManager uiManagerRef;

    private int playerLives, maxLives;
    private int playerCurrentScore;

    [SerializeField] LevelDataScriptableObject levelData;
    int currentLevel = 1;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public System.Action GameStartAction;
    public System.Action GameOverAction;

    private void Start()
    {
        obstacleManager = GetComponent<ObstacleManager>();
        uiManagerRef = GetComponent<UIManager>();
    }

    public void StartGame()
    {
        currentLevel = 1;
        playerCurrentScore = 0;
        maxLives = 3;
        playerLives = maxLives;
        GameStartAction?.Invoke();
        CreateNewObstacles();
        if (uiManagerRef != null)
        {
            uiManagerRef.ShowLevel(currentLevel);
        }
    }

    private void CreateNewObstacles()
    {
        Debug.Log("Create obstacles for level:" + currentLevel);
        int obs = levelData.levelDetails[currentLevel - 1].noOfNewObstacles;
        float maxObsSpeed = levelData.levelDetails[currentLevel - 1].maxSpeed;
        obstacleManager.CreateObstacles(obs,maxObsSpeed);
    }

    public void DeductLive()
    {
        playerLives--;
        if (uiManagerRef != null)
        {
            uiManagerRef.ShowLives(playerLives);
        }
        if (playerLives<=0)
        {
            //Game over
            obstacleManager.HideAllObstacles();
            ShowHighScore();
            //enable play button
            GameOverAction?.Invoke();
        }
    }

    public void UpdateScore(int score)
    {
        playerCurrentScore = score;

        if (currentLevel < levelData.levelDetails.Count)
        {
            if (playerCurrentScore >= levelData.levelDetails[currentLevel].score)
            {
                currentLevel++;
                CreateNewObstacles();
                if (uiManagerRef != null)
                {
                    uiManagerRef.ShowLevel(currentLevel);
                }

            }
        }

        if(uiManagerRef!= null)
        {
            uiManagerRef.ShowScore(score);
        }
    }

    private void ShowHighScore()
    {
        int highScore = PlayerPrefs.GetInt("High Score", 100);
        if(playerCurrentScore > highScore)
        {
            highScore = playerCurrentScore;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        uiManagerRef.ShowHighScore(highScore);
    }
}
