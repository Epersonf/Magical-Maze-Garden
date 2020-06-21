using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!interfaceController) Debug.LogError("No InterfaceController found.");
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
    List<TurnManager> characters = new List<TurnManager>();

    public List<TurnManager> GetCharacters()
    {
        return characters;
    }

    public void AddCharacter(TurnManager characterComponent)
    {
        characters.Add(characterComponent);
    }

    public void RemoveCharacter(TurnManager characterComponent)
    {
        TurnManager current = GetPlayingCharacter();
        characters.Remove(characterComponent);
        Turn = characters.IndexOf(current);
    }

    public TurnManager GetPlayingCharacter()
    {
        return characters[turn];
    }


    private int Turn = 0;
    public int turn
    {
        get => Turn;
        set
        {
            GetPlayingCharacter().BroadcastMessage("UnhighlightGroundAhead");
            if (value >= 0 && value < characters.Count) Turn = value;
            else Turn = 0;
            GetPlayingCharacter().SendMessage("StartTurn");
            cameraController.Focus(characters[turn].gameObject);
            UpdateInterface();
        }
    }

    public void UpdateInterface()
    {
        if (!interfaceController.updatableInterface) return;
        interfaceController.updatableInterface.UpdateTurn();
    }

    private void PassTurn()
    {
        turn++;
    }
    #endregion

    #region NextLevel
    public string NextScene = "Spring";
    public void NextLevel()
    {
        if (!GameEnded()) return;
        SceneManager.LoadScene(NextScene);
    }

    private bool GameEnded()
    {
        foreach (TurnManager player in characters)
            if (!player.playerControlled) return false;
        return true;
    }
    #endregion

    #region GameOver
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion
}