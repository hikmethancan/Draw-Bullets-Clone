using System;
using System.Collections;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.UI
{
    
    public class GunsUI : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(BulletLoader(GameManager.Instance.gunType));
        }

        private void OnEnable()
        {
            GameManager.OnGunChanged += GunChanged;
        }


        private void OnDisable()
        {
            GameManager.OnGunChanged -= GunChanged;
        }

        private void GunChanged()
        {
            
            Player.Instance.GunController.MaxBulletCount = 0;
            
        }

        private IEnumerator BulletLoader(GunType gunType)
        {
            Debug.Log(Player.Instance.GunController.MaxBulletCount + "GameManager Bullet Count");
            Debug.Log((int) gunType + " GuType");
            gunType = GameManager.Instance.gunType;
            while (true)
            {
                if (Player.Instance.GunController.MaxBulletCount < (int) gunType)
                {
                    Player.Instance.GunController.MaxBulletCount++;
                    Debug.Log("Bullet Count = " + Player.Instance.GunController.MaxBulletCount);
                }
                yield return null;
            }
        }
    }
}