using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public Button right;
    public Button left;
    public Button attack;
    public Button move;
    public Button rightArrow;
    public Button leftArrow;
    public Button upArrow;
    public Button downArrow;

    // Start is called before the first frame update
    void Start()
    {
        right.onClick.AddListener(delegate { RotateCamera(-1f); });
        left.onClick.AddListener(delegate { RotateCamera(1f); });
        move.onClick.AddListener(MoveAction);
        attack.onClick.AddListener(AttackAction);
        rightArrow.onClick.AddListener(delegate { MoveTo(1, 0); });
        leftArrow.onClick.AddListener(delegate { MoveTo(-1, 0); });
        upArrow.onClick.AddListener(delegate { MoveTo(0, 1); });
        downArrow.onClick.AddListener(delegate { MoveTo(0, -1); });
    }

    private void Update()
    {
        if (CharacterComponent.SelectedCharacter.characterInformation.aiControlled) return;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float move = Input.GetAxis("Fire2");
        float attack = Input.GetAxis("Fire3");
        if (Mathf.Abs(y) > 0.2f)
            MoveTo(0, y);
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            MoveTo(x, 0);
        if (Mathf.Abs(move) > 0)
            MoveAction();
        else if (Mathf.Abs(attack) > 0)
            AttackAction();
    }

    public void RotateCamera(float side)
    {
        CameraController c = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraController>();
        c.MoveCamera(side);
    }
    public void AttackAction()
    {
        //attack
        CharacterComponent.SelectedCharacter.Attack();
    }

    public void MoveAction()
    {
        CharacterComponent.SelectedCharacter.Move(TileComponent.SelectedTile);
    }

    public void MoveTo(float x, float y)
    {
        CharacterComponent character = CharacterComponent.SelectedCharacter;
        if (character.IsMoving || character.IsAttacking) return;
        Vector2 index = character.attachedTile.tileIndex;
        CameraController c = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraController>();
        int div = (int) c.destinationRot.eulerAngles.y / 90;
        switch (div)
        {
            //90
            case 1:
                index.x += x;
                index.y += y;
                break;
            //180
            case 2:
                index.x += y;
                index.y -= x;
                break;
            //270
            case 3:
                index.x -= x;
                index.y -= y;
                break;
            //0
            default:
                index.x -= y;
                index.y += x;
                break;
        }
        TileComponent tile;
        try
        {
            tile = character.gameManager.currentLevel.tilesInGameRunning[(int)index.x][(int)index.y];
        } catch(System.ArgumentOutOfRangeException)
        {
            return;
        }
        character.LookAt(tile.transform.position);
        tile.Select();
    }
}
