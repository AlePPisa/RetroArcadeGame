using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private AudioSource _hitAudioSource;
    [SerializeField] private AudioSource _finalHitAudioSource;
    [SerializeField] private PlayerHealth _playerHealth;
    private Vector2 _moveDir;
    private Rigidbody2D _rb;
    private bool _lastLife = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerHealth.OnLastLife += ToggleLastLife;
    }

    void OnMove(InputValue value)
    {
        if (GameManager.gameState != GameManager.GameState.inGame) return;
        _moveDir = value.Get<Vector2>();
    }

    void ToggleLastLife()
    {
        _lastLife = !_lastLife;
    }

    private void Update()
    {
        if (_moveDir == Vector2.zero) return;
        
        _rb.AddForce(_moveDir*_moveSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Damager"))
        {
            if (_lastLife)
            {
                _finalHitAudioSource.Play();
            }
            else
            {
                _hitAudioSource.Play();
            }
            
            GameManager.BroadcastEvent(GameManager.Event.playerHit);
        }
    }

    void OnEnter(InputValue value)
    {
        if (GameManager.gameState != GameManager.GameState.gameOver) return;
        GameManager.RestartGame();
    }
    private void OnDestroy()
    {
        _playerHealth.OnLastLife += ToggleLastLife;
    }
}
