using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _doorCloseDelay = 2f;

    private int _openHash = Animator.StringToHash("open");
    private int _closeHash = Animator.StringToHash("close");

    public void Open()
    {        
        _animator.ResetTrigger(_closeHash);
        _animator.SetTrigger(_openHash);
        StartCoroutine(CloseDoorCoroutine());
    }

    private IEnumerator CloseDoorCoroutine()
    {
        yield return new WaitForSeconds(_doorCloseDelay);
        _animator.ResetTrigger(_openHash);
        _animator.SetTrigger(_closeHash);
    }
}