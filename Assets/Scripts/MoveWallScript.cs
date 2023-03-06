using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWallScript : MonoBehaviour
{
    [SerializeField] float _biggest;
    [SerializeField] float _small;
    [SerializeField] float _ySpeed = 1f;
    bool _speedbool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f,_ySpeed,0f);
        if (!_speedbool && transform.position.y > _biggest || !_speedbool && transform.position.y < _small)
        {
            _speedbool = true;
            StartCoroutine(SpeedTime());
            _ySpeed *= -1;
        }
    }
    IEnumerator SpeedTime()
    {
        yield return new WaitForSeconds(0.5f);
        _speedbool = false;
    }
}
