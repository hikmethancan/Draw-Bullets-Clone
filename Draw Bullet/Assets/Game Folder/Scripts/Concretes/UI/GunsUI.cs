using System;
using System.Collections;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.UI
{
    
    public class GunsUI : MonoBehaviour
    {
        private GunType _gunType;
        private void Start()
        {
            StartCoroutine(BulletLoader());
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

        private IEnumerator BulletLoader()
        {
            _gunType = GameManager.Instance.gunType;
            while (true)
            {
                if (Player.Instance.GunController.MaxBulletCount < (int) _gunType)
                {
                    Player.Instance.GunController.MaxBulletCount++;
                }
                yield return null;
            }
        }
    }
}