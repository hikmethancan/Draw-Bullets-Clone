using System;
using System.Collections;
using System.Collections.Generic;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class BulletSpawnController : MonoBehaviour
    {
        public GunType GunType => GameManager.Instance.gunType;
        
        [Header("Bullet Prefabs")]
        [SerializeField] private BulletMovement revolverBulletMovement;
        [SerializeField] private BulletMovement sniperBulletMovement;

        private RevolverController _revolverController;
        private SniperController _sniperController;
        private bool _isWorking;
        private float _spawnRate;
        

        public Queue<BulletMovement> bulletList = new Queue<BulletMovement>();


        private void Awake()
        {
            _revolverController = revolverBulletMovement.GetComponent<RevolverController>();
            _sniperController = sniperBulletMovement.GetComponent<SniperController>();
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
            Player.Instance.GunController.GunChanged();
            bulletList.Clear();
            Player.Instance.GunController.MaxBulletCount = (int) GunType;
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
                    switch (GunType)
                    {
                        case GunType.Revolver:
                            if (Player.Instance.GunController.CurrentBulletCount < (int)GunType)
                            {
                                var go = Instantiate(revolverBulletMovement, transform.position, Quaternion.identity,
                                    transform.parent);
                                go.gameObject.SetActive(false);
                                bulletList.Enqueue(go);
                                _spawnRate = _revolverController.spawnRate;
                                Player.Instance.GunController.CurrentBulletCount++;
                                UIManager.Instance.BulletFillBar.FillBulletImage(_spawnRate);
                            }
                            else
                            {
                                _isWorking = false;
                            }

                            break;
                        case GunType.Sniper:
                            if (Player.Instance.GunController.CurrentBulletCount < (int)GunType)
                            {
                                var go2 = Instantiate(sniperBulletMovement, transform.position, Quaternion.identity,
                                    transform.parent);
                                go2.gameObject.SetActive(false);
                                bulletList.Enqueue(go2);
                                _spawnRate = _sniperController.spawnRate;
                                Player.Instance.GunController.CurrentBulletCount++;
                                UIManager.Instance.BulletFillBar.FillBulletImage(_spawnRate);
                            }
                            else
                            {
                                _isWorking = false;
                            }
                        
                            break;
                    }
                }
                else if (Player.Instance.GunController.CurrentBulletCount < Player.Instance.GunController.MaxBulletCount)
                {
                    _isWorking = true;
                }
                yield return new WaitForSeconds(_spawnRate);
            }
        }
        
        // switch (_gunType)
        // {
        //     case GunType.Revolver:
        //     if (Player.Instance.GunController.CurrentBulletCount < (int)_gunType)
        //     {
        //         var go = Instantiate(revolverBulletMovement, transform.position, Quaternion.identity,
        //             transform.parent);
        //         go.gameObject.SetActive(false);
        //         bulletList.Enqueue(go);
        //         Player.Instance.GunController.CurrentBulletCount++;
        //     }
        //     else
        //     {
        //         _isWorking = false;
        //     }
        //
        //     break;
        //     case GunType.Sniper:
        //     if (Player.Instance.GunController.CurrentBulletCount < (int)_gunType)
        //     {
        //         var go2 = Instantiate(sniperBulletMovement, transform.position, Quaternion.identity,
        //             transform.parent);
        //         go2.gameObject.SetActive(false);
        //         bulletList.Enqueue(go2);
        //         Player.Instance.GunController.CurrentBulletCount++;
        //     }
        //     else
        //     {
        //         _isWorking = false;
        //     }
        //                 
        //     break;
        // }
    }
}