using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipBakuhatu : MonoBehaviour
{
    [SerializeField] float _speed = 4f;
    [SerializeField] float _upspeed = 1f;
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Vector3 dir = Vector3.forward * _speed + Vector3.up * _upspeed;
        dir = Camera.main.transform.TransformDirection(dir);
        _rb.AddForce(dir,ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
