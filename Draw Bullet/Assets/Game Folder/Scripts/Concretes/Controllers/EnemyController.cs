
using System;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (!GameManager.Instance.isGameStarted || GameManager.Instance.isGameFinished) return;
        _navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
       
        if(player == null) return;

        GameManager.Instance.GameOver();
    }
}
