using System;
using Game_Folder.Scripts.Abstracts.Bullet;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class RevolverController : Bullet
    {
        private BulletMovement _bulletMovement;

        private void Awake()
        {
            _bulletMovement = GetComponent<BulletMovement>();
            _bulletMovement.BulletSpeed = speed;
            _bulletMovement.KillDuration = killDuration;
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out  EnemyController enemyController))
            {
                Explosion(transform.position);
                other.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
