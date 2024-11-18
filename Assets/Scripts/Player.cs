using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private float horizontalScreenLimit;
    private float verticalScreenLimit;

    public GameObject explosion;
    public GameObject bullet;
    private int lives;

    public GameObject shield;
    private bool hasShield;

    private int shooting;

    public GameManager gameManager;

    void Start()
    {
        speed = 6f;
        horizontalScreenLimit = 11.5f;
        verticalScreenLimit = 7.5f;
        lives = 3;
        hasShield = false;
        shooting = 1;
    }

    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        lives--;
        if (lives == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Powerup")
        {
            gameManager.PlayPowerUp();
            int powerupType = Random.Range(1, 5);
            switch (powerupType)
            {
                case 1:
                    speed = 9f;
                    gameManager.UpdatePowerupText("Picked up Speed!");
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    shooting = 2;
                    gameManager.UpdatePowerupText("Picked up Double Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 3:
                    shooting = 3;
                    gameManager.UpdatePowerupText("Picked up Triple Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 4:
                    gameManager.UpdatePowerupText("Picked up Shield!");
                    hasShield = true;
                    shield.SetActive(true);
                    break;
            }
            Destroy(whatIHit.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5f);
        speed = 6f;
    }

    IEnumerator ShootingPowerDown()
    {
        yield return new WaitForSeconds(5f);
        shooting = 1;
    }
}
