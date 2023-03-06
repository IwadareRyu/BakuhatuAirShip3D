using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : SingletonMonovihair<GameManager>
{
    [Tooltip("スコアのテキスト")]
    Text _scoreText;
    GameObject _plusScore;
    Text _plusScoreText;
    [Tooltip("時間のテキスト")]
    Text _timerText;
    GameObject _plusTimer;
    Text _plusTimerText;
    [SerializeField] GameObject _resultMenu;
    [Tooltip("リザルト画面のスコアのテキスト")]
    [SerializeField] Text _resultText;
    [Tooltip("スコア")]
    [SerializeField]public int _score = 0;
    [Tooltip("スコアの限界値")]
    [SerializeField] int _maxScore = 9999999;
    [Tooltip("スコアを加算する時間")]
    [SerializeField] float _scoreTime = 1f;
    [SerializeField] float _countDownTime = 300f;
    int _addScore;
    bool _isStart;
    public bool _start => _isStart;
    float _time = 0f;
    event Func<int, int, int> _onAddScore;
    protected override bool _dontDestroyOnLoad { get { return true; } }
    int _towerCount = 0;
    public int towerCount => _towerCount;

    public Func<int,int,int> OnGetScore
    {
        get => _onAddScore;
        set => _onAddScore = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GameObject.FindGameObjectWithTag("Score")?.GetComponent<Text>();
        _timerText = GameObject.FindGameObjectWithTag("Timer")?.GetComponent<Text>();
        _plusScore = GameObject.FindGameObjectWithTag("PlusScore");
        _plusTimer = GameObject.FindGameObjectWithTag("PlusTimer");
        _plusScoreText = _plusScore?.GetComponent<Text>();
        _plusTimerText = _plusTimer?.GetComponent<Text>();
        _plusScore?.SetActive(false);
        _plusTimer?.SetActive(false);
        _resultMenu?.SetActive(false);
        _time = _countDownTime;
        _towerCount = 0;

        //スコアの初期化
        ShowScore();
        _score = 0;
        if (_scoreText)
        {
            _scoreText.text = _score.ToString("0000000");
            _isStart = true;
        }
    }

    /// <summary>スコアの表示(リザルト画面のみ)</summary>
    public void ShowScore()
    {
        Text text = _resultText?.GetComponent<Text>();

        if(text)
        {
            text.text = _score.ToString("0000000");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStart)
        {
            _timerText.text = string.Format("{0:00.00}", _time);
            _time = Mathf.Max(_time - Time.deltaTime, 0f);
        }
        if(_time == 0f && _isStart)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            var poolObj = GameObject.FindGameObjectsWithTag("PoolObj");
            foreach (var i in poolObj)
            {
                i.SetActive(false);
            }
            var enemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var i in enemy)
            {
                Destroy(i);
            }
            _isStart = false;
                    ShowScore();
            _resultMenu.SetActive(true);
        }
    }

    public void TowerCountAdd()
    {
        _towerCount++;
    }

    public void AddScore(int getScore)
    {
        _score = _onAddScore.Invoke(getScore, _score);
        _addScore += getScore;
        _plusScoreText.text = "+" + _addScore;
        StartCoroutine(PlusTime(_plusScore, getScore));
    }

    public void AddTime(int getTime)
    {
        _time = Mathf.Max(_time + getTime,0f);
        _plusTimerText.text = getTime + "秒";
        StartCoroutine(PlusTime(_plusTimer,getTime));
    }

    IEnumerator PlusTime(GameObject text,int num)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
        if(_addScore != 0)_addScore = 0;
    }

    public void GameManagerReset()
    {
        Start();
    }
    private void OnLevelWasLoaded(int level)
    {
        _time = _countDownTime;
    }
}
