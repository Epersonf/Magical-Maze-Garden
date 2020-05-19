using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterComponent))]
public class AIController : MonoBehaviour
{
    public CharacterComponent characterComponent;
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        characterComponent = GetComponent<CharacterComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public CharacterComponent FindClosestEnemy()
    {
        return null;
    }
}
