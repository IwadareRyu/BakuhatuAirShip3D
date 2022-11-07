using Cinemachine;
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
    [SerializeField]float _dashPower = 2f;
    [Tooltip("�J�����؂�ւ�")]
    [SerializeField] GameObject _cm3;
    [SerializeField] GameObject _mainC;
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
    [Tooltip("�J�����̏㉺�𓮂������߂̃I�u�W�F�N�g")]
    [SerializeField] Transform _eye;
    [Tooltip("�J���������E�ɓ�����")]
    [SerializeField] AxisState Horizontal;
    [Tooltip("�J�������㉺�ɓ�����")]
    [SerializeField] AxisState Vertical;
    [Tooltip("����؂�ւ���bool�^")]
    bool _airShipFly;

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
        v = Input.GetAxisRaw("Vertical");
        Vector3 dir;
        if (!_airShipFly)
        {
            dir = Vector3.forward * v + Vector3.right * h;
        }
        else
        {
            dir = Vector3.up * v + Vector3.right * h;
        }

        //�J�����̍��W�����dir�����B
        dir = Camera.main.transform.TransformDirection(dir);
        //�J�������΂߉��ɍs���Ȃ����߂ɁAy����0�ɂ���B
        if (!_airShipFly)
        {
            dir.y = 0;
        }
        //���͂��Ȃ��ꍇ�͉�]�������A���鎞�͂��̕����ɃL������������B
        if (dir != Vector3.zero) transform.forward = dir;
        //���������̑��x�̌v�Z�B
        dir = dir.normalized * _speed;
        //���������̑��x�̌v�Z�B
        if (!_airShipFly)
        {
            dir.y = _rb.velocity.y;
        }

        //�W�����v
        if(Input.GetButtonDown("Jump"))
        {
            _rb.useGravity = !_rb.useGravity;
            _airShipFly = !_airShipFly;
        }

        //�J�����؂�ւ�(1�l�̂���R�l�̂�)
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _cm3.SetActive(true);
            _istherdPerson = true;
        }

        //�J�����؂�ւ�(3�l�̂���1�l�̂�)
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _cm3.SetActive(false);
            _istherdPerson = false;
        }

        //��s�@���Ԃ���΂�
        if(Input.GetButtonDown("Fire1") && !_isBakuhatu)
        {
            Instantiate(_bullet, _mazzle.transform.position, Quaternion.identity);
            StartCoroutine(BakuhatuTime());
        }

        Vector3 dash = new Vector3(0,0,0);
        if (Input.GetButton("Fire2") && _airShipFly)
        {
            dash += Vector3.forward * _dashPower;
            dash = Camera.main.transform.TransformDirection(dash);
            dash.y = 0;
        }

        //��s�@�̓����̌v�Z
        _rb.velocity = dir + dash;
        //�J�����̓���
        Horizontal.Update(Time.deltaTime);
        Vertical.Update(Time.deltaTime);
        //�J�����𓮂���
        var horizontal = Quaternion.AngleAxis(Horizontal.Value, Vector3.up);
        transform.rotation = horizontal;
        var vertical = Quaternion.AngleAxis(Vertical.Value, Vector3.right);
        _eye.localRotation = vertical;
    }

    /// <summary>�����̃N�[���^�C��</summary>
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

    /// <summary>�ڒn����</summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        _isGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGround = false;
    }
}
