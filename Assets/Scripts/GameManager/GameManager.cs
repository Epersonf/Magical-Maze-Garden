using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Static references setup
    public static GameManager active;
    public static CameraController cameraController;
    public static InterfaceController interfaceController;

    public void ActualizeStaticReferences()
    {
        active = this;
        cameraController = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraController>();
        interfaceController = GameObject.FindGameObjectWithTag("InterfaceController").GetComponent<InterfaceController>();
    }
    #endregion

    #region Inherited functions
    public void Awake()
    {
        ActualizeStaticReferences();
    }

    public void Start()
    {
        turn = 0;
    }

    public void Update()
    {
        
    }
    #endregion

    #region Turn Manager
    List<CharacterComponent> characters = new List<CharacterComponent>();

    public void AddCharacter(CharacterComponent characterComponent)
    {
        characters.Add(characterComponent);
    }

    public CharacterComponent GetPlayingCharacter()
    {
        return characters[turn];
    }


    private int Turn = 0;
    public int turn
    {
        get => Turn;
        set
        {
            if (value >= 0 && value < characters.Count) Turn = value;
            else Turn = 0;
            characters[turn].ResetActions();
            interfaceController.updatableInterface.UpdateTurn();
            cameraController.Focus(characters[turn].gameObject);
        }
    }
    #endregion
}
