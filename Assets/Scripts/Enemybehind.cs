using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemybehind : MonoBehaviour
{
    private NavMeshAgent enemyNavMesh;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float Seguir = 10f;
    [SerializeField] private float RotacaoSpeed = 2f;
    private Vector3 originalPosition;
    private bool SeguindoPlayer = false;

    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= Seguir)
        {

            enemyNavMesh.SetDestination(playerTransform.position);
            SeguindoPlayer = true;
        }
        else
        {

            {
                enemyNavMesh.SetDestination(originalPosition);
                SeguindoPlayer = false;
            }


           
        }
    }

    


}