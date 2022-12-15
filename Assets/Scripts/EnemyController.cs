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
    [Tooltip("Player‚ÌŒì‰q‘ÎÛ")]
    private GameObject _tower;
    [Tooltip("ˆÚ“®æ‚Ì‹ß‚­‚Ü‚ÅˆÚ“®‚µ‚½‚ç~‚Ü‚éboolŒ^")]
    public bool _stop { get; set; }
    [Tooltip("ˆÚ“®æ‚Ì•Û‘¶")]
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
