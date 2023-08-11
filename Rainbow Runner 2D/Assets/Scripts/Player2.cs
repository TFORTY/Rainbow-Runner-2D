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

    [SerializeField] float gravity;
    [SerializeField] bool isHoldingJump = false;
    [SerializeField] float maxHoldJumpTime = 0.4f;
    [SerializeField] float holdJumpTimer = 0.0f;

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
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }

        HandleMovement();
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        //Debug.Log(raycastHit2D.collider);

        return raycastHit2D.collider != null;
    }

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
}
