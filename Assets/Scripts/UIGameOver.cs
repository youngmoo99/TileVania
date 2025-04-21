using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreReusltText;
 

    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if(gameSession != null)
        {
            scoreReusltText.text = "SCORE "+gameSession.GetScore().ToString();
        }
    }

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
