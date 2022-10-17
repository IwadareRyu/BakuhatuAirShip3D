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
    [SerializeField]float _jumpPower = 2f;
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
        //カメラが斜め下に行かないために、y軸は0にする。
        dir.y = 0;
        //入力がない場合は回転させず、ある時はその方向にキャラを向ける。
        if (dir != Vector3.zero) transform.forward = dir;
        //水平方向の速度の計算。
        dir = dir.normalized * _speed;
        //垂直方向の速度の計算。
        float y = _rb.velocity.y;

        //ジャンプ
        if(Input.GetButtonDown("Jump") && _isGround)
        {
            y = _jumpPower;
        }

        //カメラ切り替え(1人称から３人称へ)
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _cm3.SetActive(true);
            _mainC.SetActive(false);
            _istherdPerson = true;
        }

        //カメラ切り替え(3人称から1人称へ)
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _cm3.SetActive(false);
            _mainC.SetActive(true);
            _istherdPerson = false;
        }

        //飛行機をぶっ飛ばす
        if(Input.GetButtonDown("Fire1") && !_isBakuhatu)
        {
            Instantiate(_bullet, _mazzle.transform.position, Quaternion.identity);
            StartCoroutine(BakuhatuTime());
        }
        //飛行機の動きの計算
        _rb.velocity = dir + Vector3.up * y;
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
