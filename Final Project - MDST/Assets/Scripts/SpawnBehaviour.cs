using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SpawnBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioSource _flashAudioSource;
    [SerializeField] private AudioSource _shootAudioSource;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private Vector2 _minMaxInitialSpawnTime;
    [SerializeField] private float _blinkTime;
    [SerializeField] private int _numOfBlinks;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _spawnArea;
    [SerializeField] private Transform _wallTransform;
    private bool _isSpawning = true;
    
    private IEnumerator Start()
    {
        _spriteRenderer.enabled = false;
        yield return  new WaitForSeconds(Random.Range(_minMaxInitialSpawnTime.x, _minMaxInitialSpawnTime.y));
        _isSpawning = false;
    }

    private void Update()
    {
        if (_isSpawning) return;

        Vector3 size = _spawnArea.size * _wallTransform.localScale.x;
        Vector2 spawnPoint = new Vector3(Random.Range(0, size.x), Random.Range(0, size.y), 0) + _wallTransform.position - (size/2);
        transform.position = spawnPoint;
        
        StartCoroutine(SpriteBlink());
    }

    private IEnumerator SpriteBlink()
    {
        _isSpawning = true;
        
        for (int i = 0; i < _numOfBlinks; i++)
        {
            _flashAudioSource.Play();
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(_blinkTime / 2);
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(_blinkTime / 2);
        }
        
        _shootAudioSource.Play();
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(_timeBetweenSpawn);
        
        _isSpawning = false;
    }
}
