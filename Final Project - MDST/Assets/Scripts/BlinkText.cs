using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    private bool _isBlinking = false;
    [SerializeField] private int _numOfBlinks;
    [SerializeField] private float _blinkTime;
    [SerializeField] private float _timeBetweenBlinks;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AudioSource _flashAudioSource;
    
    private void Update()
    {
        if (_isBlinking) return;
        
        StartCoroutine(SpriteBlink());
    }
    
    private IEnumerator SpriteBlink()
    {
        _isBlinking = true;
        
        for (int i = 0; i < _numOfBlinks; i++)
        {
            _flashAudioSource.Play();
            _text.enabled = true;
            yield return new WaitForSeconds(_blinkTime / 2);
            _text.enabled = false;
            yield return new WaitForSeconds(_blinkTime / 2);
        }
        
        yield return new WaitForSeconds(_timeBetweenBlinks);
        
        _isBlinking = false;
    }
}
