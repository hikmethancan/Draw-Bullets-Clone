using System.Collections;
using System.Collections.Generic;
using Game_Folder.Scripts.Concretes.Controllers;
using UnityEngine;

public enum GunType
{
    Revolver = 5,
    Sniper = 1,
}

public class GunController : MonoBehaviour
{
    public Coroutine FiredCoroutine;
    public float MaxBulletCount { get; set; }
    public int CurrentBulletCount { get; set; }

    public float FireRate;

    private void OnEnable()
    {
        DrawingController.OnNewPosCreated += HasFired;
    }

    private void OnDisable()
    {
        DrawingController.OnNewPosCreated -= HasFired;
    }

    private void HasFired(List<Vector3> obj)
    {
        FiredCoroutine = StartCoroutine(HasFiredCourotine(obj));
        UIManager.Instance.BulletFillBar.FillBulletImage(.4f);
    }

    public void StopFiring()
    {
        if(FiredCoroutine == null) return;
        StopCoroutine(FiredCoroutine);
        Player.Instance.DrawingController.IsFiring = false;
    }

    public void GunChanged()
    {
        CurrentBulletCount = 0;
        StopFiring();
    }


    public IEnumerator HasFiredCourotine(List<Vector3> obj)
    {
        if (Player.Instance.BulletSpawnController.bulletList.Count != 0 && CurrentBulletCount != 0f)
        {
            Player.Instance.DrawingController.IsFiring = true;
            int count = CurrentBulletCount;
            for (int i = 0; i < count; i++)
            {
                CurrentBulletCount--;
                var newBullet = Player.Instance.BulletSpawnController.bulletList.Dequeue();
                newBullet.SetNewPos(obj);
                yield return new WaitForSeconds(FireRate);    
            }

            Player.Instance.DrawingController.IsFiring = false;
        }
        
    }
}