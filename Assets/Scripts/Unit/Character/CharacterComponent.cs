using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : AliveCreature
{
    /// <summary>
    /// The tile this character is on.
    /// </summary>
    public TileComponent attachedTile;
    public string GenerateCharacterName()
    {
        string number = "";
        int i = 1;
        foreach (CharacterComponent c in gameManager.currentLevel.charactersInGameRunning)
        {
            if (c == this) break;
            if (c.characterInformation == this.characterInformation)
            {
                i++;
                number = i.ToString();
            }
        }
        return characterInformation.characterName + " " + number;
    }
    public static CharacterComponent SelectedCharacter = null;
    public CharacterData characterInformation;

    #region Selection
    private int ActionsRemaining = 0;
    public int actionsRemaining
    {
        get => ActionsRemaining;
    }

    public override void Select()
    {
        if (SelectedCharacter != null) SelectedCharacter.Unselect();
        ActionsRemaining = characterInformation.actionsPerTurn;
        base.Select();
        if (characterInformation.aiControlled)
            AIController.TakeAction(this);
        SelectedCharacter = this;
    }

    public override void Unselect()
    {
        base.Unselect();
        CharacterComponent.SelectedCharacter = null;
    }
    #endregion

    #region Actions

    #region Util
    public void LookAt(Vector3 local)
    {
        //rotating
        transform.LookAt(local, Vector3.up);
    }

    public int TileDistance(Vector2 a, Vector2 b)
    {
        Vector2 dif = a - b;
        return (int)(Mathf.Abs(dif.x) + Mathf.Abs(dif.y));
    }
    public bool IsTurn()
    {
        return gameManager.currentLevel.charactersInGameRunning[gameManager.turn] == this;
    }

    public void StartAction()
    {
        ActionsRemaining--;
    }

    public void EndAction()
    {
        attacking = false;
        moving = false;
        gameManager.interfaceUpdater.UpdateInterface();
        if (actionsRemaining <= 0)
            gameManager.turn++;
        else if (characterInformation.aiControlled)
            AIController.TakeAction(this);
    }
    public void PassTurn()
    {
        StartAction();
        EndAction();
    }
    #endregion

    #region Movement
    TileComponent movementTarget = null;
    public bool CanMove(TileComponent tile)
    {
        if (tile == null) return false;
        if (attacking) return false;
        if (attachedTile == null) return false;
        if (tile.type == TileMovementType.Impassable || (actionsRemaining <= 1 && tile.type == TileMovementType.Jumpable)) return false;
        if (tile.Occupied()) return false;
        if (TileDistance(attachedTile.tileIndex, tile.tileIndex) > 1) return false;
        return true;
    }

    private bool moving = false;
    public bool IsMoving
    {
        get => moving;
    }
    public void Move(TileComponent tile)
    {
        if (tile == null) return;
        if (attacking) return;
        if (!selected && !moving) return;
        if (!CanMove(tile)) return;
        attachedTile.RemoveCharacter();
        movementTarget = tile;
        moving = true;
        if (TileComponent.SelectedTile != null)
            TileComponent.SelectedTile.Unselect();
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetBool("moving", true);
        StartAction();
    }

    const float vel = 1f;
    const float margin = 0.01f;
    private void MoveToTile()
    {
        if (!moving) return;
        Vector3 local = movementTarget.transform.position;
        local.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, local, vel * Time.deltaTime);

        LookAt(local);

        if (Vector3.Distance(transform.position, local) < margin)
        {
            movementTarget.AttachCharacter(this);
            moving = false;
            if (GetComponent<Animator>() != null)
                GetComponent<Animator>().SetBool("moving", false);
            EndAction();
        }
    }
    #endregion

    #region Attack
    bool attacking = false;
    public bool IsAttacking
    {
        get => attacking;
    }
    public bool CanAttack()
    {
        if (moving || attacking) return false;
        if (!characterInformation.canAttack) return false;
        if (attachedTile == null) return false;
        if (attachedTile.type != TileMovementType.Passable) return false;
        return true;
    }

    public void Attack()
    {
        if (!CanAttack()) return;
        attacking = true;
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("attacking");
        StartAction();
        StartCoroutine(AttackAimed());
    }

    private IEnumerator AttackAimed()
    {
        yield return new WaitForSeconds(2f);
        if (!attacking) yield break;
        MagicBallComponent mBall = MagicBall.Generate(this, this.transform.position);
        mBall.executeAfter += this.EndAction;
    }

    #endregion
    #endregion


    private void Start()
    {
        SetLife(characterInformation.life);
        unitName = GenerateCharacterName();
        if (IsTurn())
            gameManager.interfaceUpdater.UpdateInterface();
    }
    void Update()
    {
        MoveToTile();
    }

    public override void Kill()
    {
        base.Kill();
        gameManager.currentLevel.charactersInGameRunning.Remove(this);
        if (attachedTile != null)
            attachedTile.RemoveCharacter();
        if (characterInformation.coreCharacter)
            gameManager.GameOver(false);
        Destroy(gameObject);
    }
}
