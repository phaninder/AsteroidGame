using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour
{
    [SerializeField] GameObject bigAsteroidRef;
    [SerializeField] GameObject smallAsteroidRef;

    [SerializeField] int noOfAsteroids = 10;
    //using list to save all asteroids
    private List<GameObject> bigAsteroidContainer;
    private List<GameObject> smallAsteroidContainer;
    private GameObject asteroidParent;

    // Start is called before the first frame update
    void Start()
    {
        bigAsteroidContainer = new List<GameObject>();
        smallAsteroidContainer = new List<GameObject>();
        asteroidParent = new GameObject("Asteroid Container");
        CreateAsteroids();
    }

    void CreateAsteroids()
    {
        for(int i =0;i<noOfAsteroids;i++)
        {
            bigAsteroidContainer.Add(CreateAsteroid(bigAsteroidRef));
            smallAsteroidContainer.Add(CreateAsteroid(smallAsteroidRef));
            smallAsteroidContainer.Add(CreateAsteroid(smallAsteroidRef));
        }
    }

    private GameObject CreateAsteroid(GameObject objToCreate)
    {
        GameObject tempAst = Instantiate(objToCreate,asteroidParent.transform);
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

    public void HideAsteroids()
    {
        foreach(GameObject obj in bigAsteroidContainer)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in smallAsteroidContainer)
        {
            obj.SetActive(false);
        }
    }
}
