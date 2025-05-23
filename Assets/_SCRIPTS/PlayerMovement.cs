using Unity.VisualScripting;
using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
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


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FragileBlock"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has been hig! -1 life");
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
        if (collision.gameObject.CompareTag("Ground") ||collision.gameObject.CompareTag("FragileBlock"))
        {
            isGrounded = false;
        }
    }


}
