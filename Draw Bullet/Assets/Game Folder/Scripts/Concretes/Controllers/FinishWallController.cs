using System;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class FinishWallController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            
            if(player == null) return;
            Debug.Log("WİNNNNNN");
            GameManager.Instance.isGameStarted = false;
        }
    }
}
