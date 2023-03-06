using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownEnemy : MonoBehaviour
{
    bool _spownbool;
    BulletPoolActive _pool;
    [SerializeField] string _poolTagStr = "EnemyPool";
    // Start is called before the first frame update
    void Start()
    {
        _pool = GameObject.FindGameObjectWithTag(_poolTagStr).GetComponent<BulletPoolActive>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_spownbool)
        {
            _spownbool = true;
            StartCoroutine(SpownTime());
        }
    }

    IEnumerator SpownTime()
    {
        yield return new WaitForSeconds(5f);
        GetPool();
        yield return new WaitForSeconds(5f);
        _spownbool = false;
    }

    void GetPool()
    {
        var obj = _pool.GetBullet();
        obj.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
