using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTransition : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnGameOver += Show;
        GameManager.OnGameStart += Hide;
    }

    private void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= Show;
        GameManager.OnGameStart -= Hide;
    }
}
