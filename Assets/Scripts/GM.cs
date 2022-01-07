using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public bool isOnPause;
    public float clearTime;
    private float startTime;
    private float pauseStartTime;
    private float pauseEndTime;
    private bool hasBeenPaused;

    public void ClearTimeCheckStart()
    {
        clearTime = 0.0f;
        startTime = Time.time;
        return;
    }
    
    public void ClearTimeCheckEnd()
    {
        if (hasBeenPaused)
        {
            float pauseTime = pauseEndTime - pauseStartTime;
            clearTime = Time.time - pauseTime - startTime;
            hasBeenPaused = false;
        }
        else
        {
            clearTime = Time.time - startTime;
        }
       
        startTime = 0.0f;
        Debug.Log(clearTime);
        return;
    }

    public void Pause()
    {
        if (isOnPause)
        {
            Debug.LogError("Game is already on pause!"); 
            return;
        }
        else
        {
            isOnPause = true;
            hasBeenPaused = true;
            pauseStartTime = Time.time;
            Debug.Log(pauseStartTime);
            return;
        }
    }

    public void Unpause()
    {
        if (!isOnPause)
        {
            Debug.LogError("Game is not on pause state");
            return;
        }
        else
        {
            isOnPause = false;
            pauseEndTime = Time.time;
            Debug.Log(pauseEndTime);
            return;
        }
    }

}
