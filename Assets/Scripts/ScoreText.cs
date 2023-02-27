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
    /// <summary>�X�R�A�̒ǉ�</summary>
    /// <param name="tempscore"></param>
    public int AddScore(int tempscore,int score)
    {
        //�X�R�A�����Z����O�̃X�R�A�̑���B
        float tempScore = score;
        //2�̒l�̏����������X�R�A�ɑ�������B
        score = Mathf.Min(score + tempscore, _maxScore);
        //�X�R�A�����Z����O�̃X�R�A�����̃X�R�A�܂ł̒l���������l�������Ȃ����̃X�R�A�̒l�ɂȂ�܂ő������B
        DOTween.To(() => tempScore, x => { _scoreText.text = x.ToString("0000000"); }, score, _scoreTime)
            .OnComplete(() => _scoreText.text = score.ToString("0000000"));
        _scoreText.text = score.ToString("0000000");
        return score;

    }
}
