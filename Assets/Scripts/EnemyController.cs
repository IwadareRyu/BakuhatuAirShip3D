using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    Rigidbody _rb;
    NavMeshAgent _agent;
    [SerializeField] GameObject _bip;
    [Tooltip("Playerの護衛対象")]
    private GameObject _tower;
    [Tooltip("移動先の近くまで移動したら止まるbool型")]
    public bool _stop { get; set; }
    [Tooltip("移動先の保存")]
    Vector3 _cashedTarget;
    [SerializeField] float _speed;
    Animator _animator;
    bool _count;
    Vector3 _targetPos;

    void Awake()
    {
        _tower = GameObject.FindGameObjectWithTag("Tower");
        _agent = GetComponent<NavMeshAgent>();
        _cashedTarget = _tower.transform.position;
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Vector3.Distance(_cashedTarget, _tower.transform.position) > Mathf.Epsilon || _count == false)
        {
            _cashedTarget = _tower.transform.position;
            _agent.SetDestination(_cashedTarget);
            _count = true;
        }

        if (_stop)
        {
            _targetPos = _tower.transform.position;
            _targetPos.y = 0;
            transform.LookAt(_targetPos);
            _agent.updatePosition = false;
            _animator.SetBool("Attack", true);
        }
        else
        {
            _agent.updatePosition = true;
            _animator.SetBool("Attack", false);
        }
    }

    private void LateUpdate()
    {
        //transform.LookAt(_tower.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bakuhatu")
        {
            Destroy(gameObject);
        }
    }
}
