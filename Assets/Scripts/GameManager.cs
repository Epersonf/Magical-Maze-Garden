using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Static references setup
    public static GameManager active;
    public static CameraController cameraController;

    public void ActualizeStaticReferences()
    {
        active = this;
        cameraController = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraController>();
    }
    #endregion

    #region Inherited functions
    public void Awake()
    {
        ActualizeStaticReferences();
    }

    public void Start()
    {

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
        }
    }
    #endregion
}
