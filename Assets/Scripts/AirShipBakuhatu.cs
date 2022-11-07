using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipBakuhatu : MonoBehaviour
{
    [SerializeField] float _speed = 4f;
    [SerializeField] float _upspeed = 1f;
    Rigidbody _rb;
    [SerializeField] GameObject _bakuhatu;
    // Start is called before the first frame update
    void Start()
    {
        //��s�@�_�}�̐���
        _rb = GetComponent<Rigidbody>();
        //�����J���������֎R�Ȃ�ɔ�΂�
        Vector3 dir = Vector3.forward * _speed + Vector3.up * _upspeed;
        dir = Camera.main.transform.TransformDirection(dir);
        _rb.AddForce(dir, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Bakuhatu();
    }
    /// <summary>����</summary>
    void Bakuhatu()
    {
        Instantiate(_bakuhatu, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
