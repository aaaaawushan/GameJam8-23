using UnityEngine;

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
        SystemCursorManager.Instance.HideCursor();
    }
}
