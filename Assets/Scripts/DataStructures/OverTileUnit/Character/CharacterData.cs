using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects Data/CharacterData")]
public class CharacterData : DataStructure
{
    public string characterName;
    public Sprite faceImage;
    public GameObject characterPrefab;
    public CharacterType type;
    [Tooltip("If the character can execute actions in a turn.")]
    public bool hasTurn = true;
    [Tooltip("How many actions you can execute in a turn.")]
    public int actionsPerTurn = 2;
    [Tooltip("If can move through jumpable objects.")]
    public bool canJump = false;
    [Space(5)]
    [Tooltip("If the character can attack.")]
    public bool canAttack = true;
    [Tooltip("Prefab for the MagicBall.")]
    public GameObject magicBallPrefab;
    [Tooltip("MagicBall material.")]
    public Material magicBallMaterial;
    [Tooltip("Damage per attack.")]
    public int damage = 1;
    [Space(5)]
    [Tooltip("How many damage you can take.")]
    public int life = 1;
    [Tooltip("Attacks max distance in tiles.")]
    public int range = 3;
    [Tooltip("If the character can interact.")]
    public bool canInteract = true;
    [Tooltip("If the character is controlled by a bot.")]
    public bool aiControlled = false;
    [Tooltip("If this is active, the character death will result in a gameover.")]
    public bool coreCharacter = false;

    public CharacterComponent Generate(TileComponent tile)
    {
        if (tile.Occupied())
        {
            Debug.LogError("There are two characters in the same tile.");
        }
        GameObject obj = Instantiate(characterPrefab, tile.transform.position, Quaternion.identity);
        CharacterComponent charComp = obj.AddComponent(typeof(CharacterComponent)) as CharacterComponent;
        charComp.characterInformation = this;
        BoxCollider collider = obj.AddComponent(typeof(BoxCollider)) as BoxCollider;
        collider.size = new Vector3(charComp.gameManager.gridSize * 5, charComp.gameManager.gridSize * 15, charComp.gameManager.gridSize * 5);
        tile.AttachCharacter(charComp);
        return charComp;
    }
}

public enum CharacterType
{
    Ally, Enemy
}