using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private bool isSignaling = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isSignaling == true && _audioSource.volume != 1)
        {
            _audioSource.volume += 0.05f;
        }
        else if (isSignaling == false && _audioSource.volume !=0)
        {
            _audioSource.volume -= 0.05f;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            Debug.Log("¬ошел посторонний");
            _audioSource.Play();
            isSignaling = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bulgral>(out Bulgral bulgral))
        {
            Debug.Log("посторонний ушел");
            isSignaling = false;
        }
    }
}
