using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] AsteroidCreator asteroidCreator;
    private int maxNoOfObstacles = 30;
    private int obstacleForLevel = 5;
    private int prevPowerup = -1;
    public System.Action<bool> UpdateScore;

    // Start is called before the first frame update
    void Start()
    {
        if(asteroidCreator == null)
        {
            asteroidCreator = GetComponent<AsteroidCreator>();
        }
        GameManager.Instance.GameStartAction += Init;
    }

    void Init()
    {
        prevPowerup = -1;
        StartCoroutine(CreatePowerups());
    }

    public void HideAllObstacles()
    {
        asteroidCreator.HideAsteroids();
    }

    private IEnumerator CreatePowerups()
    {
        if (!GameManager.Instance.gameOver)
        {
            float waitTime = Random.Range(8f, 20f);
            yield return new WaitForSeconds(waitTime);
            GameObject powerup;
            int randomPowerup = Random.Range(0, 1);
            if (randomPowerup  != prevPowerup)
            {
                powerup = asteroidCreator.GetShootPowerup();
            }
            else
            {
                powerup = asteroidCreator.GetShieldPowerup();
            }

            prevPowerup = randomPowerup;
            powerup.transform.position = GetRandomPosition();
            powerup.SetActive(true);

            StartCoroutine(CreatePowerups());
        }
    }

    public void CreateObstacles(int noOfObstacles,float maxSpeed)
    {
        //int noOfObstacles = (level * obstacleForLevel) < maxNoOfObstacles ? (level * obstacleForLevel) : maxNoOfObstacles;
        
        for(int i=0;i<noOfObstacles;i++)
        {
            GameObject  temp =  asteroidCreator.GetBigAsteroid();
            temp.transform.position = GetRandomPosition();
            temp.GetComponent<AsteroidMovement>().SetReference(this,maxSpeed);
            temp.SetActive(true);
        }
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 screenStartPoint = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 screenEndPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 screenDist = screenEndPoint - screenStartPoint;
        return (Random.insideUnitSphere * Screen.width);

    }

    public void UpdateObstacles(Vector2 pos,bool createSmallAsteroids,float maxSpeed)
    {
        if (createSmallAsteroids)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject temp = asteroidCreator.GetSmallAsteroid();
                temp.transform.position = pos;
                temp.GetComponent<AsteroidMovement>().SetReference(this,maxSpeed);
                temp.SetActive(true);
            }
        }
        //update score for registered events
        UpdateScore?.Invoke(createSmallAsteroids);
    }
}

public enum ObstacleType
{
    SmallAsteroid,
    BigAsteroid,
    UFO
}