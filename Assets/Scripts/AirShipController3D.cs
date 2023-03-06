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
    [SerializeField] GameObject _subCamera;
    [Tooltip("0がMainCM、1がSkyCM、2がDestroyCM")]
    [SerializeField] GameObject[] _AllCm;
    [Header("BlockとBlockの透明化切り替え")]
    [SerializeField] Material _brownStone;
    [SerializeField] Color _fade;
    [SerializeField] Color _noFade;
    [Tooltip("三人称か否か")]
    bool _observer;
    [Tooltip("飛行機のオブジェクト")]
    [SerializeField] GameObject _ship;
    Animator _reLoadAni;
    [Tooltip("飛行機のアニメーション")]
    Animator _shipAni;
    [Tooltip("プレイヤーのコア")]
    [SerializeField] GameObject _core;
    [Tooltip("飛行機発射のbool型")]
    bool _isBakuhatu;
    [Tooltip("球を発射するマズル")]
    [SerializeField] GameObject _mazzle;
    [Tooltip("飛行機の球")]
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _dropBullet;
    [Tooltip("カメラの上下を動かすためのオブジェクト")]
    [SerializeField] Transform _eye;
    [Tooltip("カメラを左右に動かす")]
    [SerializeField] AxisState Horizontal;
    [Tooltip("カメラを上下に動かす")]
    [SerializeField] AxisState Vertical;
    [Tooltip("操作切り替えのbool型")]
    bool _airShipFly;
    [SerializeField] State _state = State.NomalMode;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _shipAni = GetComponent<Animator>();
        _reLoadAni = _ship.GetComponent<Animator>();
        _shipAni.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance._start)
        {
            float h = 0;
            float v = 0;

            //blockすり抜けカメラモードではなかったら入力を受け付ける。
            if (!_observer)
            {
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
            }
            Vector3 dir = new Vector3(0, 0, 0);

            //飛行中かそうでないかで移動方法を変える。(力の加え方を変える。)
            if (_state == State.NomalMode)
            {
                dir = Vector3.forward * v + Vector3.right * h;
            }
            else if (_state == State.FlyMode)
            {
                dir = Vector3.up * v + Vector3.right * h;
            }

            //カメラの座標を基準にdirを代入。
            dir = Camera.main.transform.TransformDirection(dir);

            //カメラが斜め下に行かないために、y軸は0にする。
            if (_state != State.FlyMode)
            {
                dir.y = 0;
            }

            //入力がない場合は回転させず、ある時はその方向にキャラを向ける。
            if (dir != Vector3.zero) transform.forward = dir;

            //水平方向の速度の計算。
            dir = dir.normalized * _speed;

            //垂直方向の速度の計算。
            if (_state != State.FlyMode)
            {
                dir.y = _rb.velocity.y;
            }

            if (Input.GetButtonDown("Jump") && _state != State.DestroyMode)
            {
                _rb.useGravity = !_rb.useGravity;
                _AllCm[1].active = !_AllCm[1].active;
                _shipAni.enabled = !_shipAni.enabled;
                if (_state == State.FlyMode)
                {
                    _state = State.NomalMode;
                }
                else
                {
                    _state = State.FlyMode;
                }
            }
            ////空を飛ぶ
            //if(Input.GetButtonDown("Jump"))
            //{
            //    _rb.useGravity = !_rb.useGravity;
            //    _AllCm[1].active = !_AllCm[1].active;
            //    _shipAni.enabled = !_shipAni.enabled;
            //    _airShipFly = !_airShipFly;
            //}

            //カメラ切り替え(ブロックすり抜けカメラモード)
            if (Input.GetButton("ChangeCamera") && _state != State.DestroyMode)
            {
                _subCamera.SetActive(true);
                _brownStone.color = _fade;
                _observer = true;
            }

            //カメラ切り替え(通常カメラモード)
            if (Input.GetButtonUp("ChangeCamera"))
            {
                _subCamera.SetActive(false);
                _brownStone.color = _noFade;
                _observer = false;
            }

            //飛行機をぶっ飛ばす
            if (Input.GetButtonDown("Fire1") && !_isBakuhatu && v != -1)
            {
                Instantiate(_bullet, _mazzle.transform.position, Quaternion.identity);
                StartCoroutine(BakuhatuTime());
            }

            Vector3 dash = new Vector3(0, 0, 0);

            //飛行中、前に移動する処理。
            if (_state == State.FlyMode && !_observer && Input.GetButton("Fire2") || _state == State.FlyMode && !_observer && Input.GetButton("Fire3"))
            {
                if (Input.GetButton("Fire2"))
                {
                    dash += Vector3.forward * _dashPower;
                }
                else if (Input.GetButton("Fire3"))
                {
                    dash -= Vector3.forward * _speed;
                }
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
        else
        {
            _rb.velocity = new Vector3(0,0,0);
        }
    }

    /// <summary>爆発のクールタイム</summary>
    IEnumerator BakuhatuTime()
    {
        _ship.SetActive(false);
        _core.SetActive(true);
        _isBakuhatu = true;
        yield return new WaitForSeconds(1f);
        _ship.SetActive(true);
        _reLoadAni.Play("ReLoadShip");
        yield return new WaitForSeconds(2f);
        _core.SetActive(false);
        _isBakuhatu = false;
    }

    public void DropBakuhatu()
    {
        if (!_isBakuhatu)
        {
            Instantiate(_dropBullet, transform.position, Quaternion.identity);
            StartCoroutine(BakuhatuTime());
        }
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

    enum State
    {
        FlyMode,
        DestroyMode,
        NomalMode,
    }
}
