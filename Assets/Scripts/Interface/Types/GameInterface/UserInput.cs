using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public Button Right_Camera;
    public Button Left_Camera;

    public Button Move;
    public Button Attack;
    public Button Up;
    public Button Down;
    public Button Right;
    public Button Left;

    private void Awake()
    {
        Right_Camera.onClick.AddListener(delegate { RotateCamera(-1); });
        Left_Camera.onClick.AddListener(delegate { RotateCamera(1); });

        Up.onClick.AddListener(delegate { RotateCharacter(0, 1); });
        Down.onClick.AddListener(delegate { RotateCharacter(0, -1); });
        Right.onClick.AddListener(delegate { RotateCharacter(1, 0); });
        Left.onClick.AddListener(delegate { RotateCharacter(-1, 0); });

        Move.onClick.AddListener(MoveCharacter);
        Attack.onClick.AddListener(AttackCharacter);
    }

    private void Update()
    {
        RotateCharacter(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButton("Fire3"))
            MoveCharacter();
        if (Input.GetButton("Fire2"))
            AttackCharacter();
        if (Input.GetButtonDown("L1"))
            RotateCamera(1);
        if (Input.GetButtonDown("R1"))
            RotateCamera(-1);
    }

    public bool IsCharacterUserControlled()
    {
        return GameManager.active.GetPlayingCharacter().playerControlled;
    }

    public void RotateCamera(float side)
    {
        GameManager.cameraController.RotateCamera(side);
    }

    public void RotateCharacter(float hor, float ver)
    {
        if (!IsCharacterUserControlled()) return;
        CharacterComponent characterComponent = GameManager.active.GetPlayingCharacter();
        characterComponent.Rotate(hor, ver);
    }

    public void MoveCharacter()
    {
        if (!IsCharacterUserControlled()) return;
        CharacterComponent characterComponent = GameManager.active.GetPlayingCharacter();
        characterComponent.Move();
    }

    public void AttackCharacter()
    {
        if (!IsCharacterUserControlled()) return;
        CharacterComponent characterComponent = GameManager.active.GetPlayingCharacter();
        characterComponent.Attack();
    }
}
