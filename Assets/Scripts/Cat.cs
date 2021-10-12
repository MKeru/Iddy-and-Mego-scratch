using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cat : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpSpeed = 8f;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    const float groundCheckRadius = 0.016f;
    [SerializeField] bool isGrounded;

    float horizontalValue;
    bool facingRight = true;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /*
    void Awake()
    {
        
    }
    */

    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        
        if (horizontalValue > 0f) {
            rb.velocity = new Vector2(horizontalValue * speed, rb.velocity.y);
        }
        else if (horizontalValue < 0f) {
            rb.velocity = new Vector2(horizontalValue * speed, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        //Debug.Log(horizontalValue);
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);
    }

    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] coll = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(coll.Length > 0)
            isGrounded = true;
    }

    /*
    bool isGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null) {
            return true;
            grounded = true;
        }

        return false;
        grounded = false;
    }
    */

    void Move(float dir)
    {
        float xVal = dir * speed * Time.fixedDeltaTime * 100;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        //turning
        Vector3 currentScale = transform.localScale;
        if (facingRight && dir < 0) {
            currentScale.x *= -1;
            facingRight = false;
        }
        else if (!facingRight && dir > 0) {
            currentScale.x = Math.Abs(currentScale.x);
            facingRight = true;
        }
        transform.localScale = currentScale;
    }
}
