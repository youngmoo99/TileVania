using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [SerializeField] float sceneLoadDelay = 1f; // 씬 전환 전 대기 시간(초)
    
    /// <summary>
    /// 메인 메뉴에서 "Start" 눌렀을 때 등: 레벨1 로드
    /// </summary>
    public void LoadGame()
    {
        StartCoroutine(WaitAndLoad("Level 1", sceneLoadDelay));
    }

    /// <summary>
    /// 메인 메뉴로 즉시 이동
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// 지정한 시간(delay)만큼 기다렸다가 sceneName 로드
    /// </summary>
    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
 
}
