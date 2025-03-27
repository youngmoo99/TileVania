using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;

    BoxCollider2D myFeetCollider;

    SpriteRenderer mySpriteRender;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    float customGravityScale = 8.0f;

    bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        myRigidbody.gravityScale = customGravityScale;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    void OnFire(InputValue value)
    {
        if (!isAlive) {return;}
        Instantiate(bullet,gun.position, transform.rotation);
    }
    void OnMove(InputValue value)
    {   
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {   
        if (!isAlive) {return;}
        if(value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //do stuff 
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }

    }

    void Run() //캐릭터 움직이기
    {   
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity; 
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; 
        if (playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("isRunning",true);
        } 
        else 
        {
            myAnimator.SetBool("isRunning",false);
        }


    }
    void FlipSprite() // 캐릭터 좌우 조절
    {   
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; 
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f); 
        }
    }

    void ClimbLadder() 
    {   
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbVelocity; 
            myRigidbody.gravityScale = 0f;
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon; 
            myAnimator.SetBool("isClimbing",playerHasHorizontalSpeed);

        } 
        else 
        {
            myRigidbody.gravityScale = customGravityScale;
            myAnimator.SetBool("isClimbing",false);
        }

    }

    void Die()
    {   
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) 
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity  = deathKick;
            mySpriteRender.color = Color.red;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
       
    }
}
