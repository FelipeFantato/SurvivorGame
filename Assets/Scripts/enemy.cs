using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    [SerializeField] Transform Seguir;
    NavMeshAgent navMeshInimigo;

    private int vida = Player.vida;

    void Awake()
    {
        navMeshInimigo = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshInimigo.SetDestination(Seguir.position);
    }
  
}
