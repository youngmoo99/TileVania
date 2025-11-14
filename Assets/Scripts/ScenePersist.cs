using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 한 스테이지 동안 유지되어야 하는 오브젝트(예: 열쇠/문 상태)를
/// 씬 전환 시 파괴하지 않기 위한 헬퍼.
/// LevelExit에서 다음 씬 로드시 ResetScenePersist()로 수동 제거.
/// </summary>
public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1)
        {   
            // 중복 생성 방지
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
