using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipController3D : MonoBehaviour
{
    Rigidbody _rb;
    [Tooltip("�v���C���[�̓����̑���")]
    [SerializeField]float _speed = 2f;
    [Tooltip("�n�ʂɐڂ��Ă��邩")]
    bool _isGround;
    [Tooltip("�W�����v�̍���")]
    [SerializeField]float _jumpPower = 2f;
    [Tooltip("�J�����؂�ւ�")]
    [SerializeField] GameObject _cm3;
    [Tooltip("�O�l�̂��ۂ�")]
    bool _istherdPerson;
    [Tooltip("��s�@�̃I�u�W�F�N�g")]
    [SerializeField] GameObject _ship;
    [Tooltip("�v���C���[�̃R�A")]
    [SerializeField] GameObject _core;
    [Tooltip("��s�@���˂�bool�^")]
    bool _isBakuhatu;
    [Tooltip("���𔭎˂���}�Y��")]
    [SerializeField] GameObject _mazzle;
    [Tooltip("��s�@�̋�")]
    [SerializeField] GameObject _bullet;

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
        if(Input.GetButtonDown("Fire1") && !_isBakuhatu)
        {
            Instantiate(_bullet, _mazzle.transform.position, Quaternion.identity);
            StartCoroutine(BakuhatuTime());
        }

        _rb.velocity = dir + Vector3.up * y;
    }

    IEnumerator BakuhatuTime()
    {
        _ship.SetActive(false);
        _core.SetActive(true);
        _isBakuhatu = true;
        yield return new WaitForSeconds(3f);
        _ship.SetActive(true);
        _core.SetActive(false);
        _isBakuhatu = false;
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
