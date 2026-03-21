using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;
    public int GetScore() => _score;

    private void Start()
    {
        CustomEvents.OnChangeScore += ChangeScore;
    }

    public void LoadScore(int score)
    {
        _score = score;
        UpdateScoreText();
    }

    private void ChangeScore(int amount)
    {
        _score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }


    private void OnDestroy()
    {
        CustomEvents.OnChangeScore -= ChangeScore;
    }
}
