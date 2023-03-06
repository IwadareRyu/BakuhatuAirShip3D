using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _speed;
    [SerializeField] float _time;
    [SerializeField] GameObject _bakuhatu;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
        StartCoroutine(BakuhatuMinute());
    }

    IEnumerator BakuhatuMinute()
    {
        yield return new WaitForSeconds(_time);
        Instantiate(_bakuhatu,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
