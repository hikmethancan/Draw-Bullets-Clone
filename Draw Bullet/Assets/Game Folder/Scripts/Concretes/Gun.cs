using System;
using System.Collections;
using System.Collections.Generic;
using Game_Folder.Scripts.Concretes.Controllers;
using UnityEngine;
using UnityEngine.Serialization;

public enum GunType
{
    Revolver = 5,
    Sniper = 1,
    ShoutGun = 3
}

public class Gun : MonoBehaviour
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
    }

    public void StopFiring()
    {
        if(FiredCoroutine == null) return;
        StopCoroutine(FiredCoroutine);
        Player.Instance.DrawingController.IsFiring = false;
    }


    public IEnumerator HasFiredCourotine(List<Vector3> obj)
    {
        if (Player.Instance.BulletSpawn.bulletList.Count != 0 && CurrentBulletCount != 0f)
        {
            Player.Instance.DrawingController.IsFiring = true;
            int count = CurrentBulletCount;
            for (int i = 0; i < count; i++)
            {
                CurrentBulletCount--;
                var newBullet = Player.Instance.BulletSpawn.bulletList.Dequeue();
                newBullet.SetNewPos(obj);
                yield return new WaitForSeconds(FireRate);    
            }

            Player.Instance.DrawingController.IsFiring = false;
        }
        
    }
}