﻿using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public LayerMask gorundLayer;
    public Animator animator;
    public Transform groundChecker;

    public float playerSpeed;
    public float verticalPlayerSpeed = 5;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpFallMultiplier;
    public float groundCheckRange = 0.5f;

    bool jumpPressed;
    public bool enableShortJump = true;
    float horizontalMovement;

    // Start is called before the first frame update
    void Start()
    {
        fallMultiplier = 2f;
        playerSpeed = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        BetterFall();
        MovePlayer();
    }

    void CheckInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
    }

    void MovePlayer()
    {
        var yMovement = playerBody.velocity.y;

        if (jumpPressed && IsGrounded())
        {
            yMovement += jumpForce;
        }

        animator.SetFloat("Speed", Math.Abs(horizontalMovement));
        var playerPosition = horizontalMovement * playerSpeed * Time.deltaTime;
        playerBody.velocity = new Vector2(playerPosition, yMovement);
    }

    void BetterFall()
    {
        // czy nasz player spada?
        if (playerBody.velocity.y < 0 && enableShortJump)
        {
            //fallMultiplier
            playerBody.velocity += Vector2.up * Physics2D.gravity * fallMultiplier * Time.deltaTime;
        }
        // player leci do góry, ale spacja nie jest wciśnięta
        else if (playerBody.velocity.y > 0 && !jumpPressed)
        {
            playerBody.velocity += Vector2.up * Physics2D.gravity * lowJumpFallMultiplier * Time.deltaTime;
            //lowJumpFallMultiplier
        }
    }

    bool IsGrounded() => Physics2D.OverlapCircle(groundChecker.position, groundCheckRange, gorundLayer) != null;

}
