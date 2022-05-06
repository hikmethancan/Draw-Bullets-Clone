using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game_Folder.Scripts.Concretes.Managers;
using Game_Folder.Scripts.Concretes.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class DrawingController : MonoBehaviour
    {
        [Header("Bullet Prefab")] [SerializeField]
        private Transform bulletPrefab;

        public event Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
        public bool isBulletSpawning;

        private LineRenderer _lineRenderer;
        [FormerlySerializedAs("_points")] public List<Vector3> points = new List<Vector3>();
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
            if (Input.touchCount <= 0) return;
            _touch = Input.GetTouch(0);


            if (_touch.phase == TouchPhase.Began)
            {
                points.Clear();
                isBulletSpawning = false;
            }

            if (_touch.phase == TouchPhase.Moved)
            {
                var ray = _camera.ScreenPointToRay(_touch.position);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
                {
                    Vector3 newPoint = hitInfo.point;
                    
                    var playerNewPos = new Vector3(Player.Instance.transform.position.x,
                        Player.Instance.transform.position.z, Player.Instance.transform.position.y);
                    
                    newPoint = hitInfo.point - Player.Instance.transform.position ;
                    
                    // newPoint.z = 0.01f;
                    newPoint.y = 0.01f;
                    (newPoint.y, newPoint.z) = (newPoint.z, newPoint.y);

                    if (DistanceToLastPoint(newPoint) > RESOLATION)
                    {
                        points.Add(newPoint);
                        _lineRenderer.positionCount = points.Count;
                        _lineRenderer.SetPositions(points.ToArray());
                    }
                    
                }
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                OnNewPathCreated(points);
                isBulletSpawning = true;
                // var go = Instantiate(bulletPrefab, points.First(), Quaternion.identity);
                // StartCoroutine(SpawnBullets());
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

        private IEnumerator SpawnBullets()
        {
            for (int i = 0; i < GameManager.Instance.bulletCount; i++)
            {
                yield return new WaitForSeconds(.3f);
                var go = Instantiate(bulletPrefab, points.First(), Quaternion.identity);
            }
        }
    }
}