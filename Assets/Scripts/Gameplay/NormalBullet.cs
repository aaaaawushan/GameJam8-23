using UnityEngine;

public class NormalBullet : BulletBase
{
    [SerializeField] private GameObject destroyEffect;
    public override void OnHit()
    {
        if (MainPause.Instance.IsPaused) return;
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        StartCoroutine(ShakeAndDestroy());
    }

}
