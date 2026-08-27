using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public void Pause()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void Resume()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
}
