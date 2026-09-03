using UnityEngine;
using UnityEngine.UI;

public class IntroSkip : MonoBehaviour
{
    [SerializeField] private GameObject skipButton;
    private bool hasVisited=false;

    private void Start()
    {
        Debug.Log(hasVisited);
        skipButton.SetActive(hasVisited);
        hasVisited = true;
    }
}
