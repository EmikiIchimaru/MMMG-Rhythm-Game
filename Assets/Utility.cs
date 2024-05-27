using UnityEngine;

public static class Utility
{
    public static int CoorXToLane(float inputX, out bool isValid)
    {
    
        float tempFloat = Mathf.Round((inputX + 960) /10f);
        int tempInt =  (int) tempFloat;
        isValid = (tempInt >= -3 && tempInt <= 3);
        return tempInt;
    }

    public static float TimePositionToRealtime(int timePos, float bpm)
    {
        return timePos * 120f / bpm;
    }

    public static bool ShouldInstantiateNote(float realTimeHit, float currentTime, float approachRate)
    {
        //create an out parameter for minor offset adjustments
        float tempARMultiplier = 0.05f;
        Debug.Log((realTimeHit - currentTime - approachRate * tempARMultiplier) < 0);
        return ((realTimeHit - currentTime - approachRate * tempARMultiplier) < 0);
    }

    
}
