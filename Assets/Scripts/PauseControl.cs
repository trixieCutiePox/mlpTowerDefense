using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gameIsPaused);
        }
    }

    public static void PauseGame(bool pause)
    {
        gameIsPaused = pause;
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
