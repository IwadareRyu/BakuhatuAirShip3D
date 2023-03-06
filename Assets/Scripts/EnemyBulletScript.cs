using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody _rb;
    [SerializeField] float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(_player.transform.position);
        _rb = GetComponent<Rigidbody>();
        var dir = transform.forward * _speed;
        _rb.velocity = dir;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Player")
        {
            var playercs = _player.GetComponent<AirShipController3D>();
            playercs.DropBakuhatu();
            GameManager.Instance.AddTime(-10);
            Destroy(gameObject);
        }
    }
}
