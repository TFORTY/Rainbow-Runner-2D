using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    // ADJUST HEIGHT OF MAX JUMP

    [SerializeField] LayerMask platformsLayerMask;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player in the x direction
        transform.position += new Vector3(GameManager.Instance.GetCameraSpeed() * Time.deltaTime, 0, 0) * GameManager.Instance.GetSpeedModifier();

        jumpPressed = Input.GetKeyDown(KeyCode.Space);

        if (jumpPressed)
        {
            jumpTimer = Time.time;
            Debug.Log("Jump Pressed");
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
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.DrawRay(boxCollider2D.bounds.center, (boxCollider2D.bounds.size * Vector2.down), Color.red);

        return raycastHit2D.collider != null;
    }

    public float GetDistance()
    {
        return distance;
    }
}
