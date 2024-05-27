using UnityEngine;

public static class Utility
{
    public static float baseSpeed = 20f;
    public static int CoorXToLane(float inputX, out bool isValid)
    {
    
        float tempFloat = Mathf.Round((inputX + 960) /10f);
        int tempInt =  (int) tempFloat;
        isValid = (tempInt >= -3 && tempInt <= 3);
        return tempInt;
    }

    public static float TimePositionToRealtime(int timePos, float bpm)
    {
        return timePos * 60f / bpm;
    }

    public static bool ShouldInstantiateNote(float realTimeHit, float currentTime, float approachRate)
    {
        //create an out parameter for minor offset adjustments

        float travelTime = 140f / (baseSpeed * approachRate);
        Debug.Log((realTimeHit - currentTime - travelTime) < 0);
        return ((realTimeHit - currentTime - travelTime) < 0);
    }

    
}
