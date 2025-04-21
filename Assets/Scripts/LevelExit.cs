using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{   
    [SerializeField] float levelLoadDelay = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel()); //씬을 바로 바꾸지않고 코루틴(시간 지연같은 처리를 하기 위한 비동기 함수)을 사용해서 기다렸다가 씬을 전환
        }
        
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // 마지막 씬이라면 (씬 총 개수와 같다면)
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            string resultSceneName = "GameClear"; // ← 네 결과 씬 이름으로 바꿔줘!
            StartCoroutine(LoadResultSceneAndDestroySession(resultSceneName));
        }
        else
        {
            FindObjectOfType<ScenePersist>()?.ResetScenePersist();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    IEnumerator LoadResultSceneAndDestroySession(string resultSceneName)
    {
        // 결과 씬 로드
        SceneManager.LoadScene(resultSceneName);

        // UIResult.cs의 Start()가 실행되도록 프레임 1번 대기
        yield return null;

        // GameSession 안전하게 파괴
        if (GameSession.Instance != null)
        {
            Destroy(GameSession.Instance.gameObject);
        }

        // ScenePersist도 함께 제거 (있다면)
        ScenePersist persist = FindObjectOfType<ScenePersist>();
        if (persist != null)
        {
            Destroy(persist.gameObject);
        }
    }
    
}
