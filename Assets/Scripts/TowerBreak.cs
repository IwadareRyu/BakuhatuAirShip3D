using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class TowerBreak : MonoBehaviour
{
    [Tooltip("壊れていない方のタワー")]
    [SerializeField] GameObject _tower;
    [Tooltip("壊れた方のタワー")]
    [SerializeField] GameObject _breakTower;
    [Header("ステージの数-0.1を入力(ランダムの性質を上げるため)")]
    [SerializeField] float _stageNumber = 0.9f;
    [Header("生成するステージをここに入れていく")]
    [SerializeField] GameObject[] _newStage;
    Collider col;
    bool _breakbool;
    [SerializeField] GameObject _blockObj;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider>();
        var navSurface = GetComponent<NavMeshSurface>();
        navSurface.BuildNavMesh();
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
        if (!_breakbool)
        {
            _breakbool = true;
            StartCoroutine(BreakTime());
        }
    }

    IEnumerator BreakTime()
    {
        _breakTower.SetActive(true);
        _tower.SetActive(false);
        _blockObj.SetActive(false);
        GameManager.Instance.TowerCountAdd();

        NewStageInstansTime();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        yield return new WaitForSeconds(0.5f);
        col.enabled = false;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    void NewStageInstansTime()
    {
        var ram = (int)Random.Range(0,_stageNumber);
        var pos = new Vector3(0, 0, GameManager.Instance.towerCount * 100);
        Instantiate(_newStage[ram],pos,Quaternion.identity);
    }
}
