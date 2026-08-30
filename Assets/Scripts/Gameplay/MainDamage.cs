using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MainDamage : DamageBase
{
    [Header("Hp")]
    [SerializeField] private GameObject fullHPImage;
    [SerializeField] private GameObject damagedImage;
    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;
    [Header("EndEffect")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float elapsed = 0f;
    [SerializeField] private GameObject endPanel;
   
    void Start()
    {
        SystemCursorManager.Instance.HideCursor();
        fullHPImage.SetActive(true);
        damagedImage.SetActive(false);
        endPanel.SetActive(false);
    }
 
    public override void TakeDamage()
    {
        base.TakeDamage();
        playerAnimator.SetTrigger("isHurt");
        if (hp == 1)
        {
            fullHPImage.SetActive(false);
            damagedImage.SetActive(true);

        }
        else if (hp <= 0)
        {

            fullHPImage.SetActive(false);
            damagedImage.SetActive(false);
            AudioManager.Instance.StopBGM();
            StartCoroutine(FadeEffect());
        }
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
        SystemCursorManager.Instance.ShowCustomCursor();
      endPanel.SetActive(true);
    }


}
