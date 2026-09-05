using UnityEngine;
using UnityEngine.InputSystem;

public class BossBulletBase : MonoBehaviour
{
    [SerializeField] protected int hitCount = 1;
    [SerializeField] protected float lifetime = 2f;
    [SerializeField] protected GameObject destroyEffect;

    [SerializeField] private TMPro.TextMeshProUGUI text;


    protected SpriteRenderer sr;
    private float timer;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        timer = lifetime;
    }

    void Update()
    {
        float halfLife = lifetime / 2f;
        float elapsed = lifetime - timer;

        
        float alpha;
        if (elapsed < halfLife)
        {
            alpha = elapsed / halfLife;        
        }
        else
        {
            alpha = timer / halfLife;         
        }

        Color c = sr.color;
        c.a = alpha;
        sr.color = c;

        if (text != null)
        {
            Color tc = text.color;
            tc.a = alpha;
            text.color = tc;
        }

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
                AudioManager.Instance?.PlayHitSFX();
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