
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletFillBar : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private GameObject wholeImage;
    [SerializeField] private Image fillImage;
    
     public TMP_Text currentBulletText;
     public TMP_Text maxBulletText;
    

    public void FillBulletImage(float duration)
    {
        DOTween.Complete("mainPlayerFillBar");
        var currentStackAmount = Player.Instance.GunController.CurrentBulletCount;
        var maxStackAmount = Player.Instance.GunController.MaxBulletCount;
        var fillAmount = Mathf.InverseLerp(0, maxStackAmount, currentStackAmount);
        fillImage.DOFillAmount(fillAmount, duration).SetEase(Ease.InQuad).SetId("mainPlayerFillBar").OnComplete(() =>
        {
            SetCurrentText(Player.Instance.GunController.CurrentBulletCount);
        });
    }

    public void SetCurrentText(float current)
    {
        currentBulletText.SetText(current.ToString());
    }

    public void SetMaxText(float max)
    {
        maxBulletText.SetText(max.ToString());
    }
}
