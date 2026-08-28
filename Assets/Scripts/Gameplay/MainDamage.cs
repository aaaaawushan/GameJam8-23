using UnityEngine;
using UnityEngine.UIElements;


public class MainDamage : DamageBase
{
    [SerializeField] private GameObject fullHPImage;
    [SerializeField] private GameObject damagedImage;
    [SerializeField] private Animator playerAnimator;
    void Start()
    {
        fullHPImage.SetActive(true);
        damagedImage.SetActive(false);
    }
    public override void TakeDamage()
    {
        base.TakeDamage();
        playerAnimator.SetTrigger("isHurt");
        if (hp == 1)
        {
            fullHPImage.SetActive(false);
            damagedImage.SetActive(true);

        }
        else if (hp <= 0)
        {
            fullHPImage.SetActive(false);
            damagedImage.SetActive(false);
          
        }
    }


}
