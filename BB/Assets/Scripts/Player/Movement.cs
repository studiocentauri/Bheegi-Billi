
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 10f;
    [SerializeField]private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 10f;

    private float CoyoteTime = 0.2f;
    private float CoyoteTimeCounter;
    private float JumpBufferTime = 0.2f;
    private float JumpBuferCounter;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 25f);
    public bool isDashtimer = false;
    private float isDashTime = 0.5f;
    private float dashtime = -0.1f;
    private Animator anime;
    private int Anime_State=0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck2;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField]private AudioSource jump_a;

    private void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void Update()
    {
       
        horizontal = Input.GetAxisRaw("Horizontal");
        if(isDashtimer)
        {
            dashtime = isDashTime;
            isDashtimer = false;
        }
        else
        {
            dashtime -= Time.deltaTime;
        }
        if(IsGrounded())
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump_a.Play();
            JumpBuferCounter = JumpBufferTime;
        }
        else
        {
            JumpBuferCounter -= Time.deltaTime; 
        }
        if (JumpBuferCounter>0f && CoyoteTimeCounter>0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            JumpBuferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            CoyoteTimeCounter = 0f;
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
           Flip();
        }
        setAnimeState();
        anime.SetInteger("state", Anime_State);
    }

    private void FixedUpdate()
    {
        if (!isWallJumping && dashtime<0f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        
    }

    private bool IsWalled()
    {
        return (Physics2D.OverlapCircle(wallCheck.position, 0.5f, wallLayer)|| Physics2D.OverlapCircle(wallCheck2.position, 0.5f, wallLayer));
    }

    private void WallSlide()
    {
        Debug.Log(IsWalled());
        if (IsWalled() && !IsGrounded() && (horizontal != 0f || dashtime>=0f))
        {   
            isWallSliding = true;
           
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void setAnimeState()
    {
        if (IsGrounded())
        {
            if (horizontal > 0)
            {
                Anime_State = 1;
            }
            else if (horizontal < 0)
            {
                Anime_State = -1;
            }
            else Anime_State = 0;
        }
        else if (!IsGrounded()|| IsWalled())
        {
            if (horizontal > 0)
            {
                Anime_State = 2;
            }
            else if (horizontal < 0)
            {
                Anime_State = -2;
            }
            else Anime_State = 0;
        }
    }
}