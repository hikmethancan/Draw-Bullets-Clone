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
            
            Player.Instance.Gun.MaxBulletCount = 0;
            
        }

        private IEnumerator BulletLoader(GunType gunType)
        {
            Debug.Log(Player.Instance.Gun.MaxBulletCount + "GameManager Bullet Count");
            Debug.Log((int) gunType + " GuType");
            gunType = GameManager.Instance.gunType;
            while (true)
            {
                if (Player.Instance.Gun.MaxBulletCount < (int) gunType)
                {
                    Player.Instance.Gun.MaxBulletCount++;
                    Debug.Log("Bullet Count = " + Player.Instance.Gun.MaxBulletCount);
                }
                yield return null;
            }
        }
    }
}