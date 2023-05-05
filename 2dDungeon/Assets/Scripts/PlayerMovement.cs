using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float speed = 10f;
    private float jumpForce = 10f;

    private bool doubleJump;

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    private bool IsFacingRight = true;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animations();
        PlayerJump();   
        Flip();
    }

    private void FixedUpdate()
    {
        PlayerHorizontalMovement();
    }

    private void PlayerHorizontalMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        playerRigidbody.velocity = new Vector2(inputX * speed, playerRigidbody.velocity.y);
    }

    private void PlayerJump()
    {
        if(IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
        }

        if(Input.GetButtonUp("Jump") && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y * 0.5f);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if(IsFacingRight && Input.GetAxis("Horizontal") < 0 || IsFacingRight == false && Input.GetAxis("Horizontal") > 0)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void Animations()
    {
        playerAnimator.SetBool("IsMoving", Input.GetAxis("Horizontal") != 0);
        playerAnimator.SetBool("IsJumping", IsGrounded() == false && playerRigidbody.velocity.y != 0);
    }
}
