using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipController3D : MonoBehaviour
{
    Rigidbody _rb;
    [Tooltip("プレイヤーの動きの速さ")]
    [SerializeField]float _speed = 2f;
    [Tooltip("地面に接しているか")]
    bool _isGround;
    [Tooltip("ジャンプの高さ")]
    [SerializeField]float _dashPower = 2f;
    [Tooltip("カメラ切り替え")]
    [SerializeField] GameObject _cm3;
    [SerializeField] GameObject _mainC;
    [Tooltip("三人称か否か")]
    bool _istherdPerson;
    [Tooltip("飛行機のオブジェクト")]
    [SerializeField] GameObject _ship;
    [Tooltip("プレイヤーのコア")]
    [SerializeField] GameObject _core;
    [Tooltip("飛行機発射のbool型")]
    bool _isBakuhatu;
    [Tooltip("球を発射するマズル")]
    [SerializeField] GameObject _mazzle;
    [Tooltip("飛行機の球")]
    [SerializeField] GameObject _bullet;
    [Tooltip("カメラの上下を動かすためのオブジェクト")]
    [SerializeField] Transform _eye;
    [Tooltip("カメラを左右に動かす")]
    [SerializeField] AxisState Horizontal;
    [Tooltip("カメラを上下に動かす")]
    [SerializeField] AxisState Vertical;
    [Tooltip("操作切り替えのbool型")]
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
            v = Input.GetAxisRaw("Vertical");
        }
        Vector3 dir;

        if (!_airShipFly)
        {
            dir = Vector3.forward * v + Vector3.right * h;
        }
        else
        {
            dir = Vector3.up * v + Vector3.right * h;
        }

        //カメラの座標を基準にdirを代入。
        dir = Camera.main.transform.TransformDirection(dir);

        //カメラが斜め下に行かないために、y軸は0にする。
        if (!_airShipFly)
        {
            dir.y = 0;
        }

        //入力がない場合は回転させず、ある時はその方向にキャラを向ける。
        if (dir != Vector3.zero) transform.forward = dir;

        //水平方向の速度の計算。
        dir = dir.normalized * _speed; 

        //垂直方向の速度の計算。
        if (!_airShipFly)
        {
            dir.y = _rb.velocity.y;
        }

        //ジャンプ
        if(Input.GetButtonDown("Jump"))
        {
            _rb.useGravity = !_rb.useGravity;
            _airShipFly = !_airShipFly;
        }

        //カメラ切り替え(ブロックすり抜けカメラモード)
        if(Input.GetButton("ChangeCamera"))
        {
            _cm3.SetActive(true);
            _istherdPerson = true;
        }

        //カメラ切り替え(通常カメラモード)
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _cm3.SetActive(false);
            _istherdPerson = false;
        }

        //飛行機をぶっ飛ばす
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

        //飛行機の動きの計算
        _rb.velocity = dir + dash;
        //カメラの動き
        Horizontal.Update(Time.deltaTime);
        Vertical.Update(Time.deltaTime);
        //カメラを動かす
        var horizontal = Quaternion.AngleAxis(Horizontal.Value, Vector3.up);
        transform.rotation = horizontal;
        var vertical = Quaternion.AngleAxis(Vertical.Value, Vector3.right);
        _eye.localRotation = vertical;
    }

    /// <summary>爆発のクールタイム</summary>
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

    /// <summary>接地判定</summary>
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
