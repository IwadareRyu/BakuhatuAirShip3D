using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakuhatuTime : MonoBehaviour
{
    [SerializeField]float _time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
