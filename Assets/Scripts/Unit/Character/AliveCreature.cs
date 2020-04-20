using System;
using UnityEngine;

public class AliveCreature : UnitComponent
{

    public Action changeLifeEvent;

    #region life
    private int Life = 0;
    public int life
    {
        get => Life;
    }

    public void ChangeLife(int change)
    {
        Life += change;
        if (Life <= 0) Kill();
        if (changeLifeEvent != null)
            changeLifeEvent();
    }

    public void SetLife(int lifeSet)
    {
        Life = lifeSet;
    }

    #endregion

    #region alive
    private bool Alive = true;
    public bool alive
    {
        get => Alive;
    }
    public virtual void Kill()
    {
        CharacterComponent character = GetComponent<CharacterComponent>();
        if (character != null)
            if (character.IsTurn())
                gameManager.turn++;
        Alive = false;
    }
    #endregion
}
