using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI multiplierText;

    private int score;
    private int currentCombo;
    private int multiplier;

    // Define score values
    private const int PERFECT_SCORE = 100;
    private const int GOOD_SCORE = 50;

    // Define combo thresholds for multiplier
    private readonly int[] comboThresholds = { 10, 20, 30 };

    private void Start()
    {
        ResetScore();
    }

    public void OnNoteHit(float hitTiming, float realtimeHit)
    {
        if (Mathf.Abs(hitTiming - realtimeHit) <= 0.1f) // Perfect hit threshold
        {
            currentCombo++;
            multiplier = GetMultiplier(currentCombo);
            score += PERFECT_SCORE * multiplier;

            Debug.Log($"Perfect hit! Score: {score}, Combo: {currentCombo}, Multiplier: {multiplier}");
        }
        else if (Mathf.Abs(hitTiming - realtimeHit) <= 0.25f) // Good hit threshold
        {
            currentCombo++;
            multiplier = GetMultiplier(currentCombo);
            score += GOOD_SCORE * multiplier;

            Debug.Log($"Good hit! Score: {score}, Combo: {currentCombo}, Multiplier: {multiplier}");
        }
        else
        {
            OnNoteMissed();
        }

        UpdateScoreUI();
    }

    public void OnNoteMissed()
    {
        currentCombo = 0;
        multiplier = 1;
        UpdateScoreUI();

        Debug.Log($"Miss! Combo reset to: {currentCombo}, Multiplier reset to: {multiplier}");
    }

    private int GetMultiplier(int combo)
    {
        if (combo >= comboThresholds[2]) return 4;
        if (combo >= comboThresholds[1]) return 3;
        if (combo >= comboThresholds[0]) return 2;
        return 1;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
        comboText.text = $"Combo: {currentCombo}";
        multiplierText.text = $"Multiplier: x{multiplier}";
    }

    public void ResetScore()
    {
        score = 0;
        currentCombo = 0;
        multiplier = 1;
        UpdateScoreUI();
    }
}
