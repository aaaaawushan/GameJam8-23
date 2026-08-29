using UnityEngine;
using UnityEngine.InputSystem;

public class BossBulletBase : MonoBehaviour
{
    [SerializeField] protected int hitCount = 1;
    [SerializeField] protected float lifetime = 2f;
    [SerializeField] protected float flickerSpeed = 5f;
    [SerializeField] protected GameObject destroyEffect;

    protected SpriteRenderer sr;
    private float timer;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        timer = lifetime;
    }

    void Update()
    {
      
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * flickerSpeed));
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;

        
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }

        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnHit();
            }
        }
    }

    protected virtual void OnHit()
    {
        hitCount--;
        if (hitCount <= 0)
        {
            if (destroyEffect != null)
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            BossManager.Instance.OnBulletDestroyed(); 
            Destroy(gameObject);
        }
    }
}