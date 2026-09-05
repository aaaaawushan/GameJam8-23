using UnityEngine;
using UnityEngine.UI;

public class MainPause : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Sprite playIcon;   
    [SerializeField] private Sprite pauseIcon;

    public static MainPause Instance { get; private set; }

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            Time.timeScale = 0f;
            pauseButton.image.sprite = playIcon;
            AudioManager.Instance.PauseBGM();
          Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseButton.image.sprite = pauseIcon;
            AudioManager.Instance.ResumeBGM();
           Cursor.visible = false;
        }
    }
}

