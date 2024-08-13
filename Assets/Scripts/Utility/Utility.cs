using UnityEngine;

public static class Utility
{
    public static float baseSpeed = 20f;
    public static float hitPanelWidth = 1505f;
    public static float hitPanelMinY = -500f;
    public static float hitPanelMaxY = -200f;

    public static int LocalPointToLane(Vector2 inputPoint, out bool isValid)
    {
        float inputX = inputPoint.x;
        float inputY = inputPoint.y;
        float tempFloat = Mathf.Round((inputX/hitPanelWidth)*7f);
        int tempInt =  (int) tempFloat;
        isValid = (tempInt >= -3 && tempInt <= 3) && (inputY >= hitPanelMinY && inputY <= hitPanelMaxY);
        return tempInt;
    }

    public static float GetBaseTimeUnit(float bpm)
    {
        return 30f / bpm;
    }

    public static float TimePositionToRealtime(float timePos, float bpm, float offset)
    {
        return timePos * 30f / bpm + offset;
    }

    public static bool ShouldInstantiateNote(float realTimeHit, float currentTime, out float actualSpawnDistance)
    {
        //create an out parameter for minor offset adjustments

        float travelTime = GameManager.Instance.spawnDistance / (baseSpeed * GameManager.Instance.approachRate);
        actualSpawnDistance = (realTimeHit - currentTime) * (baseSpeed * GameManager.Instance.approachRate);
        //Debug.Log((realTimeHit - currentTime - travelTime) < 0);
        return ((realTimeHit - currentTime - travelTime) < 0);
    }


    
}
