using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBreak : MonoBehaviour
{
    [SerializeField] GameObject _tower;
    [SerializeField] GameObject _breakTower;
    [SerializeField] GameObject[] _newStage;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakTower()
    {
        GameManager.Instance.AddScore(10000);
        var poolObj = GameObject.FindGameObjectsWithTag("PoolObj");
        foreach(var i in poolObj)
        {
            i.SetActive(false);
        }
        var enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var i in enemy)
        {
            Destroy(i);
        }
        StartCoroutine(BreakTime());
    }

    IEnumerator BreakTime()
    {
        _breakTower.SetActive(true);
        _tower.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        col.enabled = false;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
