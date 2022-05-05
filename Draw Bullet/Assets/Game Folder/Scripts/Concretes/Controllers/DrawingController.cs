
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class DrawingController : MonoBehaviour
    {
        [Header("Bullet Prefab")]
        [SerializeField] private Transform bulletPrefab; 
        
        public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate {  };

        private LineRenderer _lineRenderer;
        private List<Vector3> _points = new List<Vector3>();
        private Touch _touch;
        private Camera _camera;
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;
            _touch = Input.GetTouch(0);
                
                
            if (_touch.phase == TouchPhase.Began )
            {
                _points.Clear();
            }

            if (_touch.phase == TouchPhase.Moved )
            {
                var ray = Camera.main.ScreenPointToRay(_touch.position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray,out hitInfo))
                {
                    if (DistanceToLastPoint(hitInfo.point) > 1f)
                    {
                        _points.Add(hitInfo.point);
                        _lineRenderer.positionCount = _points.Count;
                        _lineRenderer.SetPositions(_points.ToArray());
                    }
                }
            }
            else if (_touch.phase == TouchPhase.Ended )
            {
                OnNewPathCreated(_points);
            }
        }

        private float DistanceToLastPoint(Vector3 point)
        {
            if (!_points.Any())
            {
                return Mathf.Infinity;
            }
            return Vector3.Distance(_points.Last(), point);
        }
    }
}
