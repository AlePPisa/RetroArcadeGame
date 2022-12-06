using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallShrink : MonoBehaviour
{
    [SerializeField] private Vector3 _originalSize;
    [SerializeField] private Vector3 _minSize;
    [SerializeField] private float _averageTimeForShrink;
    [SerializeField] private AudioSource _flashAudioSource;
    [SerializeField] private AudioSource _shrinkAudioSource;

    [SerializeField] private int _numOfBlinks;
    [SerializeField] private float _blinkTime;
    [SerializeField] private float _shrinkSize;
    
    private bool _canShrink = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_averageTimeForShrink + Random.Range(-5, 5));
        _canShrink = true;
    }

    private void Update()
    {
        if (!_canShrink) return;
        
        StartCoroutine(SpriteBlink());
    }

    private IEnumerator SpriteBlink()
    {
        _canShrink = false;

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        
        for (int i = 0; i < _numOfBlinks; i++)
        {
            _flashAudioSource.Play();
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = false;
            }
            yield return new WaitForSeconds(_blinkTime / 2);
            
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = true;
            }
            
            yield return new WaitForSeconds(_blinkTime / 2);
        }
        
        _shrinkAudioSource.Play();
        Shrink();

        yield return new WaitForSeconds(_averageTimeForShrink + Random.Range(-5, 5));
        
        _canShrink = true;
    }

    private void Shrink()
    {
        Vector3 currentSize = transform.localScale;
        float x = Math.Clamp(currentSize.x - _shrinkSize, _minSize.x, _originalSize.x);
        float y = Math.Clamp(currentSize.y - _shrinkSize, _minSize.y, _originalSize.y);
        float z = Math.Clamp(currentSize.z - _shrinkSize, _minSize.z, _originalSize.z);
        transform.localScale = new Vector3(x, y, z);
    }
}
