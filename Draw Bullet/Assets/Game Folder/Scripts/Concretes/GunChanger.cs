
using Game_Folder.Scripts.Concretes.Managers;
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
      Player.Instance.GunController.MaxBulletCount = (int) gunType;
      Debug.Log("MaxBullet Değişti : "+Player.Instance.GunController.MaxBulletCount);
      UIManager.Instance.BulletFillBar.FillBulletImage(.1f);
   }
}
