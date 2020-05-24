using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterComponent))]
public class AIController : MonoBehaviour
{
    [System.NonSerialized]
    public CharacterComponent characterComponent;
    [System.NonSerialized]
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        characterComponent = GetComponent<CharacterComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Play()
    {
        CharacterComponent characterComponent = FindClosestEnemy();
    }


    public CharacterComponent FindClosestEnemy()
    {
        List<CharacterComponent> characterComponents = GameManager.active.GetCharacters();
        CharacterComponent closest = null;
        float record = -1;
        foreach (CharacterComponent c in characterComponents)
        {
            if (c.team == characterComponent.team) continue;
            float distance = Vector3.Distance(c.transform.position, transform.position);
            if (distance < record || closest == null)
            {
                record = distance;
                closest = c;
            }
        }
        return closest;
    }
}
