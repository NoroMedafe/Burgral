using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            Debug.Log("¬ошел посторонний");

            StartCoroutine(startSignaling());
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            Debug.Log("посторонний ушел");

            StartCoroutine(stopSignaling());
        }
    }

    private IEnumerator startSignaling()
    {
        _audioSource.Play();

        while (_audioSource.volume != 1)
        {
            _audioSource.volume += 0.05f;

            yield return null;
        }
    }
    private IEnumerator stopSignaling()
    {
        StopCoroutine(startSignaling());

        while (_audioSource.volume != 0)
        {
            _audioSource.volume -= 0.05f;

            yield return null;
        }
    }
}
