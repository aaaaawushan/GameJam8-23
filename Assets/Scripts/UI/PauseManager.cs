using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public void Pause()
    {
        settingsPanel.SetActive(true);
        SystemCursorManager.Instance.ShowCustomCursor();
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;

        string scene = SceneManager.GetActiveScene().name;
        if (scene == "MainScene" || scene == "BossScene")
        {
            SystemCursorManager.Instance.HideCursor();
        }
        else
        {
            SystemCursorManager.Instance.ShowCustomCursor();
        }
    }
}