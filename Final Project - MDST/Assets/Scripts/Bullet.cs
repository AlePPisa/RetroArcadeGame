using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _initialBulletSpeed;
    [SerializeField] private float _timeBeetwenStuckChecks;
    [SerializeField] private float _stuckCriteria;
    [SerializeField] private AudioSource _bounceAudioSource;
    
    private Rigidbody2D _rb;
    private bool _isStuck = false;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.AddForce(_initialBulletSpeed * Random.insideUnitCircle.normalized);
        InvokeRepeating(nameof(IsStuck), _timeBeetwenStuckChecks, _timeBeetwenStuckChecks);
    }

    private void IsStuck()
    {
        _isStuck = Math.Abs(_rb.velocity.x) <= _stuckCriteria || Math.Abs(_rb.velocity.y) <= _stuckCriteria;
    }

    private void Update()
    {
        if (!_isStuck) return;
        _rb.AddForce(_initialBulletSpeed * Random.insideUnitCircle.normalized);
        _isStuck = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player"))
        {
            _bounceAudioSource.Play();
            return;
        }
        
        Destroy(gameObject);
    }
}
