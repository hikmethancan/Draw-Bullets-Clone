using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class DrawingController : MonoBehaviour
    {
        [Header("Bullet Prefab")] [SerializeField]
        private Transform bulletPrefab;

        public event Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
        public static event Action<List<Vector3>> OnNewPosCreated = delegate { };
        public bool isBulletSpawning;
        public bool IsFiring;
        public float bulletSpeed;

        private LineRenderer _lineRenderer;
        [FormerlySerializedAs("_points")] public List<Vector3> points = new List<Vector3>();
        public List<Vector3> bulletPoints = new List<Vector3>();
        private Touch _touch;
        private Camera _camera;
        [FormerlySerializedAs("_layerMask")] public LayerMask layerMask;
        
         const float RESOLATION = .5f;
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!GameManager.Instance.isGameStarted)
                return;
            InputControl();
        }

        private void InputControl()
        {
            if (Input.touchCount <= 0) return;
            _touch = Input.GetTouch(0);
            if(IsFiring) return;
            
            if (_touch.phase == TouchPhase.Began)
            {
                points.Clear();
                bulletPoints.Clear();
                isBulletSpawning = false;
            }

            if (_touch.phase == TouchPhase.Moved)
            {
                var ray = _camera.ScreenPointToRay(_touch.position);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
                {
                    var newPoint = hitInfo.point - Player.Instance.transform.position ;
                    var bulletPos = newPoint;
                    bulletPos.y = .5f;
                    
                    // newPoint.z = 0.01f;
                    newPoint.y = -0.01f;
                    (newPoint.y, newPoint.z) = (newPoint.z, newPoint.y);

                    if (DistanceToLastPoint(newPoint) > RESOLATION)
                    {
                        points.Add(newPoint);
                        bulletPoints.Add(bulletPos);
                        _lineRenderer.positionCount = points.Count;
                        _lineRenderer.SetPositions(points.ToArray());
                    }
                    
                }
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                if (!UIManager.Instance.TapToPlayButton.IsGameStarted)
                    return;
                isBulletSpawning = true;
                OnNewPathCreated(points);
                OnNewPosCreated(bulletPoints);
                Player.Instance.GunController.CurrentBulletCount = 0;
                UIManager.Instance.BulletFillBar.FillBulletImage(.1f);
            }
        }

        private float DistanceToLastPoint(Vector3 point)
        {
            if (!points.Any())
            {
                return Mathf.Infinity;
            }

            return Vector3.Distance(points.Last(), point);
        }

    }
}