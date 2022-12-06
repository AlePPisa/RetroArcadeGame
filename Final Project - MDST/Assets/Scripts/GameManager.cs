using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScoreSystem))]
public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    public static GameState gameState { get; private set; }
    private ScoreSystem _scoreSystem;

    public static event Action OnGameStart;
    public static event Action OnGameOver;
    public static event Action OnPlayerHit;
    public static event Action OnGameRestart;
    
    public enum GameState
    {
        inGame,
        gameOver,
        preGame,
    }

    public enum Event
    {
        gameStart,
        gameOver,
        playerHit,
        gameRestart,
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
        gameState = GameState.preGame;
        _scoreSystem = GetComponent<ScoreSystem>();
    }

    private void Start()
    {
        DontDestroyOnLoad(Instance.gameObject);
        StartGame();
    }

    private static void StartGame()
    {
        gameState = GameState.inGame;
        BroadcastEvent(Event.gameStart);
    }

    public static void StopGame()
    {
        gameState = GameState.gameOver;
        BroadcastEvent(Event.gameOver);
    }

    public static void RestartGame()
    {
        gameState = GameState.preGame;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        BroadcastEvent(Event.gameRestart);
    }

    public static void BroadcastEvent(Event e)
    {
        switch (e)
        {
            case Event.gameStart:
            {
                GameManager.OnGameStart?.Invoke();
                break;
            }
            case Event.gameOver:
            {
                GameManager.OnGameOver?.Invoke();
                break;
            }
            case Event.playerHit:
            {
                GameManager.OnPlayerHit?.Invoke();
                break;
            }
            case Event.gameRestart:
                GameManager.OnGameRestart?.Invoke();
                break;
        }
    }
}
