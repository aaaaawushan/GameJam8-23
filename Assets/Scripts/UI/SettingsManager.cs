using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject bgmPanel;

    public void openBgm()
    {
        bgmPanel.SetActive(true);
    }
    public void closeBgm()
    {
        bgmPanel.SetActive(false);
    }
}
