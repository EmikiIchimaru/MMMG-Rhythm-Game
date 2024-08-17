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
    private const int MISS_SCORE = 0;

    // Define combo thresholds for multiplier
    private readonly int[] comboThresholds = { 10, 20, 30 };

    void Start()
    {
        // Initialize score, combo, and multiplier
        score = 0;
        comboCount = 0;
        multiplier = 1;

        UpdateUI();
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Get the touch position and convert to world point
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    // Determine the hit timing and expected note timing
                    float hitTiming = Time.time; // Example timing of player input
                    float noteTiming = 5.0f; // Example expected timing of the note

                    // Call the scoring method
                    OnNoteHit(hitTiming, noteTiming);
                }
            }
        }
    }

    public void OnNoteHit(float hitTiming, float noteTiming)
    {
        float timeDifference = Mathf.Abs(hitTiming - noteTiming);
        int points = 0;

        if (timeDifference <= 0.05f) // Perfect hit
        {
            points = PERFECT_SCORE;
            comboCount++;
        }
        else if (timeDifference <= 0.15f) // Good hit
        {
            points = GOOD_SCORE;
            comboCount++;
        }
        else // Miss
        {
            points = MISS_SCORE;
            comboCount = 0;
            multiplier = 1; // Reset multiplier on miss
        }

        // Update multiplier based on combo count
        foreach (int threshold in comboThresholds)
        {
            if (comboCount == threshold)
            {
                multiplier++;
                break;
            }
        }

        // Calculate final score
        score += points * multiplier;

        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update UI elements
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + comboCount;
        multiplierText.text = "Multiplier: x" + multiplier;
    }

    // Example of how to reset the score system (e.g., on game restart)
    public void ResetScore()
    {
        score = 0;
        comboCount = 0;
        multiplier = 1;
        UpdateUI();
    }
}
