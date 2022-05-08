using System;
using System.Collections;
using System.Collections.Generic;
using Game_Folder.Scripts.Concretes.Managers;
using Game_Folder.Scripts.Concretes.UI;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GunType _gunType => GameManager.Instance.gunType;

    [SerializeField] private Bullet revolverBullet;
    [SerializeField] private Bullet sniperBullet;
    private bool _isWorking;

    public Queue<Bullet> bulletList = new Queue<Bullet>(); 

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
        Player.Instance.Gun.CurrentBulletCount = 0;
        bulletList.Clear();
        Player.Instance.Gun.StopFiring();
        Player.Instance.Gun.MaxBulletCount = (int) _gunType;
    }

    private void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    IEnumerator SpawnBullets()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (_isWorking && UIManager.Instance.TapToPlayButton.IsGameStarted)
            {
                switch (_gunType)
                {
                    case GunType.Revolver:
                        if (Player.Instance.Gun.CurrentBulletCount < (int)_gunType)
                        {
                            var go = Instantiate(revolverBullet, transform.position, Quaternion.identity,
                                transform.parent);
                            go.gameObject.SetActive(false);
                            bulletList.Enqueue(go);
                            Player.Instance.Gun.CurrentBulletCount++;
                        }
                        else
                        {
                            _isWorking = false;
                        }

                        break;
                    case GunType.Sniper:
                        if (Player.Instance.Gun.CurrentBulletCount < (int)_gunType)
                        {
                            var go2 = Instantiate(sniperBullet, transform.position, Quaternion.identity,
                                transform.parent);
                            go2.gameObject.SetActive(false);
                            bulletList.Enqueue(go2);
                            Player.Instance.Gun.CurrentBulletCount++;
                        }
                        else
                        {
                            _isWorking = false;
                        }
                        
                        break;
                }
            }
            else if (Player.Instance.Gun.CurrentBulletCount < Player.Instance.Gun.MaxBulletCount)
            {
                _isWorking = true;
            }
            yield return new WaitForSeconds(.4f);
        }
    }
}