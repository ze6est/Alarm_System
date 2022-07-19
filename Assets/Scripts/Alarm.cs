using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxDeltaVolume = 0.001f;
    [SerializeField] private AudioSource _audioSource;    

    private float _targetVolume = 1.0f;
    private float _minVolume = 0f;    
    private IEnumerator _currentCoroutine;
    private const int _increaseVolume = 1;
    private const int _decreaseColume = -1;

    private void Awake()
    {        
        _audioSource.volume = _minVolume;        
    }    

    private void Start()
    {
        _audioSource.Play();
    }

    public void RunCoroutineChangeVolume(bool enteredPlayer)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = RunChangeVolume(enteredPlayer);
        StartCoroutine(_currentCoroutine);
    }

    private void ChangeVolume(int changeDirection = _increaseVolume)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _maxDeltaVolume * changeDirection);
    }    

    private IEnumerator RunChangeVolume(bool enteredPlayer)
    {
        if(enteredPlayer)
        {
            while (_audioSource.volume < _targetVolume)
            {
                ChangeVolume();
                yield return null;
            }
        }
        else
        {
            while (_audioSource.volume > _minVolume)
            {
                ChangeVolume(_decreaseColume);
                yield return null;
            }
        }
    }
}