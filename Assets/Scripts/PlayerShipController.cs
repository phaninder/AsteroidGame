using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5;
    [SerializeField] Rigidbody2D shipRB;
    [SerializeField] float boostForce = 50;
    [SerializeField] float maxVelocity = 10;
    [SerializeField] GameObject bulletRef;
    [SerializeField] Transform bulletStartPos;
    [SerializeField] Transform bulletPowerUpStartPos1;
    [SerializeField] Transform bulletPowerUpStartPos2;
    [SerializeField] BulletCreator bulletManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject shieldObject;

    private bool shootPowerup = false;
    private bool shieldActive = false;
    [SerializeField] float shieldTime = 8;
    [SerializeField] float shootPowerTime = 8;
    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager");
        shipRB = GetComponent<Rigidbody2D>();
        if (gameManager != null)
        {
            if (bulletManager == null)
            {
                bulletManager = gameManager.GetComponent<BulletCreator>();
            }

            if (uiManager == null)
            {
                uiManager = gameManager.GetComponent<UIManager>();
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.GameStartAction += ResetShip;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameStartAction -= ResetShip;
    }

    void ResetShip()
    {
        //Set at start position
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        shipRB.velocity = Vector2.zero;
        EnableShield(2);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        float rot = Input.GetAxis("Horizontal") * rotationSpeed;
        rot *= Time.deltaTime;
        transform.Rotate(0, 0, -rot);

        if (Input.GetButtonDown("Accelerate"))// && CheckVelocity())
        {
            //shipRB.AddForce(transform.up * boostForce, ForceMode2D.Impulse);
            shipRB.velocity = transform.up * boostForce;
        }

        if (Input.GetButtonDown("Jump"))
        {
            CreateBullets();
        }
    }

    void CreateBullets()
    {
        if (!shootPowerup)
        {
            GameObject bullet = bulletManager.GetBullet();//Instantiate(bulletRef, bulletStartPos.position, transform.rotation);
            bullet.transform.position = bulletStartPos.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<BulletMovement>().SetDir(transform.up);
            bullet.SetActive(true);
        }
        else
        {
            //Bullet 1
            GameObject bullet1 = bulletManager.GetBullet();//Instantiate(bulletRef, bulletStartPos.position, transform.rotation);
            bullet1.transform.position = bulletPowerUpStartPos1.position;
            bullet1.transform.rotation = transform.rotation;
            bullet1.GetComponent<BulletMovement>().SetDir(transform.up);
            bullet1.SetActive(true);

            //Bullet 2
            GameObject bullet2 = bulletManager.GetBullet();
            bullet2.transform.position = bulletPowerUpStartPos2.position;
            bullet2.transform.rotation = transform.rotation;
            bullet2.GetComponent<BulletMovement>().SetDir(transform.up);
            bullet2.SetActive(true);

        }
    }

    bool CheckVelocity()
    {
        if (shipRB.velocity.magnitude > maxVelocity)
        {
            return false;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            if (!shieldActive)
            {
                GameManager.Instance.DeductLive();
                ResetShip();
            }
        }
        else if(collision.gameObject.tag == "ShootPowerUp")
        {
            shootPowerup = true;
            StartCoroutine(DisablePowerShootPowerup(shootPowerTime));
        }
        else if (collision.gameObject.tag == "ShieldPowerup")
        {
            EnableShield(shieldTime);
        }
    }

    private IEnumerator DisablePowerShootPowerup(float time)
    {
        yield return new WaitForSeconds(time);
        shootPowerup = false;
    }

    private IEnumerator DisableShieldShootPowerup(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            shieldObject.GetComponent<SpriteRenderer>().enabled = !shieldObject.GetComponent<SpriteRenderer>().enabled;
        }

        shieldObject.SetActive(false);
        shieldObject.GetComponent<SpriteRenderer>().enabled = true;
        shieldActive = false;
    }

    private void EnableShield(float time)
    {
        shieldObject.SetActive(true);
        shieldActive = true;
        StartCoroutine(DisableShieldShootPowerup(time));
    }
}
