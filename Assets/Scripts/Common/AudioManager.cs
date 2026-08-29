using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    [Range(0f, 1f)] public float masterVolume = 1f;

    private float bgmBaseVolume = 1f;
    private float sfxBaseVolume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        EnsureSources();
    }

    void EnsureSources()
    {
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.playOnAwake = false;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }

        sfxBaseVolume = sfxSource.volume;
    }

    public void PlayBGM(AudioSource source)
    {
        if (source == null || source.clip == null) return;
        if (bgmSource.clip == source.clip) return;

        bgmSource.clip = source.clip;
        bgmSource.loop = source.loop;
        bgmBaseVolume = source.volume;
        bgmSource.volume = bgmBaseVolume * masterVolume;
        bgmSource.Play();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        if (bgmSource != null)
        {
            bgmSource.volume = bgmBaseVolume * masterVolume;
        }
    }

    public void PlayHitSFX()
    {
        if (sfxSource != null && sfxSource.clip != null)
        {
            sfxSource.PlayOneShot(sfxSource.clip, sfxBaseVolume * masterVolume);
        }
    }
}
