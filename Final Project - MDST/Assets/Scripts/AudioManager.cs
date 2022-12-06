using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _gameOverMusic;
    [SerializeField] private AudioSource _gameMusic;

    private void Awake()
    {
        GameManager.OnGameOver += PlayGameOver;
        GameManager.OnGameOver += StopGameTheme;
        GameManager.OnGameStart += PlayGameTheme;
    }

    private void PlayGameOver()
    {
        StartCoroutine(WaitPlayGameOver());
    }

    private void StopGameTheme()
    {
        _gameMusic.Stop();
    }

    private void PlayGameTheme()
    {
        _gameMusic.Play();
    }

    IEnumerator WaitPlayGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        _gameOverMusic.Play();
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= PlayGameOver;
        GameManager.OnGameOver -= StopGameTheme;
        GameManager.OnGameStart -= PlayGameTheme;
    }
}
