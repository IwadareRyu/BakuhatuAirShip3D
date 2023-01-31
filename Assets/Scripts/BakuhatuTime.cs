using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakuhatuTime : MonoBehaviour
{
    [SerializeField]float _time = 1f;
    bool _trigger;
    [SerializeField]Vector3 _attackRangeCenter;
    [SerializeField]float _attackRange = 1f;
    [SerializeField] float _power = 3f;
    [SerializeField] float _upPower = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackRange(),_attackRange);
    }

    Vector3 GetAttackRange()
    {
        Vector3 center = transform.position + transform.forward * _attackRangeCenter.z
            + transform.up * _attackRangeCenter.y
            + transform.right * _attackRangeCenter.x;
        return center;
    }


    void Attack()
    {
        var cols = Physics.OverlapSphere(GetAttackRange(), _attackRange);

        foreach(var c in cols)
        {
            if (c.gameObject.tag != "Player")
            {
                if (c.gameObject.tag == "Enemy")
                {
                    var enemycs = c.GetComponent<EnemyController>();
                    if (!enemycs._dead)
                    {
                        enemycs.Dead();
                    }
                }
                var rb = c.gameObject.GetComponent<Rigidbody>();

                if (rb)
                {
                    Debug.Log("Ç‘Ç¡îÚÇ◊Ç¶Ç¶Ç¶Ç¶ÅI");
                    rb.AddExplosionForce(_power, transform.position, _attackRange, _upPower, ForceMode.Impulse);
                }
            }
        }
    }
}
