using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;    
    private float _targetVolume = 1f;
    private float _maxDeltaVolume = 0.001f;
    private bool _playerInHouse = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void Start()
    {
        _audioSource.Play();
    }

    private void Update()
    {
        VolumeChange();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _playerInHouse = true;            
        }
    }    

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _playerInHouse = false;            
        }
    }    

    private void VolumeChange()
    {
        if(_playerInHouse)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _maxDeltaVolume);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, -_maxDeltaVolume);
        }
    }
}
