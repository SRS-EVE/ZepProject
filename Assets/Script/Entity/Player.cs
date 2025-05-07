using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpHeight = 1.5f;
    public float jumpDuration = 0.3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;

    private bool isJumping = false;
    private bool isGrounded = true;
    private float jumpTimer = 0f;
    private float jumpOffset = 0f;
    private float jumpBaseY = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.x != 0)
            spriteRenderer.flipX = moveInput.x < 0;

        if (!isJumping && !isGrounded)
            isGrounded = true;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
            jumpTimer = 0f;
            jumpBaseY = rb.position.y; // 현재 Y 위치 저장 (복귀용)
        }
    }

    void FixedUpdate()
    {
        // 기본 이동 계산
        Vector2 move = moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
        Vector2 basePos = rb.position + move;

        // 점프 중이라면 Y 위치를 jumpBaseY 기준으로 조절
        if (isJumping)
        {
            jumpTimer += Time.fixedDeltaTime;
            float t = jumpTimer / jumpDuration;

            if (t >= 1f)
            {
                isJumping = false;
                jumpOffset = 0f;
                isGrounded = true;
            }
            else
            {
                jumpOffset = Mathf.Sin(t * Mathf.PI) * jumpHeight;
            }

            // jumpBaseY 기준으로 위아래 연출
            basePos.y = jumpBaseY + jumpOffset;
        }

        rb.MovePosition(basePos);
    }
}
