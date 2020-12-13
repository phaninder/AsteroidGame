using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefabRef;
    [SerializeField] int noOfBullets = 30;
    private List<GameObject> bulletsContainer;
    private GameObject bulletParent;

    // Start is called before the first frame update
    void Start()
    {
        bulletsContainer = new List<GameObject>();
        bulletParent = new GameObject("Bullet Container");
        CreateBullets();
    }

    void CreateBullets()
    {
        for (int i = 0; i < noOfBullets; i++)
        {
            bulletsContainer.Add(CreateBullet());
        }
    }

    private GameObject CreateBullet()
    {
        GameObject tempAst = Instantiate(bulletPrefabRef,bulletParent.transform);
        tempAst.SetActive(false);
        return tempAst;
    }

    public GameObject GetBullet()
    {
        if (bulletsContainer != null && bulletsContainer.Count > 0)
        {
            foreach (GameObject obj in bulletsContainer)
            {
                if (!obj.activeInHierarchy)
                    return obj;
            }
        }
        //if there are no available bullets create a new one
        GameObject bullet = CreateBullet();
        bulletsContainer.Add(bullet);
        return bullet;
    }

}
