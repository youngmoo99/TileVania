using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{   
    [SerializeField] AudioClip coinPickupSFX; // 코인 획득 사운드
    [SerializeField] int pointsForCoinPickup = 100; // 코인 점수

    bool wasCollected = false; // 중복 수집 방지 플래그

    // 플레이어가 코인에 닿았을때
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {   
            wasCollected = true; // 한 번만 처리

            // 점수 추가
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);

            // 카메라 위ㅣㅊ에서 사운드 재생
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);

            // 시각적/물리적 상호작용 즉시 차단 후 파괴
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
