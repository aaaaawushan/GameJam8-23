using UnityEngine;
using UnityEngine.UI;

public class MainPause : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Sprite playIcon;   
    [SerializeField] private Sprite pauseIcon; 

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseButton.image.sprite = playIcon; 
        }
        else
        {
            Time.timeScale = 1f;
            pauseButton.image.sprite = pauseIcon; 
        }
    }
}

