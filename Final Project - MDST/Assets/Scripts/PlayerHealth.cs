using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI _text;
    private bool _isDead = false;
    public event Action OnLastLife;

    private void Start()
    {
        GameManager.OnPlayerHit += RemoveHealth;
        DisplayHealth(_health);
    }

    private void RemoveHealth()
    {
        if (_isDead) return; 
        
        _health = Math.Clamp(_health - 1, 0, Int32.MaxValue);

        if (_health <= 0)
        {
            _isDead = true;
            GameManager.StopGame();
        }

        if (_health == 1)
        {
            OnLastLife?.Invoke();
        }
        
        DisplayHealth(_health);
    }
    
    private void DisplayHealth(int health)
    { 
        _text.text = "Lives: " + health;
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerHit -= RemoveHealth;
    }
}
