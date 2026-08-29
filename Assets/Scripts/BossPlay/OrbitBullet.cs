using UnityEngine;

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
    }

    void OnMouseDown()
    {
        FindFirstObjectByType<DamageBase>().TakeDamage();
    }
}