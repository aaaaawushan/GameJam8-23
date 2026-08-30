using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float holdDuration = 1f;

    private void Start()
    {
        StartCoroutine(SplashSequence());
    }

    private IEnumerator SplashSequence()
    {
        yield return StartCoroutine(Fade(0f, 1f));
        yield return new WaitForSeconds(holdDuration);
        yield return StartCoroutine(Fade(1f, 0f));
        SceneManager.LoadScene("TitleScene");
    }

    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        canvasGroup.alpha = from;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / fadeDuration);
            canvasGroup.alpha = Mathf.SmoothStep(from, to, t);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
