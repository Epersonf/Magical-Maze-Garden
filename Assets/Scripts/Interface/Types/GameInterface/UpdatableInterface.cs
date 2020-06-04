using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatableInterface : MonoBehaviour
{
    public Text turnInfo;

    public void UpdateTurn()
    {
        TurnManager actual = GameManager.active.GetPlayingCharacter();
        if (actual)
            turnInfo.text = "Turn of " + actual.name + "\nActions remaining: " + actual.actionsRemaining;
    }
}
