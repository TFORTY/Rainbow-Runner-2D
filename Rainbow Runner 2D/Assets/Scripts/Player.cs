using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    // ADJUST HEIGHT OF MAX JUMP

    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask redLayerMask;
    [SerializeField] LayerMask blueLayerMask;
    [SerializeField] LayerMask yellowLayerMask;

    private Rigidbody2D rb;
    [SerializeField] float jumpVelocity = 10f;
    private BoxCollider2D boxCollider2D;

    [Header("Jumping")]
    private bool pressedJump = false;
    private bool releasedJump = false;
    [SerializeField] float gravityScale = 1;
    [SerializeField] float maxJumpTime = 0.5f;
    private bool startTimer = false;
    private float maxJumpTimer;
    private bool jumpPressed;
    private float jumpTimer;
    private float jumpGracePeriod = 0.2f;

    private float distance;
    public float Distance { get { return distance; } set { distance = value; } }

    private SpriteRenderer spriteRenderer;
    private bool isRed;
    private bool isColourRed;
    private bool isBlue;
    private bool isColourBlue;
    private bool isYellow;
    private bool isColourYellow;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        maxJumpTimer = maxJumpTime;

        spriteRenderer = GetComponent<SpriteRenderer>();

        isRed = false;
        isColourRed = false;
        isBlue = false;
        isColourBlue = false;
        isYellow = false;
        isColourYellow = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.StartGame)
        {
            // Move the player in the x direction
            transform.position += new Vector3(GameManager.Instance.CameraSpeed * Time.deltaTime, 0, 0) * GameManager.Instance.SpeedModifier;
        }

        jumpPressed = Input.GetKeyDown(KeyCode.Space);

        if (jumpPressed)
        {
            jumpTimer = Time.time;
        }

        if (IsGrounded() && (jumpPressed || (jumpTimer > 0 && Time.time < jumpTimer + jumpGracePeriod)))
        {         
            pressedJump = true;
            jumpTimer = -1;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            releasedJump = true;
        }

        if (startTimer)
        {
            maxJumpTimer -= Time.deltaTime;   
            if (maxJumpTimer <= 0)
            {
                releasedJump = true;
            }
        }

        ChangeColour();
        CheckPlatformCollisions();

        // Checks if player has collided with RED layer
        isRed = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 5f, redLayerMask);
        // Checks if player has collided with BLUE layer
        isBlue = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 5f, blueLayerMask);
        // Checks if player has collided with YELLOW layer
        isYellow = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 5f, yellowLayerMask);

        if (GameManager.Instance.IsRainbowPowerup)
        {
            Physics2D.IgnoreLayerCollision(3, 7, false);
            Physics2D.IgnoreLayerCollision(3, 8, false);
            Physics2D.IgnoreLayerCollision(3, 9, false);
        }
    }

    private void FixedUpdate()
    {
        if (pressedJump)
        {
            StartJump();
        }

        if (releasedJump)
        {
            StopJump();
        }

        distance += transform.position.x * Time.fixedDeltaTime;
    }

    private void StartJump()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.up * jumpVelocity;
        pressedJump = false;
        startTimer = true;
    }

    private void StopJump()
    {
        rb.gravityScale = gravityScale;
        releasedJump = false;
        maxJumpTimer = maxJumpTime;
        startTimer = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, groundLayerMask);
        Debug.DrawRay(boxCollider2D.bounds.center, (boxCollider2D.bounds.size * Vector2.down), Color.red);

        return raycastHit2D.collider != null;
    }
    private void ChangeColour()
    {     
        if (!GameManager.Instance.IsPaused)
        {
            if (Input.GetKeyDown(KeyCode.I)) // RED
            {
                isColourRed = true;
                isColourBlue = false;
                isColourYellow = false;
                spriteRenderer.color = Color.red;
            }
            else if (Input.GetKeyDown(KeyCode.O)) // BLUE
            {
                isColourBlue = true;
                isColourRed = false;
                isColourYellow = false;
                spriteRenderer.color = Color.blue;
            }
            else if (Input.GetKeyDown(KeyCode.P)) //YELLOW
            {
                isColourYellow = true;
                isColourRed = false;
                isColourBlue = false;
                spriteRenderer.color = Color.yellow;
            }
        }       
    }

    public void CheckPlatformCollisions()
    {
        // LAYERS
        // 3 - Player
        // 6 - Platforms
        // 7- Red
        // 8 - Blue
        // 9 - Yellow

        if (isColourRed && isRed)
        {
            Physics2D.IgnoreLayerCollision(3, 7, false);
        }
        else if (!isColourRed && isRed)
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
        }
       
        if (isColourBlue && isBlue)
        {
            Physics2D.IgnoreLayerCollision(3, 8, false);
        }
        else if (!isColourBlue && isBlue)
        {
            Physics2D.IgnoreLayerCollision(3, 8, true);
        }

        if (isColourYellow && isYellow)
        {
            Physics2D.IgnoreLayerCollision(3, 9, false);
        }
        else if (!isColourYellow && isYellow)
        {
            Physics2D.IgnoreLayerCollision(3, 9, true);
        }       
    }
}
