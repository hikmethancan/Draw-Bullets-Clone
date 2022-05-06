
using System;
using System.Collections;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.UI
{
    public enum GunType
    {
        Revolver = 5,
        Sniper = 1,
        ShoutGun = 3
    }
    public class GunsUI : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(BulletLoader(GunType.Revolver));
        }

        private IEnumerator BulletLoader(GunType gunType)
        {
            Debug.Log(GameManager.Instance.bulletCount + "GameManager Bullet Count");
            Debug.Log((int)gunType + " GuType");
            while (true)
            {
                if (GameManager.Instance.bulletCount  < (int) gunType)
                {
                    GameManager.Instance.bulletCount++;
                    Debug.Log("Bullet Count = "+GameManager.Instance.bulletCount);     
                }
                yield return null;
            }
        } 
    }
}
