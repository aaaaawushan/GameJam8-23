using System.Collections;
using UnityEngine;
using TMPro;

public abstract class BulletBase : MonoBehaviour
{

    public float speed;
    public int hitCount;
    [SerializeField] private string[] dialogueTexts;

    private BulletSpawner spawner;
    private Transform spawnPoint;
    private TextMeshPro textMesh;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        if (textMesh != null && dialogueTexts != null && dialogueTexts.Length > 0)
        {
            textMesh.text = dialogueTexts[Random.Range(0, dialogueTexts.Length)];
        }
    }

    void Update()
    {
        if (MainPause.Instance.IsPaused) return;
        transform.position += Vector3.left * speed * Time.deltaTime;
        float leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        if (transform.position.x < leftEdge)
        {
            FindFirstObjectByType<DamageBase>().TakeDamage();
            Destroy(gameObject);
        }
    }
    public abstract void OnHit();
  

    public void SetSpawner(BulletSpawner spawner, Transform point)
    {
        this.spawner = spawner;
        this.spawnPoint = point;
    }
    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.ReleasePoint(spawnPoint);
        }
    }
    public IEnumerator ShakeAndDestroy()
    {
        Vector3 originalPos = transform.position;
        Vector3 originalScale = transform.localScale;
        float duration = 0.4f;    
        float magnitude = 0.15f; 
        float timer = 0f;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color color = sr.color;

        while (timer < duration)
        {
            float progress = timer / duration; // 0 ¨ 1

           
            float shake = magnitude * (1f - progress);
            float offsetX = Random.Range(-shake, shake);
            float offsetY = Random.Range(-shake, shake);
            transform.position = originalPos + new Vector3(offsetX, offsetY, 0);

           
            transform.localScale = originalScale * (1f - progress);

           
            color.a = 1f - progress;
            sr.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
