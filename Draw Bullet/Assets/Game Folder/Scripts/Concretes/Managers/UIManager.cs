using System;
using System.Collections;
using System.Collections.Generic;
using Game_Folder.Scripts.Abstracts.Utilities;
using Game_Folder.Scripts.Concretes.UI;
using UnityEngine;

public class UIManager : SingletonObjects<UIManager>
{
    public TapToPlayButton TapToPlayButton;
    private void Awake()
    {
        SingletonObject(this);
        TapToPlayButton = GetComponentInChildren<TapToPlayButton>();
    }
}
