using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody; // 탄환의 물리 컴포넌트
    PlayerMovement player; // 플레이어 참조
    [SerializeField] float bulletSpeed = 20f; // 탄환 속도
    float xSpeed; // x축 이동 속도
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();

        // 플레이어의 localScale.x(좌:-1 / 우:1)에 따라 발사 방향 설정
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {   
        // 일정 속도로 직진
        myRigidbody.velocity = new Vector2(xSpeed,0f);
    }

    // 트리거 충돌: 적을 맞추면 적 파괴, 그 외에도 총알은 소멸
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") 
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    
    // 물리 충돌(벽, 플랫폼 등) 시에도 총알 제거
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
