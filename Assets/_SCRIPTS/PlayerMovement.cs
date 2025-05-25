using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 100f;
    public float jumpForce = 5f;
    public float shortJumpMultiplier = 0.5f;
    private bool isJumping = false;

    private bool isGrounded;

    private bool doubleJumpUnlocked = false;
    private int jumpCount = 0;
    private int jumpCountMax = 1; // default value.

    public GameObject DoubleJumpPowerUpPrefab;

    public TextMeshProUGUI CoinText;
    public int coinScore = 0;

    //handling life management
    public int remainingLives = 3;
    public TextMeshProUGUI remainingLivesText;
    public float resetZone = -10;

    private Vector3 startPosition;

    //gun management
    private bool hasGun = false;
    private int maxAmmo = 30;
    private int currentAmmo = 0;
    public GameObject gunObject;
    //bullet and firepoint
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;
    public TextMeshProUGUI ammoText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        //remainingLivesText.text = "Lives: " + remainingLifes;
        startPosition = new Vector3(0, 1, 0);
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

        if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.y, jumpForce);
            isGrounded = false;
            isJumping = true;
            jumpCount++;
        }

        if(Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0 && isJumping)
        {
           rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * shortJumpMultiplier);
            isJumping = false;
        }

        if(transform.position.y <= resetZone)
        {
            LoseLifeAndReset();
        }

        //mouse movement with the gun
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 direction = mousePos - gunObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunObject.transform.rotation = Quaternion.Euler(0, 0, angle);

        //shooting mechanics
        if (hasGun && Input.GetMouseButtonDown(1) && currentAmmo > 0)
        {
            Shoot();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FragileBlock"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("DoubleJumpPowerUp"))
        {
            doubleJumpUnlocked = true;
            jumpCountMax = 2;
            Destroy(DoubleJumpPowerUpPrefab);
        }

        //spawn in a new level
        if (collision.gameObject.CompareTag("Portal"))
        {
            SceneManager.LoadScene("Level1");
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FragileBlock"))
        {
            isGrounded = false;
        }
    }

    void LoseLifeAndReset()
    {
        remainingLives--;

        if(remainingLives <= 0)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            coinScore = 0;
            remainingLivesText.text = "Lives: " + remainingLives;
        }
        else
        {
            
            transform.position = startPosition;
            remainingLivesText.text = "Lives: " + remainingLives;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("L1_Gun"))
        {
            hasGun = true;
            currentAmmo = maxAmmo;
            gunObject.SetActive(true);
            Destroy(collision.gameObject);
            UpdateAmmoUI(); // function that gets called that is yet to be made
            
        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        currentAmmo--;
        UpdateAmmoUI();

        if (currentAmmo <= 0)
        {
            gunObject.SetActive(false);
            hasGun = false;
        }    

    }

    void UpdateAmmoUI()
    {
        if (hasGun)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
        else 
        {

            ammoText.text = "";
        
        }

    }

}
