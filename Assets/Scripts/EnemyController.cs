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
    [SerializeField] Transform _mazzle;
    [SerializeField] GameObject _bullet;
    [Tooltip("PlayerÇÃåÏâqëŒè€")]
    private GameObject _player;
    [Tooltip("à⁄ìÆêÊÇÃï€ë∂")]
    Vector3 _cashedTarget;
    [SerializeField] float _speed;
    Animator _animator;
    bool _attackbool;
    Vector3 _targetPos;
    public bool _dead { get; set; } = false;
    [SerializeField]bool _move;
    [SerializeField]AttackState _state = AttackState.MoveStop;
    [SerializeField] RuntimeAnimatorController _movePattern;
    [SerializeField] RuntimeAnimatorController _standPattern;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _cashedTarget = _player.transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        StartCoroutine(AgentReset());
        _cashedTarget = _player.transform.position;
        _dead = false;
        _animator.SetBool("Walk", true);
    }

    IEnumerator AgentReset()
    {
        _agent.enabled = false;
        yield return new WaitForEndOfFrame();
        _agent.enabled = true;
    }

    private void Start()
    {
        if(_move)
        {
            _animator.runtimeAnimatorController = _movePattern;
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.runtimeAnimatorController = _standPattern;
        }
    }


    void Update()
    {
        if (_state == AttackState.MoveStop)
        {
            if (_move)
            {
                //if (_agent.pathStatus != NavMeshPathStatus.PathInvalid)
                //{
                    _cashedTarget = _player.transform.position;
                    _agent.SetDestination(_cashedTarget);
                //}
            }
            else
            {
                _targetPos = _player.transform.position;
                _targetPos.y = transform.position.y;
                transform.LookAt(_targetPos);
            }
        }

        if (_state == AttackState.Attack)
        {
            _targetPos = _player.transform.position;
            _targetPos.y = transform.position.y;
            transform.LookAt(_targetPos);
            if (!_attackbool)
            {
                _attackbool = true;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        _agent.updatePosition = false;
        _agent.isStopped = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);
        _attackbool = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _state = AttackState.Attack;
            if(_move)_animator.SetBool("Walk", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _state = AttackState.MoveStop;
            if (_move) _animator.SetBool("Walk", true);
            _agent.isStopped = false;
            _agent.updatePosition = true;
        }
    }

    public void StartAttack()
    {
        Instantiate(_bullet,_mazzle.position,Quaternion.identity);
    }

    public void Dead()
    {
        _dead = true;
        GameManager.Instance.AddScore(100);
        GameObject ragDoll = (GameObject)Resources.Load("ragdoll");
        Instantiate(ragDoll, transform.position, transform.rotation);
        if (_move)
        {
            _state = AttackState.MoveStop;
            _agent.updatePosition = true;
            _agent.isStopped = false;
            _attackbool = false;
            gameObject.SetActive(false);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    enum AttackState
    {
        MoveStop,
        Attack,
    }
}
