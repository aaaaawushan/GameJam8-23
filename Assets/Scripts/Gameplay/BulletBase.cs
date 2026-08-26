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

}
