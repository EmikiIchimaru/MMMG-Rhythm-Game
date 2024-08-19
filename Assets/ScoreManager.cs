using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI multiplierText;

    private int score;
    private int comboCount;
    private int multiplier;

    // Define score values
    private const int PERFECT_SCORE = 100;
    private const int GOOD_SCORE = 50;

    // Define thresholds for judging the note hits
    private float perfectThreshold = 0.1f;
    private float goodThreshold = 0.3f;

    private void Start()
    {
        ResetScore();
    }

    // Method to handle note hits
    public void OnNoteHit(float hitTiming, float realtimeHit)
    {
        if (Mathf.Abs(hitTiming - realtimeHit) <= perfectThreshold)
        {
            comboCount++;
            multiplier = GetMultiplier();
            score += PERFECT_SCORE * multiplier;
            Debug.Log($"Perfect hit! Score: {score}, Combo: {comboCount}, Multiplier: {multiplier}");
            UpdateScoreUI();
        }
        else if (Mathf.Abs(hitTiming - realtimeHit) <= goodThreshold)
        {
            comboCount++;
            multiplier = GetMultiplier();
            score += GOOD_SCORE * multiplier;
            Debug.Log($"Good hit! Score: {score}, Combo: {comboCount}, Multiplier: {multiplier}");
            UpdateScoreUI();
        }
        else
        {
            comboCount = 0;
            multiplier = 1;
            Debug.Log($"Miss! Score: {score}, Combo reset to: {comboCount}, Multiplier reset to: {multiplier}");
            UpdateScoreUI();
        }
    }

    // Update the score, combo, and multiplier UI elements
    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
        comboText.text = $"Combo: {comboCount}";
        multiplierText.text = $"Multiplier: x{multiplier}";
    }

    // Calculate the multiplier based on the current combo count
    private int GetMultiplier()
    {
        if (comboCount >= 30) return 4;
        if (comboCount >= 20) return 3;
        if (comboCount >= 10) return 2;
        return 1;
    }

    // Reset score, combo, and multiplier
    public void ResetScore()
    {
        score = 0;
        comboCount = 0;
        multiplier = 1;
        UpdateScoreUI();
    }
}
