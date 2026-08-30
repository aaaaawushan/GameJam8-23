using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private string victorySceneName;
    [SerializeField] private float bulletFadeDuration = 0.3f;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnBGMFinished += OnVictory;
        }
    }

    void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnBGMFinished -= OnVictory;
        }
    }

    private void OnVictory()
    {
        if (MainPause.Instance.IsPaused) return;
        var damage = FindAnyObjectByType<MainDamage>();
        if (damage != null && damage.hp <= 0) return;
        ClearAllBullets();

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isSurvived", true);
            StartCoroutine(WaitForSurviveAnimation());
        }
        else
        {
            LoadVictoryScene();
        }
    }

    private void ClearAllBullets()
    {
        BulletBase[] bullets = FindObjectsByType<BulletBase>(FindObjectsSortMode.None);
        foreach (BulletBase bullet in bullets)
        {
            StartCoroutine(FadeOutBullet(bullet.gameObject));
        }
    }

    private IEnumerator FadeOutBullet(GameObject bullet)
    {
        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
        Image img = bullet.GetComponent<Image>();

        Color color;
        if (sr != null) color = sr.color;
        else if (img != null) color = img.color;
        else yield break;

        float startAlpha = color.a;
        float timer = 0f;

        while (timer < bulletFadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0f, timer / bulletFadeDuration);

            if (sr != null) sr.color = color;
            if (img != null) img.color = color;

            yield return null;
        }

        Destroy(bullet);
    }

    private IEnumerator WaitForSurviveAnimation()
    {
        while (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Panic"))
        {
            yield return null;
        }

        float waitTime = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(waitTime);

        LoadVictoryScene();
    }

    private void LoadVictoryScene()
    {
        if (!string.IsNullOrEmpty(victorySceneName))
        {
            SceneManager.LoadScene(victorySceneName);
        }
    }
}
