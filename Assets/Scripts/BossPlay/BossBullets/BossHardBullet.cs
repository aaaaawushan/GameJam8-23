using UnityEngine;

public class BossHardBullet : BossBulletBase
{
    [SerializeField] private Sprite damagedSprite;
    [SerializeField] private GameObject hitEffect;

    protected override void OnHit()
    {
        hitCount--;
        if (hitCount == 1)
        {
            sr.sprite = damagedSprite;
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