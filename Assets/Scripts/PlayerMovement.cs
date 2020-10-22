using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping = false;
    private bool isGrounded = false;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;



    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position); // Fait un collider entre les 2 groundCheck et au moment du saut, vérifie si il est en collision avec un autre collider ou non.
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButton("Jump") && isGrounded)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

}
