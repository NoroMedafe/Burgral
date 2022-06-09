using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speed;

    private float _currentSignalingValue;
    private Coroutine _currentActiveCoroutine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            _currentSignalingValue = 1;

            if (_currentActiveCoroutine != null)
                StopCoroutine(_currentActiveCoroutine);

            _audioSource.Play();

            _currentActiveCoroutine = StartCoroutine(VolumeChange(_currentSignalingValue));
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            _currentSignalingValue = 0;

            if (_currentActiveCoroutine != null)
                StopCoroutine(_currentActiveCoroutine);

            _currentActiveCoroutine = StartCoroutine(VolumeChange(_currentSignalingValue));
        }
    }

    private IEnumerator VolumeChange(float signalingValue)
    {

        while (_audioSource.volume != signalingValue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, signalingValue, _speed);

            yield return null;
        }
    }
}
