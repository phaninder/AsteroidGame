using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] ObstacleManager obstacleRef;
    [SerializeField] int score;

    private void OnEnable()
    {
        obstacleRef.UpdateScore += UpdateScore;
        GameManager.Instance.GameStartAction += () => { score = 0; };
    }

    private void OnDisable()
    {
        obstacleRef.UpdateScore -= UpdateScore;
    }

    void UpdateScore(bool bigAsteroid)
    {
        if(bigAsteroid)
        {
            score += 20;
        }
        else
        {
            score += 5;
        }

        GameManager.Instance.UpdateScore(score);
    }

}
