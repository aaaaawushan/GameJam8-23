using UnityEngine;
public class DamageBase : MonoBehaviour
{
    public  int hp;

    public virtual void TakeDamage()
    {
        hp--;
        CameraShake.Instance.Shake();
    }
}