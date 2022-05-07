
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
    private DrawingController _drawingController;
    private List<Vector3> pointList = new List<Vector3>();
    private float _collidedCount;
    private void Awake()
    {
        _drawingController = FindObjectOfType<DrawingController>();
        _drawingController.OnNewPosCreated += SetNewPos;
    }

    private void SetNewPos(List<Vector3> obj)
    {
        var points = _drawingController.bulletPoints;
        pointList = points;
        if(points == null && !_drawingController.isBulletSpawning) return;
        // while ( transform.localPosition != points.Last())
        // {
        //     transform.localPosition = Vector3.Lerp(points.First(), points.Last(), .5f);
        // }
        for (int i = 0; i < points.Count - 1; i++)
        {
            
            // transform.localPosition = Vector3.Lerp(points[i], points[i + 1], .5f);
            transform.DOLocalMove(points[i], .5f);

        }
       

        // foreach (var point in points)
        // {
        //     transform.DOLocalMove(point, .5f);
        // }
        
    }

    // private void Update()
    // {
    //     if (pointList.Count == 0)
    //         return;
    //         
    //     var currentIndex = 0;
    //     if (Vector3.Distance(pointList[currentIndex],transform.localPosition) < .1f)
    //     {
    //         currentIndex++;
    //         if (currentIndex >= pointList.Count - 1)
    //         {
    //             currentIndex = 0;
    //         }
    //     }
    //     transform.localPosition =
    //         Vector3.MoveTowards(transform.localPosition, pointList[currentIndex], Time.deltaTime * Speed);
    // }


    private void OnTriggerEnter(Collider other)
    {
        var enemyController = other.GetComponent<EnemyController>();

        if (gunType == GunType.Revolver)
        {
            if (enemyController == null) return;
            Debug.Log("Bullet Collide Enemy");
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);    
        }
        else if (gunType == GunType.Sniper)
        {
            if(_collidedCount == 3f) return;
            
            if (enemyController == null) return;
            _collidedCount++;
            other.gameObject.SetActive(false);
        }
    }


    // private void Start()
    // {
    //     StartCoroutine(Timer());
    // }
    

    IEnumerator Timer()
    {
        Debug.Log("Süre başladı");
        yield return new WaitForSeconds(3f);
        Debug.Log("Süre bitti");
        gameObject.SetActive(false);
    }


}
