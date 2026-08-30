
using UnityEngine;

public class BossSuperBullet : BossBulletBase
{
    [SerializeField] private Sprite damagedSprite1;
    [SerializeField] private Sprite damagedSprite2;
    [SerializeField] private GameObject hitEffect;

    protected override void OnHit()
    {
        hitCount--;
        if (hitCount == 2)
        {
            sr.sprite = damagedSprite1;
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        else if (hitCount == 1)
        {
            sr.sprite = damagedSprite2;
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        if (hitCount <= 0)
        {
            if (destroyEffect != null)
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            BossManager.Instance.OnBulletDestroyed();
            Destroy(gameObject);
        }
    }
}