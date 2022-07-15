using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxDeltaVolume = 0.001f;    

    private AudioSource _audioSource;    
    private float _targetVolume = 1.0f;
    private IEnumerator _currentCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void Start()
    {
        _audioSource.Play();
    }    

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = IncreaseVolume();
            StartCoroutine(_currentCoroutine);
        }        
    }    

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = TurnDownVolume();
            StartCoroutine(_currentCoroutine);
        }
    }

    private IEnumerator IncreaseVolume()
    {
        while(_audioSource.volume < _targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _maxDeltaVolume);
            yield return null;
        }        
    }

    private IEnumerator TurnDownVolume()
    {
        while(_audioSource.volume > 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, -_maxDeltaVolume);
            yield return null;
        }              
    }
}