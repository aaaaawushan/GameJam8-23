using System.Collections;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{

    public float speed;
    public int hitCount;

    private BulletSpawner spawner;
    private Transform spawnPoint;
    void Update()
    {
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
        float duration = 0.15f;
        float magnitude = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            float offsetX = Random.Range(-magnitude, magnitude);
            float offsetY = Random.Range(-magnitude, magnitude);
            transform.position = originalPos + new Vector3(offsetX, offsetY, 0);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
