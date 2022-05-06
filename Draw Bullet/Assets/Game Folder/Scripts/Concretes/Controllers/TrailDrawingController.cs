using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailDrawingController : MonoBehaviour
{
    public bool isBulletSpawning;
    private Touch _touch;
    private TrailRenderer _trailRenderer;

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
    public List<Vector3> points = new List<Vector3>();
    public LayerMask layerMask;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {


        if (Input.touchCount <= 0) return;
        _touch = Input.GetTouch(0);


        if (_touch.phase == TouchPhase.Began)
        {
            points.Clear();
            isBulletSpawning = false;
            DrawRayPoint();
            
        }

        if (_touch.phase == TouchPhase.Moved)
        {
            DrawRayPoint();
        }

        else if (_touch.phase == TouchPhase.Ended)
        {
            OnNewPathCreated(points);
            isBulletSpawning = true;
            // var go = Instantiate(bulletPrefab, points.First(), Quaternion.identity);
            // StartCoroutine(SpawnBullets());
            _trailRenderer.enabled = false;
            _trailRenderer.emitting = false;
            _trailRenderer.Clear();
        }
    }

    float DistanceToLastPoint(Vector3 point)
        {
            if (!points.Any())
            {
                return Mathf.Infinity;
            }

            return Vector3.Distance(points.Last(), point);
        }

        private void DrawRayPoint()
        {
            var ray = _camera.ScreenPointToRay(_touch.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
            {
                transform.position = hitInfo.point;
                _trailRenderer.enabled = true;
                _trailRenderer.emitting = true;
            }
        }
    }
