using System;
using DG.Tweening;
using Game_Folder.Scripts.Abstracts.Utilities;
using Game_Folder.Scripts.Concretes.Managers;
using Game_Folder.Scripts.Concretes.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : SingletonObjects<UIManager>
{
    public TapToPlayButton TapToPlayButton { get; private set; }
    public BulletFillBar BulletFillBar { get; private set; }

    public TMP_Text gunTypeText;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    private void Awake()
    {
        SingletonObject(this);
        BulletFillBar = GetComponent<BulletFillBar>();
        TapToPlayButton = GetComponentInChildren<TapToPlayButton>();
    }

    private void Start()
    {
        SetGunTypeText();
    }

    public void SetGunTypeText()
    {
        gunTypeText.SetText("Gun Type : "+GameManager.Instance.gunType.ToString());
    }
    
    
    public void GunChangedTextWarning()
    {
        DOTween.Complete("scaleGoldImage");
        gunTypeText.rectTransform.DOPunchScale(Vector3.one * .4f, .8f, 3).SetEase(Ease.Linear).SetId("scaleGoldImage");
    }
}