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
    CapsuleCollider2D myCollider;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
}
