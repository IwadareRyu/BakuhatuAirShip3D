using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _speed = 3f;
    [SerializeField] GameObject _bip;
    [SerializeField] Vector3 _lay;
    [SerializeField] LayerMask _layer = 0;
    private GameObject _tower;
    public bool _hit { get; set; }
    public bool _stop { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tower = GameObject.FindGameObjectWithTag("Tower");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir;
        if (!_hit)
        {
            if (!_stop)
            {
                dir = Vector3.forward * _speed;
            }
            else
            {
                dir = new Vector3(0f, 0f, 0f);
            }
        }
        else
        {
            dir = Vector3.left * _speed;
        }
        dir = transform.TransformDirection(dir);
        _rb.velocity = dir;
    }

    private void LateUpdate()
    {
        transform.LookAt(_tower.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bakuhatu")
        {
            Destroy(gameObject);
        }
    }
}
