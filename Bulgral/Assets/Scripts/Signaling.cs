using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private bool _isSignaling = false;
   
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            _isSignaling = true;
            StopAllCoroutines();
            StartCoroutine(SetActiveAlarm(_isSignaling));
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            _isSignaling = false;
            StopAllCoroutines();
            StartCoroutine(SetActiveAlarm(_isSignaling));
        }
    }

    private IEnumerator SetActiveAlarm(bool isSignaling)
    {

        if (isSignaling)
        {
            _audioSource.Play();

            while (_audioSource.volume != 1)
            {
                _audioSource.volume += 0.01f;

                yield return null;
            }
        }
        else
        {

            while (_audioSource.volume != 0)
            {
                _audioSource.volume -= 0.01f;

                yield return null;
            }
        }
    }
}
