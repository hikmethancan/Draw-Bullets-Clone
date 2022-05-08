
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game_Folder.Scripts.Concretes.Controllers;
using Game_Folder.Scripts.Concretes.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public GunType gunType;
    
    private const float Speed = 5f;
    private DrawingController _drawingController => Player.Instance.DrawingController;
    private List<Vector3> pointList = new List<Vector3>();
    private float _collidedCount;
    private bool _hasFired;

    public void SetNewPos(List<Vector3> obj)
    {
        pointList = obj;
        if (pointList.Count != 0f && _drawingController.isBulletSpawning && !_hasFired)
        {
            gameObject.SetActive(true);
            StartCoroutine(BulletNewPos());
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out EnemyController enemyController))
        {
            if (gunType == GunType.Revolver)
            {
                other.gameObject.SetActive(false);
                gameObject.SetActive(false);    
            }
            else if (gunType == GunType.Sniper)
            {
                _collidedCount++;
                other.gameObject.SetActive(false);
                if(_collidedCount <= 2f) return;
                
                gameObject.SetActive(false);
            }
        }
    }


    // private void Start()
    // {
    //     StartCoroutine(Timer());
    // }

    IEnumerator BulletNewPos()
    {
        
        // var points = _drawingController.bulletPoints;
        // pointList = points;
        Debug.Log("isbUllet : "+_drawingController.isBulletSpawning);
        
       
        _hasFired = true;
        for (int i = 0; i < pointList.Count - 1; i++)
        {
            Debug.Log("For İÇerisindeyiz");
            yield return new WaitForSeconds(_drawingController.bulletSpeed);
            transform.localPosition = pointList[i];
            // transform.localPosition = Vector3.Lerp(points[i], points[i + 1], .5f);
        }
        StartCoroutine(Timer());
        var direction = (pointList.Last() - pointList[pointList.Count - 2]).normalized;
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(_drawingController.bulletSpeed);
            transform.localPosition += direction;
        }
    }
    IEnumerator Timer()
    {
        Debug.Log("Süre başladı");
        yield return new WaitForSeconds(3f);
        Debug.Log("Süre bitti");
        gameObject.SetActive(false);
        _hasFired = false;
    }


}
