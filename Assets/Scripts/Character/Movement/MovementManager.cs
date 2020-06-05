using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(TurnManager))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class MovementManager : MonoBehaviour
{
    private MovementDetector movementDetector;
    private TurnManager turnManager;
    private CharacterController characterController;
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GroundComponent AttachedGround = null;
    public GroundComponent attachedGround { get => AttachedGround; }


    bool moving = false;

    private void Start()
    {
        if (attachedGround == null) Debug.LogError("Please, set the AttachedGround in the inspector. " + transform.name);
        movementDetector = GetComponentInChildren<MovementDetector>();
        turnManager = GetComponent<TurnManager>();
        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.angularSpeed = 0;
    }

    private void Update()
    {
        if (!moving) return;
        SendMessage("OnMoveEvent", SendMessageOptions.DontRequireReceiver);
        if (XZDistance(transform.position, navMeshAgent.destination) < 0.1f)
        {
            movementDetector.gameObject.SendMessage("HighlightGroundAhead", SendMessageOptions.DontRequireReceiver);
            SendMessage("EndAction");
            SendMessage("OnIdleEvent", SendMessageOptions.DontRequireReceiver);
            moving = false;
        }
    }

    public void Move()
    {
        if (IsBusy()) return;
        GroundComponent groundAhead = movementDetector.GetGroundAhead();
        if (groundAhead == null) return;
        if (groundAhead.IsOccupied()) return;
        navMeshAgent.SetDestination(groundAhead.transform.position);
        moving = true;
        AttachGround(groundAhead);
        SendMessage("StartAction");
    }

    public void AttachGround(GroundComponent groundComponent)
    {
        if (attachedGround)
            attachedGround.gameObject.SendMessage("DettachCharacter");
        groundComponent.SendMessage("AttachCharacter", turnManager);
        AttachedGround = groundComponent;
    }

    public void Rotate(float[] param)
    {
        if (IsBusy()) return;
        float[] treatedData = Equalize(param[0], param[1]);
        Vector3 cameraRot = GameManager.cameraController.transform.eulerAngles;
        Vector3 direction = new Vector3(treatedData[0], 0, treatedData[1]);
        if (turnManager.playerControlled)
            direction = Quaternion.Euler(cameraRot) * direction;
        if (direction.magnitude == 0) return;
        transform.rotation = Quaternion.LookRotation(direction);
        movementDetector.gameObject.SendMessage("HighlightGroundAhead", SendMessageOptions.DontRequireReceiver);
    }

    public bool IsBusy()
    {
       return !turnManager.IsTurn() || turnManager.executingAction || moving || turnManager.actionsRemaining <= 0 || (GameManager.cameraController.rotating && turnManager.playerControlled);
    }

    #region Util
    public static float[] Equalize(float h, float v)
    {
        float[] axis = { h, v };
        _ = Mathf.Abs(h) > Mathf.Abs(v) ? (axis[1] = 0) : (axis[0] = 0);
        return axis;
    }

    /// <summary>
    /// Returns the distance of two vectors based only on X and Z axis.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float XZDistance(Vector3 a, Vector3 b)
    {
        a.y = 0;
        b.y = 0;
        return Vector3.Distance(a, b);
    }
    #endregion
}