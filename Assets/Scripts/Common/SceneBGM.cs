using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    public AudioSource sceneAudioSource;

    void Start()
    {
        if (AudioManager.Instance != null && sceneAudioSource != null && sceneAudioSource.clip != null)
        {
            AudioManager.Instance.PlayBGM(sceneAudioSource);
        }
    }

    void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM();
        }
    }
}
