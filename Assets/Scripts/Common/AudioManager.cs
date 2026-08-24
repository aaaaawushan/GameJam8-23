using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider bgmSlider;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgmSlider.value = audioSource.volume;
        audioSource.Play();
    }
    public void SetBGMVolume(float value)
    {
        audioSource.volume = value;
    }
}
