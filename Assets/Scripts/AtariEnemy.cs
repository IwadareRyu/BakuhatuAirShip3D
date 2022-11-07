using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtariEnemy : MonoBehaviour
{
    [SerializeField]EnemyController _enemyScript;
    [SerializeField]Animator _anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Wall"))
        {
            _enemyScript._hit = true;
        }
        if(other.gameObject.tag == ("Tower"))
        {
            StartCoroutine(StopTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Wall"))
        {
            _enemyScript._hit = false;
        }
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.5f);
        _enemyScript._stop = true;
        _anim.Play("Attack");
    }
}
