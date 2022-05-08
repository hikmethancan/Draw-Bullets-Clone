
using Game_Folder.Scripts.Concretes.Managers;
using Game_Folder.Scripts.Concretes.UI;
using UnityEngine;

public class GunChanger : MonoBehaviour
{
   public GunType gunType;

   private void OnTriggerEnter(Collider other)
   {
      var player = other.GetComponent<Player>();
        
      if(player == null) return;
      GameManager.Instance.gunType = gunType;
      GameManager.Instance.GunChanged();
      Debug.Log("Yeni guntype = "+GameManager.Instance.gunType);
   }
}
