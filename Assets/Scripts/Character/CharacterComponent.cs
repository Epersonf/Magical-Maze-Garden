using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class CharacterComponent : AliveCreature
{
    const float margin = 0.4f;

    public string characterName = "Emilia";
    public Sprite characterSprite;
    public bool playerControlled = true;
    public int actionsPerTurn = 3;
    public bool canJump = false;

    #region movementDetector
    MovementDetector movementDetector;
    public void UnhighlightMovementDetector()
    {
        movementDetector.Unhighlight();
    }
    #endregion

    Rigidbody rg;
    Collider clldr;

    protected override void Awake()
    {
        base.Awake();
        movementDetector = GetComponentInChildren<MovementDetector>();
        rg = GetComponent<Rigidbody>();
        clldr = GetComponent<Collider>();
    }

    protected override void Start()
    {
        base.Start();
        GameManager.active.AddCharacter(this);
    }

    protected override void Update()
    {
        base.Update();
        MoveToDestination();
    }

    #region Execute Action System
    bool executingAction = false;
    bool moving = false;
    bool attack = false;
    #region actionsRemaining
    int ActionsRemaining = 0;
    public void ResetActions()
    {
        ActionsRemaining = actionsPerTurn;
    }
    public int actionsRemaining
    {
        get => ActionsRemaining;
    }
    #endregion

    public bool StartAction()
    {
        if (actionsRemaining <= 0) return false;
        ActionsRemaining--;
        executingAction = true;
        return true;
    }

    public void EndAction()
    {
        if (!executingAction) return;
        executingAction = false;
        moving = false;
        attack = false;
        animator.SetBool("moving", false);
        if (actionsRemaining > 0)
            GameManager.interfaceController.updatableInterface.UpdateTurn();
        else
            GameManager.active.turn++;
    }

    public void MoveToDestination()
    {
        if (attachedGround == null && moving) return;
        float distance = Vector3.Distance(transform.position, attachedGround.transform.position);
        if (distance > margin)
        {
            Vector3 direction = attachedGround.characterPosition.position - transform.position;
            animator.SetBool("moving", true);
            if (rg.velocity.magnitude < 2f)
                rg.AddForce(direction.normalized * 50 * Time.deltaTime, ForceMode.Impulse);
        }
        else if (!attack) EndAction();
    }
    #endregion

    #region Assign first ground
    private void OnCollisionEnter(Collision collision)
    {
        GroundComponent groundComponent = collision.transform.GetComponent<GroundComponent>();
        if (groundComponent != null)
        {
            attachedGround = groundComponent;
        }
    }
    #endregion
    
    #region Occupied
    private GroundComponent AttachedGround;
    public GroundComponent attachedGround
    {
        get => AttachedGround;
        set
        {
            if (value == null) return;
            if (value.IsOccupied()) return;
            executingAction = true;
            if (AttachedGround != null)
                AttachedGround.attachedCharacter = null;
            AttachedGround = value;
            AttachedGround.attachedCharacter = this;
        }
    }

    public bool IsAttached()
    {
        return attachedGround == null;
    }
    #endregion

    #region Inputs
    public void Move()
    {
        if (!CanPlay()) return;
        GroundComponent groundAhead = movementDetector.GetGroundAhead();
        if (groundAhead == null) return;
        if (groundAhead.IsOccupied()) return;
        if (!StartAction()) return;
        moving = true;
        attachedGround = groundAhead;
    }

    public void Rotate(float hor, float ver)
    {
        if (!CanPlay()) return;
        float[] treatedData = Equalize(hor, ver);
        Vector3 cameraRot = GameManager.cameraController.transform.eulerAngles;
        Vector3 direction = Quaternion.Euler(cameraRot) * new Vector3(treatedData[0], 0, treatedData[1]);
        if (direction.magnitude == 0) return;
        transform.rotation = Quaternion.LookRotation(direction);
        movementDetector.HighlightGroundAhead();
    }

    public void Attack()
    {
        ShooterCharacter shooterCharacter = GetComponent<ShooterCharacter>();
        if (!CanPlay()) return;
        if (shooterCharacter == null) return;
        if (!StartAction()) return;
        attack = true;
        animator.SetTrigger("attack");
        shooterCharacter.SpawnShoot(movementDetector.GetMagicBallOrigin(), movementDetector.GetMagicBallDestination());
    }

    public bool CanPlay()
    {
        return GameManager.active.GetPlayingCharacter() == this && !executingAction && !died && !executingAction;
    }

    #endregion

    #region Util
    public static float[] Equalize(float h, float v)
    {
        float[] axis = {h, v};
        _ = Mathf.Abs(h) > Mathf.Abs(v) ? (axis[1] = 0) : (axis[0] = 0);
        return axis;
    }
    #endregion
}