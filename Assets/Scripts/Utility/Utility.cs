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

    public static float TimePositionToRealtime(int timePos, float bpm)
    {
        return timePos * 60f / bpm;
    }

    public static bool ShouldInstantiateNote(float realTimeHit, float currentTime, float approachRate)
    {
        //create an out parameter for minor offset adjustments

        float travelTime = GameManager.Instance.spawnDistance / (baseSpeed * approachRate);
        //Debug.Log((realTimeHit - currentTime - travelTime) < 0);
        return ((realTimeHit - currentTime - travelTime) < 0);
    }


    
}
