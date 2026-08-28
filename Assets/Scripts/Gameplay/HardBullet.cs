using UnityEngine;
using UnityEngine.UI;

public class HardBullet : BulletBase
{
    [SerializeField] private Sprite lastHitPic;
    [SerializeField] private GameObject hitEffect;     
    [SerializeField] private GameObject destroyEffect;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void OnHit()
    {
        hitCount--;
        if (hitCount == 1)
        {
            sr.sprite = lastHitPic;
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        if (hitCount <= 0)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            StartCoroutine(ShakeAndDestroy());
        }
    }


}
