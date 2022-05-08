using System;
using Game_Folder.Scripts.Abstracts.Utilities;
using Game_Folder.Scripts.Concretes.UI;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.Managers
{
    public class GameManager : SingletonObjects<GameManager>
    {
        public int bulletCount = 0;
        public bool isGameStarted;
        public GunType gunType;

        public static event Action OnGunChanged;

        
        private void Awake()
        {
            SingletonObject(this);
            gunType = GunType.Revolver;
            Debug.Log(bulletCount + "bullet count");
        }

        public void GunChanged()
        {
            OnGunChanged?.Invoke();
        }
    }
}