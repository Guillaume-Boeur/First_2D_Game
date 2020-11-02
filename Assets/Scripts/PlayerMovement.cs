using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping = false;
    private bool isGrounded = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisonLayers;
   

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisonLayers);


        if (Input.GetButton("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        // animator.SetFloat() prend en paramètre 1 l'id de l'animator et en paramètre 2 la vélocité de l'axe x du rigidbody. Le problème c'est que si on va à gauche, la valeur est négative. Mathf.Abs() va rend cette valeur Absolue et donc toujours positive.
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }
    void FixedUpdate()
    {
        MovePlayer(horizontalMovement);
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
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

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
