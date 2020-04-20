using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region high
    [SerializeField]
    private float High = 0f;
    public float high
    {
        get => High;
    }
    #endregion

    #region gridSize
    [SerializeField]
    private float GridSize = 0.5f;
    public float gridSize
    {
        get => GridSize;
    }
    #endregion

    public GameLevel currentLevel;
    public InterfaceUpdater interfaceUpdater;
    public InterfaceController interfaceController;

    #region int turn = 0; Turn manager
    private int Turn = 0;
    public int turn
    {
        get => Turn;
        set
        {
            if (currentLevel.Lost()) GameOver(false);
            if (currentLevel.Won()) GameOver(true);
            if (value >= 0 && value < currentLevel.charactersInGameRunning.Count)
                Turn = value;
            else
                Turn = 0;
            CharacterComponent characterToPlay = currentLevel.charactersInGameRunning[Turn];
            GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraController>().Focus(characterToPlay.gameObject);
            if (!characterToPlay.characterInformation.hasTurn)
            {
                turn++;
                return;
            }
            characterToPlay.Select();
            interfaceUpdater.UpdateInterface();
        }
    }
    #endregion

    void Awake()
    {
        currentLevel.Generate(this);
    }
    private void Start()
    {
        turn = 0;
    }

    public void GameOver(bool won)
    {
        Debug.LogError("Victory: " + won);
    }
}
