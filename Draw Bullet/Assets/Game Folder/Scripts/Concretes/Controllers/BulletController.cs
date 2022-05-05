using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game_Folder.Scripts.Concretes.Controllers
{
    public class BulletController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Queue<Vector3> pathPoints = new Queue<Vector3>();
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            FindObjectOfType<DrawingController>().OnNewPathCreated += SetDestination;
        }

        private void SetDestination(IEnumerable<Vector3> points)
        {
            pathPoints = new Queue<Vector3>(points);
        }

        private void Update()
        {
            UpdateDestination();
        }

        private void UpdateDestination()
        {
            if (ShouldSetDestination())
                _navMeshAgent.SetDestination(pathPoints.Dequeue());
        }

        private bool ShouldSetDestination()
        {
            if (pathPoints.Count == 0)
            {
                return false;
            }

            if (_navMeshAgent.hasPath == false || _navMeshAgent.remainingDistance < 0.5f)
            {
                return true;
            }

            return false;
        }
    }
}
