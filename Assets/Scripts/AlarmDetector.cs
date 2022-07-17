using UnityEngine;
using UnityEngine.Events;

public class AlarmDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent _playerEnteredHouse;
    [SerializeField] private UnityEvent _playerCameOutHouse;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {            
            _playerEnteredHouse?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {            
            _playerCameOutHouse?.Invoke();
        }
    }
}