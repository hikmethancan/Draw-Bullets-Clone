
using System;
using System.Collections.Generic;
using System.Linq;
using Game_Folder.Scripts.Concretes.Controllers;
using UnityEngine;
using PathCreation;

public class GeneratePath : MonoBehaviour
{
    public bool closedLoop = true;
    public List<Transform> waypoints = new List<Transform>();
    public List<Vector3> pos = new List<Vector3>();

    private DrawingController _drawingController;
    private void Awake()
    {
        _drawingController = FindObjectOfType<DrawingController>();
    }

    private void OnEnable()
    {
        _drawingController.OnNewPathCreated += GenerateNewPath;
    }

    private void OnDisable()
    {
        _drawingController.OnNewPathCreated -= GenerateNewPath;
    }

    private void GenerateNewPath(IEnumerable<Vector3> point)
    {
        foreach (var newPoint in point)
        {
            pos.Add(newPoint);
        }

        foreach (var VARIABLE in waypoints)
        {
            
        }
        if (waypoints.Count > 0) {
            // Create a new bezier path from the waypoints.
            BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
            GetComponent<PathCreator> ().bezierPath = bezierPath;
        }   
    }
    
}
