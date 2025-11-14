using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{   
    // 전역 접근 가능한 싱글톤
    public static GameSession Instance { get; private set; }

    [SerializeField] int playersLives = 3; // 남은 목숨
    [SerializeField] int score = 0; // 현재 점수
    [SerializeField] TextMeshProUGUI livesText; // HUD: 목숨 텍스트
    [SerializeField] TextMeshProUGUI scoreText;  // HUD: 점수 텍스트

    void Awake()
    {   
        // 싱글톤 보장 & 씬 이동 시 유지
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
        // 씬이 바뀔 때마다 HUD 레퍼런스를 다시 연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 새 씬 로드 직후 HUD 오브젝트를 태그로 찾아 재바인딩
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되면 UI 다시 찾아 연결
        livesText = GameObject.FindWithTag("LivesText")?.GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    // HUD 텍스트 갱신
    void UpdateUI()
    {
        if (livesText != null)
            livesText.text = playersLives.ToString();

        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    /// <summary>
    /// 플레이어 사망 로직 진입(목숨 차감 or 게임오버)
    /// </summary>
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

    // 목숨 1 깎고 현재 씬 재시작
    void TakeLife()
    {
        playersLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UpdateUI();
    }

    // 게임 오버 처리: 결과 씬으로 이동 후 세션 종료
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

    /// <summary>
    /// 점수 가산 및 HUD 갱신
    /// </summary>
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
    
    /// <summary>
    /// 메인 메뉴 복귀 등에서 세션 수치 초기화(오브젝트는 유지)
    /// </summary>
    public void ResetSession()
    {
        playersLives = 3;
        score = 0;
        UpdateUI();
    }
}