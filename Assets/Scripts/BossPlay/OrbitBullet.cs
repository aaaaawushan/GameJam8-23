using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitBullet : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private SpriteRenderer sr;


    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float x = originalPos.x + Random.Range(-shakeIntensity, shakeIntensity);
        float y = originalPos.y + Random.Range(-shakeIntensity, shakeIntensity);
        transform.position = new Vector3(x, y, 0);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                BossManager.Instance.TakeDamage();
            }
        }
    }

}