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
        //3人称じゃなかったら左右に動かさないようにする。
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        //カメラの座標を基準にdirを代入。
        dir = Camera.main.transform.TransformDirection(dir);
        //斜め下に行かないために、y軸は0にする。
        dir.y = 0;
        //入力がない場合は回転させず、ある時はその方向にキャラを向ける。
        if (dir != Vector3.zero) transform.forward = dir;
        //水平方向の速度の計算。
        dir = dir.normalized * _speed;
        //垂直方向の速度の計算。
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
