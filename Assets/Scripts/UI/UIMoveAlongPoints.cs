using UnityEngine;

public class UIMoveAlongPoints : MonoBehaviour
{
    [SerializeField] private RectTransform[] targetPoints;
    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private bool loop = true;
    [SerializeField] private string nextSceneName;
    [SerializeField] private SceneLoader sceneLoader;

    private int _currentIndex = 0;
    private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetPoints == null || targetPoints.Length == 0 || _rectTransform == null) return;

        RectTransform target = targetPoints[_currentIndex];
        if (target == null) return;

        _rectTransform.anchoredPosition = Vector2.MoveTowards(
            _rectTransform.anchoredPosition,
            target.anchoredPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(_rectTransform.anchoredPosition, target.anchoredPosition) < 0.5f)
        {
            _currentIndex++;
            if (_currentIndex >= targetPoints.Length)
            {
                if (loop)
                {
                    _currentIndex = 0;
                }
                else
                {
                    _currentIndex = targetPoints.Length - 1;
                    enabled = false;
                    if (!string.IsNullOrEmpty(nextSceneName))
                    {
                        sceneLoader?.LoadScene(nextSceneName);
                    }
                }
            }
        }
    }
}
