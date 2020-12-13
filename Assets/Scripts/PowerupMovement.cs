using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMovement : MonoBehaviour
{
    private float maxSpeed = 2f;
    [SerializeField] Rigidbody2D rbRef;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (rbRef == null)
        {
            rbRef = GetComponent<Rigidbody2D>();
        }

        yield return new WaitForSeconds(20);
        DestroyPowerup();
    }

    private void OnEnable()
    {
        float speedX = Random.Range(-maxSpeed, maxSpeed);
        float speedY = Random.Range(-maxSpeed, maxSpeed);

        if (rbRef == null)
        {
            rbRef = GetComponent<Rigidbody2D>();
        }

        rbRef.velocity = new Vector2(speedX, speedY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyPowerup();
        }
    }

    public void DestroyPowerup()
    {
        this.gameObject.SetActive(false);
    }
}
