using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour
{
    [SerializeField] GameObject bigAsteroidRef;
    [SerializeField] GameObject smallAsteroidRef;
    [SerializeField] GameObject shootPowerup;
    [SerializeField] GameObject shieldPowerup;
    [SerializeField] int noOfAsteroids = 10;
    //using list to save all asteroids
    private List<GameObject> bigAsteroidContainer;
    private List<GameObject> smallAsteroidContainer;
    private List<GameObject> shootPowerupContainer;
    private List<GameObject> shieldPowerupContainer;
    private GameObject asteroidParent;
    private GameObject powerupParent;

    // Start is called before the first frame update
    void Start()
    {
        bigAsteroidContainer = new List<GameObject>();
        smallAsteroidContainer = new List<GameObject>();
        shootPowerupContainer = new List<GameObject>();
        shieldPowerupContainer = new List<GameObject>();
        asteroidParent = new GameObject("Asteroid Container");
        powerupParent = new GameObject("Powerup Container");

        CreateAsteroids();
        CreatePowerups();
    }

    void CreateAsteroids()
    {
        for (int i = 0; i < noOfAsteroids; i++)
        {
            bigAsteroidContainer.Add(CreateAsteroid(bigAsteroidRef));
            smallAsteroidContainer.Add(CreateAsteroid(smallAsteroidRef));
            smallAsteroidContainer.Add(CreateAsteroid(smallAsteroidRef));
        }
    }

    void CreatePowerups()
    {
        for (int i = 0; i < 5; i++)
        {
            shootPowerupContainer.Add(CreatePowerup(shootPowerup));
            shieldPowerupContainer.Add(CreatePowerup(shieldPowerup));
        }
    }

    private GameObject CreatePowerup(GameObject powerup)
    {
        GameObject tempAst = Instantiate(powerup,powerupParent.transform);
        tempAst.SetActive(false);
        return tempAst;
    }

    private GameObject CreateAsteroid(GameObject objToCreate)
    {
        GameObject tempAst = Instantiate(objToCreate, asteroidParent.transform);
        tempAst.SetActive(false);
        return tempAst;
    }

    public GameObject GetBigAsteroid()
    {
        if (bigAsteroidContainer != null && bigAsteroidContainer.Count > 0)
        {
            foreach (GameObject obj in bigAsteroidContainer)
            {
                if (!obj.activeInHierarchy)
                    return obj;
            }
        }
        //if there are no available asteroids create a new one
        GameObject newAst = CreateAsteroid(bigAsteroidRef);
        bigAsteroidContainer.Add(newAst);
        return newAst;
    }

    public GameObject GetSmallAsteroid()
    {
        if (smallAsteroidContainer != null && smallAsteroidContainer.Count > 0)
        {
            foreach (GameObject obj in smallAsteroidContainer)
            {
                if (!obj.activeInHierarchy)
                    return obj;
            }
        }

        GameObject newAst = CreateAsteroid(smallAsteroidRef);
        smallAsteroidContainer.Add(newAst);
        return newAst;

    }

    public GameObject GetShieldPowerup()
    {
        foreach (GameObject obj in shieldPowerupContainer)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        GameObject temp = CreatePowerup(shieldPowerup);
        shieldPowerupContainer.Add(temp);
        return temp;
    }

    public GameObject GetShootPowerup()
    {
        foreach (GameObject obj in shootPowerupContainer)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        GameObject temp = CreatePowerup(shootPowerup);
        shootPowerupContainer.Add(temp);
        return temp;
    }

    public void HideAsteroids()
    {
        foreach (GameObject obj in bigAsteroidContainer)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in smallAsteroidContainer)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in shootPowerupContainer)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in shieldPowerupContainer)
        {
            obj.SetActive(false);
        }
    }
}
