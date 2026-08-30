using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverSFX : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverClip);
        }
    }
}