using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    int _maxScore = 9999999;
    Text _scoreText;
    [SerializeField] float _scoreTime = 1f;

    private void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGetScore += AddScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGetScore -= AddScore;
    }
    /// <summary>スコアの追加</summary>
    /// <param name="tempscore"></param>
    public int AddScore(int tempscore,int score)
    {
        //スコアを加算する前のスコアの代入。
        float tempScore = score;
        //2つの値の小さい方がスコアに代入される。
        score = Mathf.Min(score + tempscore, _maxScore);
        //スコアを加算する前のスコアから後のスコアまでの値を小さい値を代入しながら後のスコアの値になるまで代入する。
        DOTween.To(() => tempScore, x => { _scoreText.text = x.ToString("0000000"); }, score, _scoreTime)
            .OnComplete(() => _scoreText.text = score.ToString("0000000"));
        _scoreText.text = score.ToString("0000000");
        return score;

    }
}
