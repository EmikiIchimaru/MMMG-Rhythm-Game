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
}
