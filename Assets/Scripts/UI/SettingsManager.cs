using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject bgmPanel;
    [SerializeField] private Slider bgmSlider;

    void Start()
    {
        if (AudioManager.Instance != null && bgmSlider != null)
        {
            bgmSlider.value = AudioManager.Instance.masterVolume;
            bgmSlider.onValueChanged.RemoveAllListeners();
            bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        }
    }

    public void openBgm()
    {
        bgmPanel.SetActive(true);
    }

    public void closeBgm()
    {
        bgmPanel.SetActive(false);
    }
}
