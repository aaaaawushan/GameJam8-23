using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class IntroManager : MonoBehaviour
{
    [System.Serializable]
    public class IntroPage
    {
        public Sprite image;
        [TextArea(1, 6)]
        public string text;
    }
    [Header("Page Date")]
    [SerializeField] private IntroPage[] pages;


    [Header("UI")]
    [SerializeField] private Image displayImage;
    [SerializeField] private CanvasGroup imageCanvasGroup;
    [SerializeField] private TypewriterText typewriter;
    [SerializeField] private CanvasGroup textCanvasGroup;
    [SerializeField] private float pageFadeDuration = 1f;

    [Header("Presentation Settings")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float delayBetweenPages = 0.3f;
    [SerializeField] private float gameInstructionTime = 3f;
    private bool isLastPage;

    [Header("Over")]
    [SerializeField] private string nextSceneName = "";

    private bool _waitingForClick;
    private bool _clicked;


    private void Start()
    {
        StartCoroutine(PlayIntro());
    }
    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return;
        if (isLastPage) return;

        if (typewriter.isTyping)
        {
            typewriter.Skip();
        }
        else if (_waitingForClick)
        {
            _clicked = true;
        }
    }
    private IEnumerator PlayIntro()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            var page = pages[i];
            bool sameAsPrevious = displayImage.sprite == page.image;

            if (!sameAsPrevious)
            {
                displayImage.sprite = page.image;
            }

            if (i == 0)
            {
                imageCanvasGroup.alpha = 1;
            }
            else if (!sameAsPrevious)
            {
                yield return StartCoroutine(Fade(0f, 1f));
            }

            typewriter.Play(page.text);
            textCanvasGroup.alpha = 1f;

            _waitingForClick = true;
            _clicked = false;
            yield return new WaitUntil(() => _clicked);
            _waitingForClick = false;

            bool sameAsNext = i < pages.Length - 1 && pages[i + 1].image == page.image;

            if (!sameAsNext)
            {
                StartCoroutine(FadeText(1f, 0f));
                yield return StartCoroutine(Fade(1f, 0f));
            }
            else
            {
                yield return StartCoroutine(FadeText(1f, 0f));
            }
            typewriter.Stop();


            if (i < pages.Length - 1)
            {
                yield return new WaitForSeconds(delayBetweenPages);
            }
        }
        OnIntroComplete();
    }
    private IEnumerator FadeText(float from, float to)
    {
        float timer = 0f;
        textCanvasGroup.alpha = from;
        while (timer < pageFadeDuration)
        {
            timer += Time.deltaTime;
            textCanvasGroup.alpha = Mathf.Lerp(from, to, timer / pageFadeDuration);
            yield return null;
        }
        textCanvasGroup.alpha = to;
    }
    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        imageCanvasGroup.alpha = from;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / fadeDuration);
            imageCanvasGroup.alpha = Mathf.SmoothStep(from, to, t);
            yield return null;
        }
        imageCanvasGroup.alpha = to;
    }
    private void OnIntroComplete()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
