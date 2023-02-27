using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : SingletonMonovihair<GameManager>
{
    [Tooltip("�X�R�A�̃e�L�X�g")]
    Text _scoreText;
    [Tooltip("���Ԃ̃e�L�X�g")]
    Text _timerText;
    [Tooltip("���U���g��ʂ̃X�R�A�̃e�L�X�g")]
    [SerializeField] Text _resultText;
    [Tooltip("�X�R�A")]
    [SerializeField]public int _score = 0;
    [Tooltip("�X�R�A�̌��E�l")]
    [SerializeField] int _maxScore = 9999999;
    [Tooltip("�X�R�A�����Z���鎞��")]
    [SerializeField] float _scoreTime = 1f;
    [SerializeField] float _countDownTime = 300f;
    float time = 0f;
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
        _scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        _timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        //�X�R�A�̏�����
        ShowScore();
        _scoreText.text = _score.ToString("0000000");

    }

    /// <summary>�X�R�A�̕\��(���U���g��ʂ̂�)</summary>
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
        _timerText.text = string.Format("{0:00.00}",_countDownTime);
        _countDownTime = Mathf.Max(_countDownTime - Time.deltaTime, 0f);
    }

    public void TowerCountAdd()
    {
        _towerCount++;
    }

    public void AddScore(int getScore)
    {
        _score = _onAddScore.Invoke(getScore,_score);
    }
}
