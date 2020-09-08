using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpSpeed = 28f;
    [SerializeField] float climbSpeed = 7f;
    float startingGravity;
    bool isAlive;

    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCollider2D;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    void Start()
    {
        isAlive = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        startingGravity = myRigidBody.gravityScale;

    }

    void Update()
    {
        if(isAlive)
        {
            Run();

            FlipSprite();

            Jump();

            Climb();
        }

        isTouchingEnemy();
    }

    private void Run()
    {
        float runSpeed = Input.GetAxis("Horizontal"); //[-1; 1]

        bool isPlayerMovingHorizontally = Mathf.Abs(runSpeed * playerSpeed) > Mathf.Epsilon;

        if(isPlayerMovingHorizontally)
        {
            myRigidBody.velocity = new Vector2(runSpeed * playerSpeed, myRigidBody.velocity.y);
            
        }

        myAnimator.SetBool("Run", isPlayerMovingHorizontally);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);

            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Climb()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myRigidBody.gravityScale = startingGravity;
            myAnimator.SetBool("Climb", false);
            return; 
        }
        
        float controlThrow = Input.GetAxis("Vertical"); //-1 +1
        Vector2 climbVector = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVector;
        myRigidBody.gravityScale = 0f;
        bool isClimbing = Mathf.Abs(myRigidBody.velocity.y) > 0;       
        myAnimator.SetBool("Climb", isClimbing);
        
    }

    private void FlipSprite()
    {
        bool isPlayerMovingHorizontally = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (isPlayerMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1);
        }
    }

    private void isTouchingEnemy()
    {
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            myRigidBody.velocity += new Vector2(25f, 10f);
            myAnimator.SetTrigger("Die");
            isAlive = false;
            FindObjectOfType<GameSession>().managePlayerDeath();
        }
    }
}

