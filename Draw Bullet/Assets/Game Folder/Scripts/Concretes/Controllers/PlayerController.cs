using UnityEngine;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
    }
}
