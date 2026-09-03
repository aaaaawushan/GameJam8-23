using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    [Range(0f, 1f)] public float masterVolume = 1f;
    public System.Action OnBGMFinished;

    private float bgmBaseVolume = 1f;
    private float sfxBaseVolume = 2f;

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

    }

    public void PlayBGM(AudioSource source)
    {
        if (source == null || source.clip == null) return;

        bgmSource.clip = source.clip;
        bgmSource.loop = source.loop;
        bgmBaseVolume = source.volume;
        bgmSource.volume = bgmBaseVolume * masterVolume;
        bgmSource.Play();

        StopAllCoroutines();
        if (!bgmSource.loop)
        {
            StartCoroutine(WaitForBGMEnd());
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    private IEnumerator WaitForBGMEnd()
    {
        if (bgmSource == null || bgmSource.clip == null) yield break;

        float clipLength = bgmSource.clip.length;

        while (bgmSource != null && bgmSource.time < clipLength - 0.05f)
        {
            
            if (!bgmSource.isPlaying && bgmSource.time > 0f)
            {
                
                yield return null;
                continue;
            }
            yield return null;
        }

        OnBGMFinished?.Invoke();
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
    public void PauseBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
    }

    public void ResumeBGM()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.UnPause();
        }
    }
}
