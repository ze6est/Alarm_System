using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _doorCloseDelay = 2f;

    public void Open()
    {
        _animator.ResetTrigger("close");
        _animator.SetTrigger("open");
        StartCoroutine(CloseDoorCoroutine());
    }

    private IEnumerator CloseDoorCoroutine()
    {
        yield return new WaitForSeconds(_doorCloseDelay);
        _animator.ResetTrigger("open");
        _animator.SetTrigger("close");
    }
}