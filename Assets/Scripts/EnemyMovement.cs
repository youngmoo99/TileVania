using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    [SerializeField] float moveSpeed = 1f; // 좌우 순찰 속도
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {   
        // x축으로 일정 속도로 이동(순찰)
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    }

    // 경계 트리거(끝지점)를 벗어나면 방향 전환
    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed; // 속도 부호 반전
        FlipEnemyFacing();  // 좌우 스트라이프 뒤집기
    }

    // 현재 이동 방향에 맞게 캐릭터 좌우 반전
    void FlipEnemyFacing() 
    {  
        // velocity.x의 부호를 기준으로 x 스케일을 ±1로 설정
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);     
    }
}
