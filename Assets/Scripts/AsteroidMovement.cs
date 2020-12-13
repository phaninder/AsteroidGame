using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private float maxSpeed = 2f;
    private ObstacleManager obstaclesManagerRef;
    [SerializeField] bool smallAsteroid;
    [SerializeField] Rigidbody2D rbRef;
    // Start is called before the first frame update
    void Start()
    {
        if(rbRef == null)
        {
            rbRef = GetComponent<Rigidbody2D>();
        }
        
    }

    public void SetReference(ObstacleManager obstacleManager,float maxSpeed)
    {
        obstaclesManagerRef = obstacleManager;
        this.maxSpeed = maxSpeed;
    }

    private void OnEnable()
    {
        float speedX = Random.Range(-maxSpeed, maxSpeed);
        float speedY = Random.Range(-maxSpeed, maxSpeed);
        
        if(rbRef == null)
        {
            rbRef = GetComponent<Rigidbody2D>();
        }

        rbRef.velocity = new Vector2(speedX, speedY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            DestroyAsteroid();
        }
    }

    public void DestroyAsteroid()
    {
        if(obstaclesManagerRef != null)
        {
            //update asteroid destruction to manager
            obstaclesManagerRef.UpdateObstacles(this.transform.position,!smallAsteroid,maxSpeed);
        }
        this.gameObject.SetActive(false);
    }
}
