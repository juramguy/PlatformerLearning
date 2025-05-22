using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    public float moveSpeed = 10f;

    private Rigidbody2D rb;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocityY) * Time.deltaTime;
        }


    }


}
