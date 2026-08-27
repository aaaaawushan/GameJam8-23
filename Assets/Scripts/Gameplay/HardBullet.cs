using UnityEngine;

public class HardBullet : BulletBase
{
    public override void OnHit()
    {
        hitCount--;
        if (hitCount <= 0)
        {
            StartCoroutine(ShakeAndDestroy());
        }
    }


}
