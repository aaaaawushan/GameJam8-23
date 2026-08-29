using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [Header("Boss Pic")]
    [SerializeField] private SpriteRenderer bossRenderer;
    [SerializeField] private Sprite breathSprite1;
    [SerializeField] private Sprite breathSprite2;
    [SerializeField] private float breathInterval = 0.5f;
    private bool isHurt = false;
    [SerializeField] private Sprite hurtSprite1;
    [SerializeField] private Sprite hurtSprite2;
    [SerializeField] private float hurtDisplayTime = 0.3f;

    [Header("HP")]
    [SerializeField] private int hp = 3;

    [Header("Scene")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private string failSceneName;
    private bool isBossDefeated = false;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float elapsed = 0f;

    [Header("Dialogue")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private float spawnInterval = 2f;


    [Header("OrbitBullet")]
    [SerializeField] private GameObject[] orbitGroup1;
    [SerializeField] private GameObject[] orbitGroup2;
    [SerializeField] private GameObject[] orbitGroup3;
    [SerializeField] protected GameObject destroyEffect;
    private bool isSpawning = true;

    [Header("BadGone")]
    [SerializeField] private int requiredKills = 5;

    private int currentKills = 0;
    private bool phase1TimePassed = false;
    private bool phase2TimePassed = false;
    private bool phase3TimePassed = false;
    private bool phase1Cleared = false;
    private bool phase2Cleared = false;
    private bool phase3Cleared = false;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnLoop());
        StartCoroutine(OrbitBulletTimeline());
        StartCoroutine(BreathLoop());
    }
    IEnumerator BreathLoop()
    {
        while (true)
        {
            if (!isHurt)
            {
                bossRenderer.sprite = breathSprite1;
                yield return new WaitForSeconds(breathInterval);
                if (!isHurt)
                {
                    bossRenderer.sprite = breathSprite2;
                    yield return new WaitForSeconds(breathInterval);
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    IEnumerator SpawnLoop()
    {
        while (isSpawning && hp > 0)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject prefab = bulletPrefabs[Random.Range(0, bulletPrefabs.Length)];

            Instantiate(prefab, point.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void TakeDamage()
    {
        hp--;
        StartCoroutine(HurtEffect());

        if (hp <= 0)
        {
            isSpawning = false;
            isBossDefeated = false; 
            StartCoroutine(FadeEffect());
        }
    }

    IEnumerator HurtEffect()
    {
        isHurt = true;
        bossRenderer.sprite = hurtSprite1;
        yield return new WaitForSeconds(hurtDisplayTime);
        bossRenderer.sprite = hurtSprite2;
        yield return new WaitForSeconds(hurtDisplayTime);
        isHurt = false;
    }
    IEnumerator FadeEffect()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = elapsed / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
        SceneManager.LoadScene(isBossDefeated ? nextSceneName : failSceneName);
    }

    void DestroyGroup(GameObject[] group)
    {
        foreach (var obj in group)
        {
            if (obj != null)
            {
                if (destroyEffect != null)
                    Instantiate(destroyEffect, obj.transform.position, Quaternion.identity);
                Destroy(obj);
            }
        }
    }

    IEnumerator OrbitBulletTimeline()
    {
        yield return new WaitForSeconds(30f);
        phase1TimePassed = true;
        CheckPhase();

        yield return new WaitForSeconds(30f);
        phase2TimePassed = true;
        CheckPhase();

        yield return new WaitForSeconds(16f);
        phase3TimePassed = true;
        CheckPhase();
        yield return new WaitForSeconds(3f);


        if (phase1Cleared && phase2Cleared && phase3Cleared)
        {
            isBossDefeated = true; 
            isSpawning = false;
            StartCoroutine(FadeEffect());
        }
        else
        {
            isBossDefeated = false;
            isSpawning = false;
            StartCoroutine(FadeEffect());
        }
    }
    public void OnBulletDestroyed()
    {
        currentKills++;
        CheckPhase();
    }

    void CheckPhase()
    {
        if (!phase1Cleared && phase1TimePassed && currentKills >= requiredKills)
        {
            phase1Cleared = true;
            currentKills = 0;
            DestroyGroup(orbitGroup1);
        }
        else if (phase1Cleared && !phase2Cleared && phase2TimePassed && currentKills >= requiredKills)
        {
            phase2Cleared = true;
            currentKills = 0;
            DestroyGroup(orbitGroup2);
        }
        else if (phase2Cleared && !phase3Cleared && phase3TimePassed && currentKills >= requiredKills)
        {
            phase3Cleared = true;
            currentKills = 0;
            DestroyGroup(orbitGroup3);
        }
    }
}