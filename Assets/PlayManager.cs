using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : Singleton<PlayManager>
{
    public void PlayerInput(int touchIndex, int laneIndex, Touch touch)
    {
        Debug.Log($"PlayerInput: {touchIndex}, {laneIndex}, {touch.phase}");
    }
}
