using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] LayerMask platformsLayerMask;

    private Rigidbody2D rb;
    [SerializeField] float jumpVelocity = 10f;
    private BoxCollider2D boxCollider2D;

    [SerializeField] float moveSpeed = 40f;

    private bool pressedJump = false;
    private bool releasedJump = false;
    [SerializeField] float gravityScale = 1;

    [SerializeField] float jumpTimer = 0.5f;
    private bool startTimer = false;
    private float timer;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        timer = jumpTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {         
            pressedJump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            releasedJump = true;
        }

        if (startTimer)
        {
            timer -= Time.deltaTime;   
            if (timer <= 0)
            {
                releasedJump = true;
            }
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {                
            rb.velocity = Vector2.up * jumpVelocity;                     
        }

        HandleMovement();
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
        timer = jumpTimer;
        startTimer = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        //Debug.Log(raycastHit2D.collider);
        Debug.DrawRay(boxCollider2D.bounds.center, (boxCollider2D.bounds.size * Vector2.down), Color.red);

        return raycastHit2D.collider != null;
    }

    #region Basic Movement
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {                     
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);           
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    #endregion
}
