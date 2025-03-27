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
        yield return new WaitForSecondsRealtime(levelLoadDelay); // 실제 시간으로 levelLoadDelay초 기다림
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // 마지막 단계면 1단계로 변경
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex); //다음 씬 이동
    }
    
}
