using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipBakuhatu : MonoBehaviour
{
    [SerializeField] float _speed = 4f;
    [SerializeField] float _upspeed = 1f;
    Rigidbody _rb;
    [SerializeField] GameObject _bakuhatu;
    float _timer;
    bool _bakuhatuTime;
    // Start is called before the first frame update
    void Start()
    {
        //飛行機ダマの制御
        _rb = GetComponent<Rigidbody>();
        //球をカメラ方向へ山なりに飛ばす
        Vector3 dir = Vector3.forward * _speed + Vector3.up * _upspeed;
        dir = Camera.main.transform.TransformDirection(dir);
        _rb.AddForce(dir, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 0.2f)
        {
            _bakuhatuTime = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && _bakuhatuTime)
        {
            if(collision.gameObject.tag == "Tower")
            {
                var tower = collision.gameObject.GetComponent<TowerBreak>();
                tower.BreakTower();
            }
            Bakuhatu();
        }
    }
    /// <summary>爆発</summary>
    void Bakuhatu()
    {
        Instantiate(_bakuhatu, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
