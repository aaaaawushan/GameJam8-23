using UnityEngine;

public class NormalBullet : BulletBase
{
    public override void OnHit()
    {
        StartCoroutine(ShakeAndDestroy());
    }

}
