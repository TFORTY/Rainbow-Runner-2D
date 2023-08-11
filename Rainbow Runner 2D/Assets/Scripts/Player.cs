using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;
   
    public bool isHoldingJump = false; 
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGrace = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        // Check if the player is on the ground
        // Also check if the player is close to the ground to input another jump 
        if (isGrounded || groundDistance <= jumpGrace)
        {
            // If the player is on the ground, they can jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }

        // Checks if the player is not holding jump, so apply gravity
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        // If the player is not on the ground, they have jumped, apply upwards velocity
        if (!isGrounded)
        {
            // Player is holding jump, create a higher jump for a short period of time
            // If the timer exceeds the duration, bring them back down
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;

            // Normal mini jump
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;

            }
            // Checks if the player has collided with the ground
            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        // Update the position of the player
        transform.position = pos;
    }
}
