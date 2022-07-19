using UnityEngine.Events;
using UnityEngine;

public class Door : MonoBehaviour
{    
    [SerializeField] private UnityEvent _enteredPlayer;
    [SerializeField] private UnityEvent _cameOutPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {            
            _enteredPlayer?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {            
            _cameOutPlayer?.Invoke();
        }
    }    
}