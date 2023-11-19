using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int PlayerScore { get; private set; } = 0;

    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void IncreaseScore(int points)
    {
        PlayerScore += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + PlayerScore;
        }
    }
}