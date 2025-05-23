using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    public float moveSpeed = 100f;
    public float jumpForce = 5f;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        //remainingLivesText.text = "Lives: " + remainingLifes;
        startPosition = new Vector3(0, 1, 0);
        
    }

    // Update is called once per frame
    void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.y, jumpForce);
            isGrounded = false;
            jumpCount++;
        }

        if(transform.position.y <= resetZone)
        {
            LoseLifeAndReset();
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
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            coinScore = 0;
            remainingLivesText.text = "Lives: " + remainingLives;
        }
        else
        {
            Debug.Log("Respawning...");
            transform.position = startPosition;
            remainingLivesText.text = "Lives: " + remainingLives;
        }

    }



}
