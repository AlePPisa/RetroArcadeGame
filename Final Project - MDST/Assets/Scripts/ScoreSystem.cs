using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textHighScore;
    [SerializeField] private int _scoreScale;
    [SerializeField] private HighscoreSystem _highscoreSystem;
    private GameManager _gameManager;
    private bool _isRecording = false;
    private int _extraScore = 0;
    private float _starTime = 0;
    private float _stopTime = 0;

    public void Start()
    {
        GameManager.OnGameStart += StartScore;
        GameManager.OnGameOver += StopScore;
    }

    public int GetScore()
    {
        return (int) Math.Floor((Time.time - _starTime) * _scoreScale) + _extraScore;
    }

    public void StartScore()
    {
        if (_isRecording) return;
        
        _isRecording = true;
        _starTime = Time.time;
    }

    public void UpdateHighScore()
    {
        string scoreText = _highscoreSystem.highscore.ToString();
        string leadingZeros = "";
        for (int i = 0; i < 6 - scoreText.Length; i++)
        {
            leadingZeros += "0";
        }
        
        _textHighScore.text = "High-score: " + leadingZeros + scoreText;
    }

    public void StopScore()
    {
        if (!_isRecording) return;
        
        _isRecording = false;
        _stopTime = Time.time;
    }

    public void AddScore(int points)
    {
        if (points < 0) return;
        
        _extraScore += points;
    }
    
    public void RemoveScore(int points)
    {
        if (points < 0) return;
        
        _extraScore = Math.Clamp(_extraScore - points, 0, Int32.MaxValue);
    }

    private void DisplayScore(int score)
    {
        string scoreText = score.ToString();
        string leadingZeros = "";
        for (int i = 0; i < 6 - scoreText.Length; i++)
        {
            leadingZeros += "0";
        }

        _textScore.text = "Score: " + leadingZeros + scoreText;

        // HighScore management
        if (_highscoreSystem.highscore > score) return;
        
        _highscoreSystem.highscore = score;
        _textHighScore.text = "High-score: " + leadingZeros + scoreText;
    }

    private void Update()
    {
        if (!_isRecording) return;
        
        DisplayScore((int) Math.Floor((Time.time - _starTime) * _scoreScale));
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= StartScore;
        GameManager.OnGameOver -= StopScore;
    }
}
