﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ShooterCharacter))]
[RequireComponent(typeof(MovementManager))]
[RequireComponent(typeof(AnimationBehaviour))]
[RequireComponent(typeof(AliveCreature))]
[RequireComponent(typeof(AIController))]
public class TurnManager : MonoBehaviour
{
    const float margin = 0.4f;

    public string characterName = "Emilia";
    public Sprite characterSprite;
    public bool playerControlled = true;
    public int actionsPerTurn = 3;

    private int ActionsRemaining = 0;
    public int actionsRemaining { get => ActionsRemaining; }


    private void Awake()
    {
        GameManager.active.SendMessage("AddCharacter", this, SendMessageOptions.DontRequireReceiver);
    }

    public bool IsTurn()
    {
        return GameManager.active.GetPlayingCharacter() == this;
    }

    bool ExecutingAction = false;
    public bool executingAction { get => ExecutingAction; }

    public void StartAction()
    {
        if (!IsTurn() || executingAction || actionsRemaining <= 0) return;
        ActionsRemaining--;
        ExecutingAction = true;
    }

    public void EndAction()
    {
        if (!ExecutingAction) return;
        ExecutingAction = false;
        GameManager.active.SendMessage("UpdateInterface");
        if (actionsRemaining <= 0) GameManager.active.SendMessage("PassTurn");
        else if (!playerControlled) SendMessage("AIPlay");
    }

    public void StartTurn()
    {
        ActionsRemaining = actionsPerTurn;
        if (!playerControlled)
            SendMessage("AIPlay");
    }

    public void EndTurn()
    {
        if (actionsRemaining <= 0) GameManager.active.SendMessage("PassTurn");
    }

    public void PassAction()
    {
        StartAction();
        EndAction();
    }

    public void OnDieEvent()
    {
        GameManager.active.RemoveCharacter(this);
        this.enabled = false;
    }
}