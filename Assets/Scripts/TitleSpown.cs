using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpown : MonoBehaviour
{
    [SerializeField] GameObject _spownObj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpownTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpownTime()
    {
        Instantiate(_spownObj, transform.position, Quaternion.identity);
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(_spownObj,transform.position,Quaternion.identity);
        }
    }
}
