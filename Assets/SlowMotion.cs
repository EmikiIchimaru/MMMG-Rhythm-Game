using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionScale = 0.4f; // The time scale during slow motion
    public float normalTimeScale = 1.0f; // The normal time scale
    public float transitionDuration = 1.0f; // The duration for the transition

    private bool isSlowMotion = false;
    private float targetTimeScale;
    private float timeScaleVelocity = 0.0f;

    void Update()
    {
        // Toggle slow motion on or off with the S key
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSlowMotion = !isSlowMotion;
            targetTimeScale = isSlowMotion ? slowMotionScale : normalTimeScale;
        }

        // Smoothly transition to the target time scale
        Time.timeScale = Mathf.SmoothDamp(Time.timeScale, targetTimeScale, ref timeScaleVelocity, transitionDuration);

        // Optional: Adjust the fixedDeltaTime to maintain consistent physics simulation
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
