using UnityEngine;
public class DamageBase : MonoBehaviour
{
    [SerializeField] protected int hp;

    public virtual void TakeDamage()
    {
        hp--;
        CameraShake.Instance.Shake();
    }
}