using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakuhatuTime : MonoBehaviour
{
    [SerializeField]float _time = 1f;
    bool _trigger;
    [SerializeField]Vector3 _attackRangeCenter;
    [SerializeField]float _attackRange = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RagDoll" && !_trigger)
        {
            _trigger = true;
            Attack();
        }
    }

    void Attack()
    {
        var cols = Physics.OverlapSphere(GetAttackRange(), _attackRange);

        foreach(var c in cols)
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
        }
    }
}
