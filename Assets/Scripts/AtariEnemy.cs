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
        if(other.gameObject.tag == ("Tower"))
        {
            StartCoroutine(StopTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Tower"))
        {
            _enemyScript._stop = false;
        }
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("é~Ç‹ÇÍÇ¶ÅI");
        _enemyScript._stop = true;
    }
}
