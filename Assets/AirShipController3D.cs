using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipController3D : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField]float _speed = 2f;
    [SerializeField]bool _isGround;
    [SerializeField]float _jumpPower = 2f;
    [SerializeField] GameObject _cm3;
    bool _istherdPerson;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = 0;
        float v = 0;
        if (!_istherdPerson)
        {
            h = Input.GetAxisRaw("Horizontal");
        }
        v = Mathf.Max(Input.GetAxisRaw("Vertical"), 0);
        //3�l�̂���Ȃ������獶�E�ɓ������Ȃ��悤�ɂ���B
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        //�J�����̍��W�����dir�����B
        dir = Camera.main.transform.TransformDirection(dir);
        //�΂߉��ɍs���Ȃ����߂ɁAy����0�ɂ���B
        dir.y = 0;
        //���͂��Ȃ��ꍇ�͉�]�������A���鎞�͂��̕����ɃL������������B
        if (dir != Vector3.zero) transform.forward = dir;
        //���������̑��x�̌v�Z�B
        dir = dir.normalized * _speed;
        //���������̑��x�̌v�Z�B
        float y = _rb.velocity.y;

        if(Input.GetButtonDown("Jump") && _isGround)
        {
            y = _jumpPower;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _cm3.SetActive(true);
            _istherdPerson = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _cm3.SetActive(false);
            _istherdPerson = false;
        }    

        _rb.velocity = dir + Vector3.up * y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGround = false;
    }
}
