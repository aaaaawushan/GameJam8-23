using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public void Pause()
    {
        settingsPanel.SetActive(true);
        var cm = FindAnyObjectByType<CursorManager>();
      if (cm != null) cm.SetDefaultCursor(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        settingsPanel.SetActive(false);
        var cm = FindAnyObjectByType<CursorManager>();
        if (cm != null) cm.SetDefaultCursor(false);
        if (MainPause.Instance != null && MainPause.Instance.IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}