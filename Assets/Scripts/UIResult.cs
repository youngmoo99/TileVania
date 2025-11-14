using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreReusltText; // 최종 점수
    [SerializeField] TextMeshProUGUI livesResultText; // 남은 목숨

    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if(gameSession != null)
        {
            scoreReusltText.text = "SCORE "+gameSession.GetScore().ToString();
            livesResultText.text = "LIFE "+gameSession.GetLives().ToString();
        }
    }

    // 재시작 버튼
    public void OnClickRestartGame()
    {
        // GameSession 삭제
        if (GameSession.Instance != null)
        {
            Destroy(GameSession.Instance.gameObject);
        }

        // ScenePersist 삭제 (있다면)
        ScenePersist persist = FindObjectOfType<ScenePersist>();
        if (persist != null)
        {
            Destroy(persist.gameObject);
        }

        SceneManager.LoadScene("Level 1"); // 다시 첫 스테이지로
    }

    // 메인 메뉴 버튼
    public void OnClickMainMenu()
    {
        if (GameSession.Instance != null)
        {
            Destroy(GameSession.Instance.gameObject);
        }

        ScenePersist persist = FindObjectOfType<ScenePersist>();
        if (persist != null)
        {
            Destroy(persist.gameObject);
        }

        SceneManager.LoadScene("MainMenu");
    }
}
