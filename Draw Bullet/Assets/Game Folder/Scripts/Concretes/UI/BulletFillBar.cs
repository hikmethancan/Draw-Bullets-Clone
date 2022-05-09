
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BulletFillBar : MonoBehaviour
{
    [SerializeField] private GameObject wholeImage;
    [SerializeField] private Image fillImage;
    

    public void FillBulletImage(float duration)
    {
        DOTween.Complete("mainPlayerFillBar");
        var currentStackAmount = Player.Instance.GunController.CurrentBulletCount;
        var maxStackAmount = Player.Instance.GunController.MaxBulletCount;
        var fillAmount = Mathf.InverseLerp(0, maxStackAmount, currentStackAmount);
        fillImage.DOFillAmount(fillAmount, duration).SetEase(Ease.InQuad).SetId("mainPlayerFillBar");
    }
}
