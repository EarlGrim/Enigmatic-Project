using UnityEngine;

public class TestingSprite : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 7f;
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;
    public Transform cameraTransform;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity completely
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY; // Lock vertical movement
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get movement input (W/S now act like A/D for left/right movement)
        float moveX = Input.GetAxis("Horizontal");
        float moveWS = Input.GetAxis("Vertical");
        float finalMoveX = moveX + moveWS; // Combine A/D and W/S inputs
        movement = new Vector2(finalMoveX, 0).normalized; // Restrict to horizontal only

        // Handle dashing
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashTime;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
                isDashing = false;
        }

        // Flip sprite based on movement direction
        if (finalMoveX != 0)
        {
            spriteRenderer.flipX = finalMoveX < 0;
        }
    }

    void FixedUpdate()
    {
        // Determine speed based on input
        float currentSpeed = isDashing ? dashSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed);

        // Apply movement using Rigidbody2D with linearVelocity (horizontal only)
        rb.linearVelocity = new Vector2(movement.x * currentSpeed, 0);
    }
}
