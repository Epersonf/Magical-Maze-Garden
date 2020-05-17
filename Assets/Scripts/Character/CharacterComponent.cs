using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class CharacterComponent : MonoBehaviour
{
    public string characterName = "Emilia";
    public Sprite characterSprite;
    public bool playerControlled = true;
    public int actionsPerTurn = 3;

    MovementDetector movementDetector;
    Rigidbody rg;
    Collider clldr;
    Animator animator;

    protected virtual void Awake()
    {
        GameManager.active.AddCharacter(this);
        movementDetector = GetComponentInChildren<MovementDetector>();
        rg = GetComponent<Rigidbody>();
        clldr = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        GoToDestination();
    }

    public void GoToDestination()
    {
        if (attachedGround == null) return;
        float distance = Vector3.Distance(transform.position, attachedGround.transform.position);
        if (distance > 0.1f)
        {
            transform.LookAt(attachedGround.characterPosition);
        }
    }

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
            if (value.IsOccupied()) return;
            if (AttachedGround != null)
                AttachedGround.attachedCharacter = null;
            AttachedGround = value;
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
        if (GameManager.active.GetPlayingCharacter() != this) return;

    }

    public void Rotate(float hor, float ver)
    {
        if (GameManager.active.GetPlayingCharacter() != this) return;
        Vector3 cameraRot = GameManager.cameraController.transform.eulerAngles;
        Vector3 direction = Quaternion.Euler(cameraRot) * new Vector3(hor, 0, ver);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Attack()
    {
        if (GameManager.active.GetPlayingCharacter() != this) return;

    }
    #endregion
}
