using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    private NavMeshAgent enemyNavMesh;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float followDistance = 10f; 
    
    private bool seguindoJogador = false;

    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
       
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= followDistance)
        {
            enemyNavMesh.SetDestination(playerTransform.position);
            seguindoJogador = true;
        }
        else
        {
            if (seguindoJogador)
            {
                seguindoJogador = false;
            }
        }
    }
}
