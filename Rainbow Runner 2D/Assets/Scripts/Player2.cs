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

    [SerializeField] bool isHoldingJump = false;
    [SerializeField] float maxHoldJumpTime = 0.4f;
    [SerializeField] float holdJumpTimer = 0.0f;
    [SerializeField] float jumpGrace = 1;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() /*&& Input.GetKeyDown(KeyCode.Space)*/)
        {       
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.up * jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
             
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        HandleMovement();
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;      

        if (!IsGrounded())
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += rb.velocity.y * Time.fixedDeltaTime;

            if (!isHoldingJump)
            {
                Vector2 velocity = rb.velocity;
                velocity.y += rb.gravityScale * Time.fixedDeltaTime;
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        //Debug.Log(raycastHit2D.collider);

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
