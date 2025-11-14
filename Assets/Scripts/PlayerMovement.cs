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
    [SerializeField] float runSpeed = 5f; // 좌우 이동 속도
    [SerializeField] float jumpSpeed = 5f; // 점프 초기 속도
    [SerializeField] float climbSpeed = 5f; // 사다리 등반 속도
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f); // 사망 시 반동

    [SerializeField] GameObject bullet; // 발사체 프리펩
    [SerializeField] Transform gun; // 총구 위치

    float customGravityScale = 8.0f; // 기본 중력 배수

    bool isAlive = true; // 사망 상태 플래그
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRender = GetComponent<SpriteRenderer>();

        // 프로젝트 기본 중력 대신 캐릭터 전용 중력 사용
        myRigidbody.gravityScale = customGravityScale;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die(); // 적/함정 접촉 체크
    }

    // Input System: Fire 액션
    void OnFire(InputValue value)
    {
        if (!isAlive) {return;}
        Instantiate(bullet,gun.position, transform.rotation);
    }

    // Input System: Move 액션 (Vector2)
    void OnMove(InputValue value)
    {   
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
    }

    // Input System: Jump 액션(누를 때 1회 처리)
    void OnJump(InputValue value)
    {   
        if (!isAlive) {return;}

        // 발이 Ground 레이어에 닿아있을 때만 점프 허용
        if (value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //do stuff 
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

    }

    /// <summary>
    /// 좌우 이동 및 달리기 애니메이션 제어
    /// </summary>
    void Run() 
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

    /// <summary>
    /// 이동 방향에 따라 스프라이트 좌우 반전
    /// </summary>
    void FlipSprite() // 캐릭터 좌우 조절
    {   
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; 
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f); 
        }
    }

    /// <summary>
    /// 사다리 레이어에 접촉 시 중력 0으로 등반 상태 전환
    /// </summary>
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

    /// <summary>
    /// 적/함정 레이어 접촉 시 사망 처리 + GameSession에 알림
    /// </summary>
    void Die()
    {   
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) 
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity  = deathKick; // 살짝 튀는 연출
            mySpriteRender.color = Color.red; // 피격 효과
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
       
    }
}
