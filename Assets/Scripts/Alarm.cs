using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxDeltaVolume = 0.001f;
    [SerializeField] private AudioSource _audioSource;    

    private float _targetVolume = 1.0f;
    private float _minVolume = 0f;
    private int _soundReduction = -1;
    private IEnumerator _currentCoroutine;

    private void Awake()
    {        
        _audioSource.volume = _minVolume;
    }    

    private void Start()
    {
        _audioSource.Play();
    }    

    public void TurnOnIncreaseVolume()
    {
        AssignCurrentCoroutine(IncreaseVolume());
    }

    public void TurnOnDecreaseVolume()
    {
        AssignCurrentCoroutine(DecreaseVolume());
    }

    private void AssignCurrentCoroutine(IEnumerator volumeChangerCoroutine)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = volumeChangerCoroutine;
        StartCoroutine(_currentCoroutine);
    }

    private IEnumerator IncreaseVolume()
    {
        while (_audioSource.volume < _targetVolume)
        {
            ChangeVolume();
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        while (_audioSource.volume > _minVolume)
        {
            ChangeVolume(_soundReduction);
            yield return null;
        }
    }

    private void ChangeVolume(int changeDirection = 1)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _maxDeltaVolume * changeDirection);
    }
}