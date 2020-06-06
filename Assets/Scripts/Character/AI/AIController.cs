using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TurnManager))]
[RequireComponent(typeof(ShooterCharacter))]
public class AIController : MonoBehaviour
{
    private NavMeshPath pathForEnemy;

    TurnManager turnManager;
    ShooterCharacter shooterCharacter;
    NavMeshAgent navMeshAgent;
    MovementDetector movementDetector;
    private void Awake()
    {
        turnManager = GetComponent<TurnManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        shooterCharacter = GetComponent<ShooterCharacter>();
        movementDetector = GetComponentInChildren<MovementDetector>();
    }

    public void AIPlay()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        TurnManager closestEnemy = FindClosestEnemy();
        if (closestEnemy == null)
        {
            GameManager.active.GameOver();
            yield break;
        }
        pathForEnemy = new NavMeshPath();
        navMeshAgent.CalculatePath(closestEnemy.transform.position, pathForEnemy);

        if (pathForEnemy.corners.Length < 2)
        {
            turnManager.PassAction();
            yield break;
        }

        Vector3 direction = pathForEnemy.corners[0] - pathForEnemy.corners[1];
        yield return new WaitForSeconds(0.2f);
        SendMessage("Rotate", new float[] { -direction.z, direction.x });
        yield return new WaitForSeconds(0.2f);
        Vector3 origin = movementDetector.GetMagicBallOrigin();
        Vector3 destination = movementDetector.GetMagicBallDestination();
        if (CanAttackPlayer(origin, destination))
            SendMessage("Attack");
        else
            SendMessage("Move");
    }

    public bool CanAttackPlayer(Vector3 origin, Vector3 destination)
    {
        RaycastHit[] hit = Physics.RaycastAll(origin, destination - origin, Vector3.Distance(origin, destination));
        foreach (RaycastHit h in hit)
        {
            ShooterCharacter enemyShooter = h.transform.GetComponent<ShooterCharacter>();
            if (enemyShooter.team == this.shooterCharacter.team) continue;
            if (h.transform.GetComponent<AliveCreature>() != null) return true;
            if (!enemyShooter.passThrough) break;
        }
        return false;
    }

    public TurnManager FindClosestEnemy()
    {
        TurnManager toReturn = null;
        float distanceRecord = 0;
        foreach (TurnManager turnManager in GameManager.active.GetCharacters())
        {
            ShooterCharacter shooterCharacter = turnManager.GetComponent<ShooterCharacter>();
            if (this.shooterCharacter.team == shooterCharacter.team) continue;
            float distance = Vector3.Distance(transform.position, turnManager.transform.position);
            if (distanceRecord > distance || toReturn == null)
            {
                distanceRecord = distance;
                toReturn = turnManager;
            }
        }
        return toReturn;
    }
    

}
