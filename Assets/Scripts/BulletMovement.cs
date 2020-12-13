using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public Vector2 moveDir;

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;
        StartCoroutine(DisableAfterFewSeconds());
    }

    public void SetDir(Vector2 dir)
    {
        moveDir = dir;
    }

    IEnumerator DisableAfterFewSeconds()
    {
        yield return new WaitForSeconds(4);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            this.gameObject.SetActive(false);
        }
    }
}
