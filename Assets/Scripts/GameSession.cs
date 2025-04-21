using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    [SerializeField] int playersLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되면 UI 다시 찾아 연결
        livesText = GameObject.FindWithTag("LivesText")?.GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (livesText != null)
            livesText.text = playersLives.ToString();

        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playersLives > 1)
        {
            TakeLife();
        }
        else
        {
            RestGameSession(); // 게임 오버
        }
    }

    void TakeLife()
    {
        playersLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UpdateUI();
    }

    void RestGameSession()
    {
        StartCoroutine(GoToGameOverScene());
    }

    IEnumerator GoToGameOverScene()
    {
        SceneManager.LoadScene("GameOver"); // ← 결과 씬 이름 맞게 설정
        yield return null; // 씬 로딩 이후 한 프레임 기다렸다가
        Destroy(gameObject); // GameSession 제거
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLives()
    {
        return playersLives;
    }

    public void ResetSession()
    {
        playersLives = 3;
        score = 0;
        UpdateUI();
    }
}