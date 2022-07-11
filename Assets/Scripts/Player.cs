using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour{
    
    [SerializeField] private float _doorOpenDelay = 0.2f;

    private NavMeshAgent _myAgent;
    private bool _hasControl = true;    
    
    private void Awake()
    {
        _myAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    { 
        if(Input.GetMouseButtonDown(0) && _hasControl)
        {
            ClickToMove();
        }
    }

    private void ClickToMove()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(myRay, out hitInfo);

        if(hasHit)
        {
            SetDestination(hitInfo.point);
        }
    }

    private void SetDestination(Vector3 target)
    {
        _myAgent.SetDestination(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Door>())
        {
            _hasControl = false;
            _myAgent.isStopped = true;
            other.GetComponent<Door>().Open();
            StartCoroutine(DelayCoroutine());
        }
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(_doorOpenDelay);
        _hasControl = true;
        _myAgent.isStopped = false;
    }
}