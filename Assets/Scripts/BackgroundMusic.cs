using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Awake()
    {
        BackgroundMusicSingleton();
    }
    
    /// <summary>
    /// 배경음 오브젝트를 싱글톤으로 유지.
    /// - 이미 존재한다면 새로 생긴 건 파괴
    /// - 처음 것 하나만 씬 전환에도 유지(DontDestroyOnLoad)
    /// </summary>


    void BackgroundMusicSingleton()
    {   
        // 현재 씬에 있는 타입(BackgroundMusic)의 인스턴스 개수
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if(instanceCount > 1)
        {   
            // 중복 방지: 잠깐 비활성화 후 자기 자신 제거
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else 
        {   
            // 첫 인스턴스만 씬이 바뀌어도 유지
            DontDestroyOnLoad(gameObject);
        }
    }
}
