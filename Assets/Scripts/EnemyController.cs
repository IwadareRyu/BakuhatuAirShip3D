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
    [Tooltip("PlayerÇÃåÏâqëŒè€")]
    private GameObject _tower;
    [Tooltip("à⁄ìÆêÊÇÃãﬂÇ≠Ç‹Ç≈à⁄ìÆÇµÇΩÇÁé~Ç‹ÇÈboolå^")]
    public bool _stop { get; set; }
    [Tooltip("à⁄ìÆêÊÇÃï€ë∂")]
    Vector3 _cashedTarget;
    [SerializeField] float _speed;
    Animator _animator;
    bool _count;
    Vector3 _targetPos;
    public bool _dead { get; set; } = false;

    void Awake()
    {
        _tower = GameObject.FindGameObjectWithTag("Tower");
        _cashedTarget = _tower.transform.position;
        _agent = GetComponent<NavMeshAgent>();
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

        //_targetPos = _tower.transform.position;
        //_targetPos.y = 0;
        //transform.LookAt(_targetPos);
    }

    private void LateUpdate()
    {
        //transform.LookAt(_tower.transform);
    }

    public void Dead()
    {
        _dead = true;
        GameObject ragDoll = (GameObject)Resources.Load("ragdoll");
        Instantiate(ragDoll, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
