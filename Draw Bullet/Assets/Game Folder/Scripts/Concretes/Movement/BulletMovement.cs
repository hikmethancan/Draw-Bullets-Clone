
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game_Folder.Scripts.Concretes.Controllers;
using Game_Folder.Scripts.Concretes.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletMovement : MonoBehaviour
{
    public GunType gunType;
    
    private const float Speed = 5f;
    private DrawingController _drawingController => Player.Instance.DrawingController;
    private List<Vector3> _pointList = new List<Vector3>();
    private bool _hasFired;
    
    public float BulletSpeed { get; set; }
    public float KillDuration { get; set; }

    public void SetNewPos(List<Vector3> obj)
    {
        _pointList = obj;
        if (_pointList.Count != 0f && _drawingController.isBulletSpawning && !_hasFired)
        {
            gameObject.SetActive(true);
            StartCoroutine(BulletNewPos());
        }

    }


    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out EnemyController enemyController))
    //     {
    //         if (gunType == GunType.Revolver)
    //         {
    //             other.gameObject.SetActive(false);
    //             gameObject.SetActive(false);    
    //         }
    //         else if (gunType == GunType.Sniper)
    //         {
    //             _collidedCount++;
    //             other.gameObject.SetActive(false);
    //             if(_collidedCount <= 2f) return;
    //             
    //             gameObject.SetActive(false);
    //         }
    //     }
    // }


    // private void Start()
    // {
    //     StartCoroutine(Timer());
    // }

    IEnumerator BulletNewPos()
    {
        _hasFired = true;
        for (int i = 0; i < _pointList.Count - 1; i++)
        {
            yield return new WaitForSeconds(BulletSpeed);
            transform.localPosition = _pointList[i];
        }
        StartCoroutine(Timer());
        var direction = (_pointList.Last() - _pointList[_pointList.Count - 2]).normalized;
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(BulletSpeed);
            transform.localPosition += direction;
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(KillDuration);
        gameObject.SetActive(false);
        _hasFired = false;
    }


}
