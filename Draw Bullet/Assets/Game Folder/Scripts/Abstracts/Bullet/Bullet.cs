using UnityEngine;

namespace Game_Folder.Scripts.Abstracts.Bullet
{
    public abstract class Bullet : MonoBehaviour
    {
        public float maxBulletCount;
        public float speed;
        public float killDuration;
        public int fireRate;
        public float spawnRate;
        public GameObject explosionParticle;

        public virtual void Explosion(Vector3 pos)
        {
            Instantiate(explosionParticle,pos,Quaternion.identity);
        }
    }
}