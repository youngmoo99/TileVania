using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playersLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText; //목숨 
    [SerializeField] TextMeshProUGUI scoreText; //점수

    void Awake() 
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) 
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playersLives.ToString();    
        scoreText.text = score.ToString();
    }

    void Update()
    {
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
            RestGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife() //플레이어 목숨 감소 원래 진행중인 스테이지 진행
    {
        playersLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playersLives.ToString(); 
    }

    void RestGameSession() // 첫번째 스테이지 진행
    {   
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0); 
        Destroy(gameObject);
    }
}
