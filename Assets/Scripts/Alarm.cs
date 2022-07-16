using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxDeltaVolume = 0.001f;

    private AudioSource _audioSource;
    private float _targetVolume = 1.0f;
    private float _minVolume = 0f;
    private int _soundReduction = -1;
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
        if (other.GetComponent<Player>())
        {
            TurnOnCoroutine(IncreaseVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            TurnOnCoroutine(TurnDownVolume());
        }
    }

    private IEnumerator IncreaseVolume()
    {
        while (_audioSource.volume < _targetVolume)
        {
            ChangeVolume();
            yield return null;
        }
    }

    private IEnumerator TurnDownVolume()
    {
        while (_audioSource.volume > _minVolume)
        {
            ChangeVolume(_soundReduction);
            yield return null;
        }
    }

    private void TurnOnCoroutine(IEnumerator includedCoroutine)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = includedCoroutine;
        StartCoroutine(_currentCoroutine);
    }

    private void ChangeVolume(int changeDirection = 1)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _maxDeltaVolume * changeDirection);
    }
}