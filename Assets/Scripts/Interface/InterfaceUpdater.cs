using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUpdater : MonoBehaviour
{
    public GameManager gameManager;

    public Text roundAnnouncer;

    public void UpdateInterface()
    {
        UpdateTurnText();
    }

    private void UpdateTurnText()
    {
        try
        {
            CharacterComponent selected = gameManager.currentLevel.charactersInGameRunning[gameManager.turn];
            string name = selected.unitName;
            roundAnnouncer.text = "Turn of " + name + "\nActions remaining: " + selected.actionsRemaining;
        } catch(System.ArgumentOutOfRangeException)
        {
            gameManager.turn++;
        }
    }
}
