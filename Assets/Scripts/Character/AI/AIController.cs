using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TurnManager))]
public class AIController : MonoBehaviour
{
    [System.NonSerialized]
    public TurnManager characterComponent;
    [System.NonSerialized]
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        characterComponent = GetComponent<TurnManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
